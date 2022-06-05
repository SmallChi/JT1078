using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Protocol.H264
{
    public class H264Decoder
    {
        /// <summary>
        /// 
        /// <see cref="https://github.com/samirkumardas/jmuxer/blob/master/src/parsers/h264.js"/>
        /// </summary>
        /// <param name="package"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<H264NALU> ParseNALU(JT1078Package package, string key = null)
        {
            List<H264NALU> h264NALUs = new List<H264NALU>();
            int i=0,state=0,laststate=0;
            int? lastIndex=null;
            int length = package.Bodies.Length;
            byte value;
            ReadOnlySpan<byte> buffer = package.Bodies;
            while (i < length)
            {
                value = buffer[i++];
                switch (state)
                {
                    case 0:
                        if (value == 0) state = 1;
                        break;
                    case 1:
                        state = value == 0 ? 2 : 0;
                        break;
                    case 2:
                    case 3:
                        if (value == 0)
                        {
                            state = 3;
                        }
                        else if (value == 1 && i < length)
                        {
                            if (lastIndex.HasValue)
                            {
                                var tmp = buffer.Slice(lastIndex.Value, i - state - 1 - lastIndex.Value);
                                h264NALUs.Add(Create(package, tmp, state+1));
                            }
                            lastIndex = i;
                            laststate = state+1;
                            state = 0;
                        }
                        else
                        {
                            state = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (lastIndex.HasValue)
            {
                h264NALUs.Add(Create(package, buffer.Slice(lastIndex.Value), laststate));
            }
            return h264NALUs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public JT1078AVFrame ParseAVFrame(JT1078Package package, string key = null)
        {
            JT1078AVFrame jT1078AVFrame = new JT1078AVFrame();
            jT1078AVFrame.LogicChannelNumber = package.LogicChannelNumber;
            jT1078AVFrame.SIM = package.SIM;
            jT1078AVFrame.Nalus = new List<H264NALU>();
            int i = 0, state = 0, laststate = 0;
            int? lastIndex = null;
            int length = package.Bodies.Length;
            byte value;
            ReadOnlySpan<byte> buffer = package.Bodies;
            while (i < length)
            {
                value = buffer[i++];
                switch (state)
                {
                    case 0:
                        if (value == 0) state = 1;
                        break;
                    case 1:
                        state = value == 0 ? 2 : 0;
                        break;
                    case 2:
                    case 3:
                        if (value == 0)
                        {
                            state = 3;
                        }
                        else if (value == 1 && i < length)
                        {
                            if (lastIndex.HasValue)
                            {
                                var tmp = buffer.Slice(lastIndex.Value, i - state - 1 - lastIndex.Value);
                                H264NALU nalu = Create(package, tmp, state + 1);
                                if (nalu.NALUHeader.NalUnitType == NalUnitType.PPS)
                                {
                                    jT1078AVFrame.PPS = nalu;
                                }
                                else if (nalu.NALUHeader.NalUnitType == NalUnitType.SPS)
                                {
                                    jT1078AVFrame.SPS = nalu;
                                    ExpGolombReader h264GolombReader = new ExpGolombReader(jT1078AVFrame.SPS.RawData);
                                    var spsInfo = h264GolombReader.ReadSPS();
                                    jT1078AVFrame.Width = spsInfo.width;
                                    jT1078AVFrame.Height = spsInfo.height;
                                    jT1078AVFrame.LevelIdc= spsInfo.levelIdc;
                                    jT1078AVFrame.ProfileIdc=spsInfo.profileIdc;
                                    jT1078AVFrame.ProfileCompat=(byte)spsInfo.profileCompat;
                                }
                                jT1078AVFrame.Nalus.Add(nalu);
                            }
                            lastIndex = i;
                            laststate = state + 1;
                            state = 0;
                        }
                        else
                        {
                            state = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (lastIndex.HasValue)
            {
                H264NALU nalu = Create(package, buffer.Slice(lastIndex.Value), laststate);
                if(nalu.NALUHeader.NalUnitType== NalUnitType.PPS)
                {
                    jT1078AVFrame.PPS = nalu;
                }
                else if(nalu.NALUHeader.NalUnitType == NalUnitType.SPS)
                {
                    jT1078AVFrame.SPS = nalu;
                    ExpGolombReader h264GolombReader = new ExpGolombReader(jT1078AVFrame.SPS.RawData);
                    var spsInfo = h264GolombReader.ReadSPS();
                    jT1078AVFrame.Width = spsInfo.width;
                    jT1078AVFrame.Height = spsInfo.height;
                    jT1078AVFrame.LevelIdc = spsInfo.levelIdc;
                    jT1078AVFrame.ProfileIdc = spsInfo.profileIdc;
                    jT1078AVFrame.ProfileCompat = (byte)spsInfo.profileCompat;
                }
                jT1078AVFrame.Nalus.Add(nalu);
            }
            return jT1078AVFrame;
        }

        /// <summary>
        /// 
        /// <see cref="https://github.com/samirkumardas/jmuxer/blob/master/src/parsers/h264.js"/>
        /// </summary>
        /// <param name="package"></param>
        /// <param name="h264NALUs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public void ParseNALU(JT1078Package package, List<H264NALU> h264NALUs,string key = null)
        {
            int i = 0, state = 0, laststate = 0;
            int? lastIndex = null;
            int length = package.Bodies.Length;
            byte value;
            ReadOnlySpan<byte> buffer = package.Bodies;
            while (i < length)
            {
                value = buffer[i++];
                switch (state)
                {
                    case 0:
                        if (value == 0) state = 1;
                        break;
                    case 1:
                        state = value == 0 ? 2 : 0;
                        break;
                    case 2:
                    case 3:
                        if (value == 0)
                        {
                            state = 3;
                        }
                        else if (value == 1 && i < length)
                        {
                            if (lastIndex.HasValue)
                            {
                                var tmp = buffer.Slice(lastIndex.Value, i - state - 1 - lastIndex.Value);
                                h264NALUs.Add(Create(package, tmp, lastIndex.Value));
                            }
                            lastIndex = i;
                            laststate = state + 1;
                            state = 0;
                        }
                        else
                        {
                            state = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (lastIndex.HasValue)
            {
                h264NALUs.Add(Create(package, buffer.Slice(lastIndex.Value), laststate));              
            }
        }

        private H264NALU Create(JT1078Package package,ReadOnlySpan<byte> nalu, int startCodePrefix)
        {
            H264NALU nALU = new H264NALU();
            nALU.SIM = package.SIM;
            nALU.DataType = package.Label3.DataType;
            nALU.LogicChannelNumber = package.LogicChannelNumber;
            nALU.LastFrameInterval = package.LastFrameInterval;
            nALU.LastIFrameInterval = package.LastIFrameInterval;
            nALU.Timestamp = package.Timestamp;
            nALU.RawData = nalu.ToArray();
            if (startCodePrefix == 3)
            {
                nALU.StartCodePrefix = H264NALU.Start1;
            }
            else if (startCodePrefix == 4)
            {
                nALU.StartCodePrefix = H264NALU.Start2;
            }
            nALU.NALUHeader = new NALUHeader(nalu.Slice(0,1));  
            return nALU;
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
                if (EPBPositions.Any()&&sourceIndex == EPBPositions[0])
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
