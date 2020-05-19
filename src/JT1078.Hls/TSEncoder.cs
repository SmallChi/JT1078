using JT1078.Protocol.Enums;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JT1078.Protocol;
using JT1078.Hls.MessagePack;
using JT1078.Hls.Enums;
using System.Collections.Concurrent;
using System.Security.Cryptography;

[assembly: InternalsVisibleTo("JT1078.Hls.Test")]

namespace JT1078.Hls
{
    /// <summary>
    /// 1.PAT
    /// 2.PMT
    /// 3.PES
    /// </summary>
    public class TSEncoder
    {
        private const int FiexdSegmentPESLength = 184;
        private const int FiexdTSLength = 188;
        private ConcurrentDictionary<string, byte> VideoCounter = new ConcurrentDictionary<string, byte>();
        //private ConcurrentDictionary<string, byte> AudioCounter = new ConcurrentDictionary<string, byte>();
        public byte[] CreatePAT(JT1078Package jt1078Package, int minBufferSize = 188)
        {
            byte[] buffer = TSArrayPool.Rent(minBufferSize);
            try
            {
                TS_PAT_Package package = new TS_PAT_Package();
                package.Header = new TS_Header();
                package.Header.ContinuityCounter = 0;
                package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
                package.Header.PayloadUnitStartIndicator = 1;
                package.Header.PID = 0;
                package.Programs = new List<TS_PAT_Program>();
                package.Programs.Add(new TS_PAT_Program()
                {
                    ProgramNumber = 0x0001,
                    PID = 0x1000,
                });
                TSMessagePackWriter messagePackReader = new TSMessagePackWriter(buffer);
                package.ToBuffer(ref messagePackReader);
                return messagePackReader.FlushAndGetArray();
            }
            finally
            {
                TSArrayPool.Return(buffer);
            }
        }
        public byte[] CreatePMT(JT1078Package jt1078Package, int minBufferSize = 188)
        {
            byte[] buffer = TSArrayPool.Rent(minBufferSize);
            try
            {
                TS_PMT_Package package = new TS_PMT_Package();
                package.Header = new TS_Header();
                package.Header.ContinuityCounter = 0;
                package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
                package.Header.PayloadUnitStartIndicator = 1;
                package.Header.PID = 4096;
                package.TableId = 0x02;
                package.Components = new List<TS_PMT_Component>();
                package.Components.Add(new TS_PMT_Component
                {
                     StreamType= StreamType.h264,
                     ElementaryPID = 256,
                     ESInfoLength=0
                });
                TSMessagePackWriter messagePackReader = new TSMessagePackWriter(buffer);
                package.ToBuffer(ref messagePackReader);
                return messagePackReader.FlushAndGetArray();
            }
            finally
            {
                TSArrayPool.Return(buffer);
            }
        }
        public byte[] CreatePES(JT1078Package jt1078Package, int minBufferSize = 188)
        {
            //将1078一帧的数据拆分成一小段一小段的PES包
            byte[] buffer = TSArrayPool.Rent(jt1078Package.Bodies.Length + minBufferSize);
            TSMessagePackWriter messagePackReader = new TSMessagePackWriter(buffer);
            //TSHeader + Adaptation + PES1
            //TSHeader + PES2
            //TSHeader + Adaptation + PESN
            try
            {
                int totalLength = 0;
                TS_Package package = new TS_Package();
                package.Header = new TS_Header();
                //ts header 4
                totalLength += 4;
                package.Header.PID = 256;
                string key = jt1078Package.GetKey();
                if (VideoCounter.TryGetValue(key, out byte counter))
                {
                    if (counter > 0xf)
                    {
                        counter = 0;
                    }
                    package.Header.ContinuityCounter = counter++;
                    VideoCounter.TryUpdate(key, counter, counter);
                }
                else
                {
                    package.Header.ContinuityCounter = counter++;
                    VideoCounter.TryAdd(key, counter);
                }
                package.Header.PayloadUnitStartIndicator = 1;
                package.Header.Adaptation = new TS_AdaptationInfo();
                package.Payload = new PES_Package();
                package.Payload.StreamId = 0xe0;
                package.Payload.PESPacketLength = 0;
                //PESStartCode + StreamId + PESPacketLength
                //3 + 1 + 2
                totalLength += (3+1+2);
                package.Payload.PTS_DTS_Flag = PTS_DTS_Flags.all;
                if (jt1078Package.Label3.DataType== JT1078DataType.视频I帧)
                {
                    //ts header adaptation
                    //PCRIncluded + PCR
                    //1 + 5
                    totalLength += (1 + 5);
                    package.Header.Adaptation.PCRIncluded = PCRInclude.包含;
                    package.Header.Adaptation.PCR = jt1078Package.LastIFrameInterval;
                    package.Payload.DTS = jt1078Package.LastIFrameInterval;
                    package.Payload.PTS = jt1078Package.LastIFrameInterval;
                }
                else
                {
                    //ts header adaptation
                    //PCRIncluded 
                    //1
                    totalLength += 1;
                    package.Header.Adaptation.PCRIncluded = PCRInclude.不包含;
                    package.Payload.DTS = jt1078Package.LastFrameInterval;
                    package.Payload.PTS = jt1078Package.LastFrameInterval;
                }
                //Flag1 + PTS_DTS_Flag + DTS + PTS
                //1 + 1 + 5 + 5
                totalLength += 12;
                //根据计算剩余的长度进行是否需要填充第一包
                var remainingLength = FiexdTSLength - totalLength;
                int index = 0;
                //情况1:1078的第一包不够剩余(remainingLength)字节
                //情况2:1078的第一包比剩余(remainingLength)字节多
                //情况3: 1078的第一包等于剩余(remainingLength)字节
                //填充大小
                int fullSize = jt1078Package.Bodies.Length - remainingLength;
                package.Payload.Payload = new ES_Package();
                if (fullSize < 0)
                {
                    //这个很重要，需要控制
                    package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
                    //还差一点
                    fullSize = Math.Abs(fullSize);
                    package.Header.Adaptation.FillSize = (byte)fullSize;
                    package.Payload.Payload.NALUs = new List<byte[]>() { jt1078Package.Bodies };
                    package.ToBuffer(ref messagePackReader);
                }
                else if(fullSize==0)
                {
                    //这个很重要，需要控制
                    package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
                    //刚刚好
                    package.Header.Adaptation.FillSize = 0;
                    package.Payload.Payload.NALUs = new List<byte[]>() { jt1078Package.Bodies };
                    package.ToBuffer(ref messagePackReader);
                }
                else
                {
                    //这个很重要，需要控制
                    package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
                    //太多了，需要拆分
                    package.Header.Adaptation.FillSize = 0;
                    package.Payload.Payload.NALUs = new List<byte[]>();
                    ReadOnlySpan<byte> dataReader = jt1078Package.Bodies;
                    package.Payload.Payload.NALUs.Add(dataReader.Slice(index, remainingLength).ToArray());
                    index += remainingLength;
                    package.ToBuffer(ref messagePackReader);
                    while(index!= jt1078Package.Bodies.Length)
                    {
                        if (counter > 0xf)
                        {
                            counter = 0;
                        }
                        int segmentFullSize = jt1078Package.Bodies.Length - index;
                        if(segmentFullSize >= FiexdSegmentPESLength)
                        {
                            CreateSegmentPES(ref messagePackReader, dataReader.Slice(index, FiexdSegmentPESLength).ToArray(), counter++);
                            index += FiexdSegmentPESLength;
                        }
                        else
                        {
                            CreateSegmentPES(ref messagePackReader, dataReader.Slice(index, segmentFullSize).ToArray(), counter++);
                            index += segmentFullSize;
                        }
                    }
                    VideoCounter.TryUpdate(key, counter, counter);
                }
                return messagePackReader.FlushAndGetArray();
            }
            finally
            {
                TSArrayPool.Return(buffer);
            }
        }
        internal void CreateSegmentPES(ref TSMessagePackWriter writer,byte[] nalu, byte counter)
        {
            TS_Segment_Package package = new TS_Segment_Package();
            package.Header = new TS_Header();
            package.Header.PID = 256;
            package.Header.ContinuityCounter = counter;
            package.Header.PayloadUnitStartIndicator = 0;
            package.Payload = nalu;
            //这个很重要，需要控制
            //package.Header.AdaptationFieldControl= AdaptationFieldControl.同时带有自适应域和有效负载
            //这个很重要，填充大小
            //package.Header.Adaptation.FillSize
            if (nalu.Length < FiexdSegmentPESLength)
            {
                package.Header.PackageType = PackageType.Data_End;
                package.Header.Adaptation = new TS_AdaptationInfo();
                package.Header.Adaptation.PCRIncluded = PCRInclude.不包含;
                package.Header.Adaptation.FillSize = (byte)(FiexdSegmentPESLength - nalu.Length);
                package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
            }
            else
            {
                package.Header.PackageType = PackageType.Data_Segment;
                package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
            }
            package.Payload = nalu;
            package.ToBuffer(ref writer);
        }
    }
}
