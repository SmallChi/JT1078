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
using JT1078.Hls.Descriptors;
using JT1078.Protocol.Extensions;
using JT1078.Hls.Options;
using System.Buffers;

[assembly: InternalsVisibleTo("JT1078.Hls.Test")]

namespace JT1078.Hls
{
    /// <summary>
    /// 1.SDT
    /// 2.PAT
    /// 3.PMT
    /// 4.PES
    /// </summary>
    public class TSEncoder
    {
        private const int FiexdSegmentPESLength = 184;
        private const int FiexdTSLength = 188;
        private const string ServiceProvider = "JTT1078";
        private const string ServiceName = "Koike&TK"; 
        private const int H264DefaultHZ = 90;
        private Dictionary<string, byte> VideoCounter;
        //todo:音频同步
        //private Dictionary<string, byte> AudioCounter = new Dictionary<string, byte>();

      /// <summary>
      /// 
      /// </summary>
        public TSEncoder()
        {
            VideoCounter = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
        }

        public byte[] CreateSDT(int minBufferSize = 188)
        {
            byte[] buffer = TSArrayPool.Rent(minBufferSize);
            try
            {
                TS_SDT_Package package = new TS_SDT_Package();
                package.Header = new TS_Header();
                package.Header.PID = 0x0011;
                package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
                package.Header.ContinuityCounter = 0;
                package.TableId = 0x42;
                package.TransportStreamId = 0x0001;
                package.VersionNumber = 0;
                package.CurrentNextIndicator = 0x01;
                package.SectionNumber = 0x00;
                package.LastSectionNumber = 0x00;
                package.OriginalNetworkId = 0xFF01;
                package.Services = new List<TS_SDT_Service>();
                package.Services.Add(new TS_SDT_Service()
                {
                    ServiceId = 0x0001,
                    EITScheduleFlag = 0x00,
                    EITPresentFollowingFlag = 0x00,
                    RunningStatus = TS_SDT_Service_RunningStatus.运行,
                    FreeCAMode = 0x00,
                    Descriptors = new List<TS_SDT_Service_Descriptor> 
                    {
                         new TS_SDT_Service_Descriptor{
                            Tag=0x48,
                            ServiceType= TS_SDT_Service_Descriptor_ServiceType.数字电视业务,
                            ServiceProvider=ServiceProvider,
                            ServiceName=ServiceName
                         }
                    }
                });
                TSMessagePackWriter writer = new TSMessagePackWriter(buffer);
                package.ToBuffer(ref writer);
                return  writer.FlushAndGetArray();
            }
            finally
            {
                TSArrayPool.Return(buffer);
            }
        }
        public byte[] CreatePAT(int minBufferSize = 188)
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
        public byte[] CreatePMT(int minBufferSize = 188)
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
        public byte[] CreatePES(in JT1078Package jt1078Package, int minBufferSize = 1024)
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
                package.Header.PackageType = PackageType.Data_Start;
                string key = jt1078Package.GetKey();
                if(VideoCounter.TryGetValue(key,out byte counter))
                {
                    if (counter > 0xf)
                    {
                        counter = 0; 
                    }
                }
                else
                {
                    VideoCounter.Add(key, counter);
                }
                package.Header.ContinuityCounter = counter;
                counter++;
                package.Header.PayloadUnitStartIndicator = 1;
                package.Header.Adaptation = new TS_AdaptationInfo();
                package.Payload = new PES_Package();
                package.Payload.StreamId = 0xe0;
                package.Payload.PESPacketLength = 0;
                //PESStartCode + StreamId+ Flag1 + PTS_DTS_Flag + PESPacketLength
                //3 + 1 + 1 + 1 + 2
                totalLength += (3+1+1+1+2);
                package.Payload.PTS_DTS_Flag = PTS_DTS_Flags.all;
                long timestamp= (long)jt1078Package.Timestamp;
                if (jt1078Package.Label3.DataType== JT1078DataType.视频I帧)
                {
                    //ts header adaptation
                    //PCRIncluded + Timestamp
                    //1 + 6
                    totalLength += (1 + 6);
                    package.Header.Adaptation.PCRIncluded = PCRInclude.包含;
                    package.Header.Adaptation.Timestamp = timestamp * H264DefaultHZ;
                    package.Payload.DTS = timestamp * H264DefaultHZ;
                    package.Payload.PTS = timestamp * H264DefaultHZ;
                }
                else if(jt1078Package.Label3.DataType == JT1078DataType.视频P帧)
                {
                    //ts header adaptation
                    //PCRIncluded 
                    //1
                    totalLength += 1;
                    package.Header.Adaptation.PCRIncluded = PCRInclude.不包含;
                    package.Payload.DTS = timestamp * H264DefaultHZ;
                    package.Payload.PTS = timestamp * H264DefaultHZ;
                }
                //Flag1 + PTS_DTS_Flag + DTS + PTS
                //1 + 1 + 5 + 5 = 12
                totalLength += 12;
                totalLength += TSConstants.FiexdESPackageHeaderLength;
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
                    while (index!= jt1078Package.Bodies.Length)
                    {
                        if (counter > 0xf)
                        {
                            counter = 0;
                        }
                        int segmentFullSize = jt1078Package.Bodies.Length - index;
                        if (segmentFullSize >= FiexdSegmentPESLength)
                        {
                            CreateSegmentPES(ref messagePackReader, dataReader.Slice(index, FiexdSegmentPESLength).ToArray(), counter);
                            index += FiexdSegmentPESLength;
                        }
                        else
                        {
                            var nalu = dataReader.Slice(index, segmentFullSize).ToArray();
                            //当等于183字节的时候
                            //12698D08E8DBDBCDF6C6FA19DD88490E908D687D1755BE87DF82754BE2D245270569310B3030A4904DF1883ED09A68CA1C79BC4B97642B5BC095A55E56868D05370D3BC8B7B60B4642A486B6852656C01FFADACEF4BD4320E8BE9C54D44177A433CC37493FA1D8ACD0164E92454D03B6EC0617B133AEF43B599BF85632AB9B5FF671F0DDAA07E8F279F5639BA88E3FFFFCE1D3351FAF43DF23BCEB7E3B2CAB3EABAED23B25097B7F51FF38E8D0EBAB589CEC42B0440EB8
                            //if (jt1078Package.Label3.DataType == JT1078DataType.视频P帧)
                            //{
                            //    string hex = dataReader.Slice(index, segmentFullSize).ToArray().ToHexString();
                            //}
                            CreateSegmentPES(ref messagePackReader, dataReader.Slice(index, segmentFullSize).ToArray(), counter);
                            index += segmentFullSize;
                        }
                        counter++;
                    }
                }
                VideoCounter[key]= counter;
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
            //AdaptationLengthPosition + PCRIncluded
            //1 + 1
            if (nalu.Length < (FiexdSegmentPESLength))
            {
                int size = FiexdSegmentPESLength - 1 - 1 - nalu.Length;
                package.Header.PackageType = PackageType.Data_End;
                if (size < 0)
                {
                    // nalu剩余183字节的时候
                    // 头部4个字节 + 自适应域长度1字节（0）+183 =188
                    package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
                }
                else
                {
                    package.Header.Adaptation = new TS_AdaptationInfo();
                    package.Header.Adaptation.PCRIncluded = PCRInclude.不包含;
                    package.Header.Adaptation.FillSize = (byte)(size);
                    package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
                }
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
