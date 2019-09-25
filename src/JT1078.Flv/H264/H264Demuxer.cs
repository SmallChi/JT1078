using JT1078.Flv.Extensions;
using JT1078.Flv.MessagePack;
using JT1078.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Flv.H264
{
    public class H264Demuxer
    {
        public List<H264NALU> ParseNALU(JT1078Package package)
        {
            List<H264NALU> units = new List<H264NALU>();
            int offset = 0;
            (int previousOffset, int previousContentOffset) previous = (0, 0);
            int len = package.Bodies.Length;
            ReadOnlySpan<byte> tmpBuffer = package.Bodies;
            int index = 0;
            while (offset < len)
            {
                if ((len - offset - 3) < 0 || (len - offset - 4) < 0)
                {
                    if (previous.previousOffset == 0 && previous.previousContentOffset == 0)
                    {
                        int startCodePrefix=4;
                        if (tmpBuffer.Slice(0, 3).SequenceEqual(H264NALU.Start1))
                        {
                            startCodePrefix = 3;
                        }
                        units.Add(Create(package, tmpBuffer.Slice(offset, 1), startCodePrefix));
                        units[index++].RawData = tmpBuffer.ToArray();
                    }
                    else
                    {
                        units[index++].RawData = tmpBuffer.Slice(previous.previousContentOffset + (previous.previousOffset - previous.previousContentOffset)).ToArray();
                    }
                    break;
                }
                if (tmpBuffer.Slice(offset, 3).SequenceEqual(H264NALU.Start1))
                {
                    offset += 3;
                    if ((offset - 3) != 0)
                    {
                        units[index++].RawData = tmpBuffer.Slice(previous.previousContentOffset + 3, offset - previous.previousOffset - 3).ToArray();
                    }
                    units.Add(Create(package, tmpBuffer.Slice(offset, 1), 3));
                    previous = (offset, offset - 3);
                }
                else if (tmpBuffer.Slice(offset, 4).SequenceEqual(H264NALU.Start2))
                {
                    offset += 4;
                    if ((offset - 4) != 0)
                    {
                        units[index++].RawData = tmpBuffer.Slice(previous.previousContentOffset + 4, offset - previous.previousOffset - 4).ToArray();
                    }
                    units.Add(Create(package, tmpBuffer.Slice(offset, 1), 4));
                    previous = (offset, offset - 4);
                }
                else
                {
                    offset++;
                }
            }
            return units;
        }

        private H264NALU Create(JT1078Package package,ReadOnlySpan<byte> naluheader, int startCodePrefix)
        {
            H264NALU nALU = new H264NALU();
            nALU.SIM = package.SIM;
            nALU.Label3 = package.Label3;
            nALU.LogicChannelNumber = package.LogicChannelNumber;
            nALU.LastFrameInterval = package.LastFrameInterval;
            nALU.LastIFrameInterval = package.LastIFrameInterval;
            if (startCodePrefix == 3)
            {
                nALU.StartCodePrefix = H264NALU.Start1;
            }
            else if (startCodePrefix == 4)
            {
                nALU.StartCodePrefix = H264NALU.Start2;
            }
            nALU.NALUHeader = new NALUHeader(naluheader);  
            return nALU;
        }

        /// <summary>
        /// Identify NAL unit types and pass on the NALU, trackId, presentation and decode timestamps
        /// for the NALUs to the next stream component.
        /// Also, preprocess caption and sequence parameter NALUs.
        /// 常用Nalu_type:
        /// 0x67 (0 11 00111) SPS   非常重要              type = 7
        /// 0x68 (0 11 01000) PPS   非常重要              type = 8
        /// 0x65 (0 11 00101) IDR帧 关键帧(非常重要)      type = 5
        /// 0x61 (0 11 00001) I帧   重要                  type = 1  非IDR的I帧不大常见
        /// 0x41 (0 10 00001) P帧   重要                  type = 1     
        /// 0x01 (0 00 00001) B帧   不重要                type = 1
        /// 0x06 (0 00 00110) SEI   不重要                type = 6
        /// <see cref="https://blog.csdn.net/huibailingyu/article/details/42879573"/>
        /// </summary>
        /// <param name="h264NALU"></param>
        /// <returns></returns>
        public void NALUTypeFilter(H264NALU h264NALU)
        {
            switch (h264NALU.NALUHeader.NalUnitType)
            {
                //IDR
                case 5:
                    
                    break;
                case 6:
                    h264NALU.RawData = DiscardEmulationPreventionBytes(h264NALU.RawData);

                    break;
                //SPS
                case 7:
                    h264NALU.RawData = DiscardEmulationPreventionBytes(h264NALU.RawData);
                    ExpGolombReader h264GolombReader = new ExpGolombReader(h264NALU.RawData);
                    var spsInfo = h264GolombReader.ReadSPS();
                    
                    break;
                //PPS
                case 8:
                    
                    break;
                //AUD
                case 9:
                    
                    break;
            }
        }

        /// <summary>
        /// Expunge any "Emulation Prevention" bytes from a "Raw Byte Sequence Payload"
        /// <see cref="https://blog.csdn.net/u011399342/article/details/80472084"/>
        /// 防止竞争插入0x03
        /// </summary>
        /// <param name="srcBuffer"></param>
        /// <returns></returns>
        public byte[] DiscardEmulationPreventionBytes(ReadOnlySpan<byte> srcBuffer)
        {
            int length = srcBuffer.Length;
            List<int> EPBPositions = new List<int>();
            int i = 1;
            // Find all `Emulation Prevention Bytes`
            while (i < length - 2)
            {
                if (srcBuffer[i] == 0 && srcBuffer[i + 1] == 0 && srcBuffer[i + 2] == 0x03)
                {
                    EPBPositions.Add(i + 2);
                    i += 2;
                }
                else
                {
                    i++;
                }
            }
            // If no Emulation Prevention Bytes were found just return the original
            // array
            if (EPBPositions.Count == 0)
            {
                return srcBuffer.ToArray();
            }
            // Create a new array to hold the NAL unit data
            int newLength = length - EPBPositions.Count;
            byte[] newBuffer = new byte[newLength];
            var sourceIndex = 0;
            for (i = 0; i < newLength; sourceIndex++, i++)
            {
                if (sourceIndex == EPBPositions[0])
                {
                    // Skip this byte
                    sourceIndex++;
                    // Remove this position index
                    EPBPositions.RemoveAt(0);
                }
                newBuffer[i] = srcBuffer[sourceIndex];
            }
            return newBuffer;
        }
    }
}
