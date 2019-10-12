using JT1078.Flv.Enums;
using JT1078.Flv.H264;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Protocol;
using JT1078.Protocol.Enums;
using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT1078.Flv.Test")]
namespace JT1078.Flv
{
    public class FlvEncoder
    {
        public class FlvFrameInfo
        {
            public uint PreviousTagSize { get; set;}
            public uint LastFrameInterval { get; set; }
            public uint LastIFrameInterval { get; set; }
        }

        public static readonly byte[] VideoFlvHeaderBuffer;
        private static readonly Flv.H264.H264Decoder H264Decoder;
        private static readonly ConcurrentDictionary<string, SPSInfo> VideoSPSDict;
        private static readonly ConcurrentDictionary<string, FlvFrameInfo> FlvFrameInfoDict;
        internal static readonly ConcurrentDictionary<string, (uint PreviousTagSize,byte[] Buffer)> FlvFirstFrameCache;
        static FlvEncoder()
        {
            FlvHeader VideoFlvHeader = new FlvHeader(true, false);
            VideoFlvHeaderBuffer = VideoFlvHeader.ToArray().ToArray();
            VideoSPSDict = new ConcurrentDictionary<string, SPSInfo>(StringComparer.OrdinalIgnoreCase);
            FlvFrameInfoDict = new ConcurrentDictionary<string, FlvFrameInfo>(StringComparer.OrdinalIgnoreCase);
            FlvFirstFrameCache = new ConcurrentDictionary<string, (uint PreviousTagSize, byte[] Buffer)>(StringComparer.OrdinalIgnoreCase);
            H264Decoder = new Flv.H264.H264Decoder();
        }

        public byte[] CreateScriptTagFrame(int width, int height, double frameRate = 25d)
        {
            byte[] buffer = FlvArrayPool.Rent(1024);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body script tag
                //flv body tag header
                FlvTags flvTags = new FlvTags();
                flvTags.Type = TagType.ScriptData;
                flvTags.Timestamp = 0;
                flvTags.TimestampExt = 0;
                flvTags.StreamId = 0;
                //flv body tag body
                flvTags.DataTagsData = new Amf3();
                flvTags.DataTagsData.Amf3Metadatas = new List<IAmf3Metadata>();
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_Duration
                {
                    Value = 0d
                });
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_VideoDataRate
                {
                    Value = 0d
                });
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_VideoCodecId
                {
                    Value = 7d
                });
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_FrameRate
                {
                    Value = frameRate
                });
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_Width
                {
                    Value = width
                });
                flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_Height
                {
                    Value = height
                });
                flvMessagePackWriter.WriteFlvTag(flvTags);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
        public byte[] CreateVideoTag0Frame(byte[] spsRawData, byte[] ppsRawData, SPSInfo spsInfo)
        {
            byte[] buffer = FlvArrayPool.Rent(1024);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags();
                flvTags.Type = TagType.Video;
                flvTags.Timestamp = 0;
                flvTags.TimestampExt = 0;
                flvTags.StreamId = 0;
                //flv body tag body
                flvTags.VideoTagsData = new VideoTags();
                flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
                flvTags.VideoTagsData.VideoData = new AvcVideoPacke();
                flvTags.VideoTagsData.VideoData.AvcPacketType = AvcPacketType.SequenceHeader;
                flvTags.VideoTagsData.VideoData.CompositionTime = 0;
                AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord();
                aVCDecoderConfigurationRecord.AVCProfileIndication = spsInfo.profileIdc;
                aVCDecoderConfigurationRecord.ProfileCompatibility = (byte)spsInfo.profileCompat;
                aVCDecoderConfigurationRecord.AVCLevelIndication = spsInfo.levelIdc;
                aVCDecoderConfigurationRecord.NumOfPictureParameterSets = 1;
                aVCDecoderConfigurationRecord.PPSBuffer = ppsRawData;
                aVCDecoderConfigurationRecord.SPSBuffer = spsRawData;
                flvTags.VideoTagsData.VideoData.AVCDecoderConfiguration = aVCDecoderConfigurationRecord;
                flvMessagePackWriter.WriteFlvTag(flvTags);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
        public byte[] CreateVideoTagOtherFrame(FlvFrameInfo flvFrameInfo, H264NALU nALU)
        {
            byte[] buffer = FlvArrayPool.Rent(65535);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags();
                flvTags.Type = TagType.Video;
                flvTags.TimestampExt = 0;
                flvTags.StreamId = 0;
                //flv body tag body
                flvTags.VideoTagsData = new VideoTags();
                flvTags.VideoTagsData.VideoData = new AvcVideoPacke();
                flvTags.VideoTagsData.VideoData.CompositionTime = 0;
                flvTags.VideoTagsData.VideoData.AvcPacketType = AvcPacketType.Raw;
                if (nALU.NALUHeader.NalUnitType == 5 || nALU.DataType == JT1078DataType.视频I帧)
                {
                    flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
                    flvTags.Timestamp = flvFrameInfo.LastIFrameInterval;
                }
                else
                {
                    flvTags.Timestamp = flvFrameInfo.LastFrameInterval;
                    flvTags.VideoTagsData.FrameType = FrameType.InterFrame;
                }
#warning Timestamp时间戳没有控制好
                flvTags.Timestamp = flvFrameInfo.LastIFrameInterval;
                flvTags.VideoTagsData.VideoData.Data = nALU.RawData;
                flvMessagePackWriter.WriteFlvTag(flvTags);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        public byte[] CreateFlvFrame(List<H264NALU> nALUs,int minimumLength = 65535)
        {
            byte[] buffer = FlvArrayPool.Rent(minimumLength);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                H264NALU sps=null, pps=null;
                SPSInfo spsInfo = new SPSInfo();
                foreach (var naln in nALUs)
                {
                    string key = naln.GetKey();
                    if (sps != null && pps != null)
                    {
                        if (VideoSPSDict.TryGetValue(key, out var spsInfoCache))
                        {
                            //todo: 主次码流
                            if (spsInfoCache.height != spsInfo.height && spsInfoCache.width != spsInfo.width)
                            {
                                if (FlvFrameInfoDict.TryGetValue(key, out FlvFrameInfo flvFrameInfo))
                                {
                                    CreateFlvKeyFrame(ref flvMessagePackWriter, key, sps.RawData, pps.RawData, spsInfo);
                                    VideoSPSDict.TryUpdate(key, spsInfo, spsInfo);
                                    flvFrameInfo.LastIFrameInterval = 0;
                                    FlvFrameInfoDict.TryUpdate(key, flvFrameInfo, flvFrameInfo);
                                }
                            }
                        }
                        else
                        {
                            flvMessagePackWriter.WriteArray(VideoFlvHeaderBuffer);
                            CreateFlvKeyFrame(ref flvMessagePackWriter, key, sps.RawData, pps.RawData, spsInfo);
                            VideoSPSDict.TryAdd(key, spsInfo);
                        }
                        sps = null;
                        pps = null;
                        continue;
                    }
                    //7 8 6 5 1 1 1 1 7 8 6 5 1 1 1 1 1 7 8 6 5 1 1 1 1 1
                    switch (naln.NALUHeader.NalUnitType)
                    {
                        case 5:// IDR
                        case 1:// I/P/B
                            if (FlvFrameInfoDict.TryGetValue(key, out FlvFrameInfo  flvFrameInfo))
                            {
                                flvFrameInfo.LastIFrameInterval += naln.LastIFrameInterval;
                                flvFrameInfo.LastFrameInterval += naln.LastFrameInterval;
                                // PreviousTagSize
                                flvMessagePackWriter.WriteUInt32(flvFrameInfo.PreviousTagSize);
                                // Data Tag Frame
                                var flvFrameBuffer = CreateVideoTagOtherFrame(flvFrameInfo, naln);
                                flvMessagePackWriter.WriteArray(flvFrameBuffer);
                                flvFrameInfo.PreviousTagSize = (uint)flvFrameBuffer.Length;
                                FlvFrameInfoDict.TryUpdate(key, flvFrameInfo, flvFrameInfo);
                            }
                            break;
                        case 7:// sps
                            sps = naln;
                            var rawData = H264Decoder.DiscardEmulationPreventionBytes(naln.RawData);
                            ExpGolombReader h264GolombReader = new ExpGolombReader(rawData);
                            spsInfo = h264GolombReader.ReadSPS();    
                            break;
                        case 8:// pps
                            pps = naln;
                            break;
                        case 6://SEI
                            break;
                        default:
                            break;
                    }
                }
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
        private void CreateFlvKeyFrame(ref FlvMessagePackWriter flvMessagePackWriter, string key, byte[] spsRawData, byte[] ppsRawData, SPSInfo spsInfo)
        {
            //flv body PreviousTagSize awalys 0
            flvMessagePackWriter.WriteUInt32(0);
            //flv body script tag
            var scriptTagFrameBuffer= CreateScriptTagFrame(spsInfo.width, spsInfo.height);
            flvMessagePackWriter.WriteArray(scriptTagFrameBuffer);
            //flv script tag PreviousTagSize
            flvMessagePackWriter.WriteUInt32((uint)scriptTagFrameBuffer.Length);
            //flv body video tag
            var videoTagFrame0Buffer= CreateVideoTag0Frame(spsRawData, ppsRawData, spsInfo);
            flvMessagePackWriter.WriteArray(videoTagFrame0Buffer);
            uint videoTag0PreviousTagSize = (uint)videoTagFrame0Buffer.Length;
            //cache PreviousTagSize
            FlvFrameInfoDict.AddOrUpdate(key, new FlvFrameInfo { PreviousTagSize = videoTag0PreviousTagSize}, (a, b) => {
                b.PreviousTagSize = videoTag0PreviousTagSize;
                return b;
            });
            var buffer = flvMessagePackWriter.FlushAndGetArray();
            FlvFirstFrameCache.AddOrUpdate(key,(videoTag0PreviousTagSize, buffer),(a,b)=> {
                b.PreviousTagSize = videoTag0PreviousTagSize;
                b.Buffer = buffer;
                return b;
            });
        }
        public byte[] CreateFlvFrame(JT1078Package package,int minimumLength = 65535)
        {
            var nalus = H264Decoder.ParseNALU(package);
            if (nalus == null || nalus.Count <= 0) return default;
            return CreateFlvFrame(nalus, minimumLength);
        }
        public byte[] GetFirstFlvFrame(string key,byte[] bufferFlvFrame)
        {
            if (FlvFirstFrameCache.TryGetValue(key, out var firstBuffer))
            {
                var length = firstBuffer.Buffer.Length + bufferFlvFrame.Length;
                byte[] buffer = FlvArrayPool.Rent(length);
                try
                {
                    Span<byte> tmp = buffer;
                    firstBuffer.Buffer.CopyTo(tmp);
                    BinaryPrimitives.WriteUInt32BigEndian(bufferFlvFrame, firstBuffer.PreviousTagSize);
                    bufferFlvFrame.CopyTo(tmp.Slice(firstBuffer.Buffer.Length));
                    return tmp.Slice(0, length).ToArray();
                }
                finally
                {
                    FlvArrayPool.Return(buffer);
                }
             }
            return default;
        }
    }
}
