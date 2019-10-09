using JT1078.Flv.Enums;
using JT1078.Flv.H264;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Protocol.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Flv
{
    public class FlvEncoder
    {
        struct FlvFrameInfo
        {
            public uint PreviousTagSize { get; set;}
            public uint LastFrameInterval { get; set; }
        }
        public static readonly byte[] VideoFlvHeaderBuffer;
        private const uint PreviousTagSizeFixedLength = 4;
        private static readonly ConcurrentDictionary<string, SPSInfo> VideoSPSDict;
        private static readonly Flv.H264.H264Decoder H264Decoder;
        private static readonly ConcurrentDictionary<string, FlvFrameInfo> FlvFrameInfoDict;
        static FlvEncoder()
        {
            FlvHeader VideoFlvHeader = new FlvHeader(true, false);
            VideoFlvHeaderBuffer = VideoFlvHeader.ToArray().ToArray();
            VideoSPSDict = new ConcurrentDictionary<string, SPSInfo>();
            FlvFrameInfoDict = new ConcurrentDictionary<string, FlvFrameInfo>();
            H264Decoder = new Flv.H264.H264Decoder();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateFlvKeyFrame(ref FlvMessagePackWriter flvMessagePackWriter, string key,byte[] spsRawData, byte[] ppsRawData, SPSInfo spsInfo, uint previousTagSize = 0)
        {
            int currentMarkPosition = 0;
            int nextMarkPosition = 0;
            currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
            //flv body script tag
            CreateScriptTagFrame(ref flvMessagePackWriter, spsInfo.width, spsInfo.height, previousTagSize);
            nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

            //flv body video tag
            uint scriptTagFramePreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
            AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord();
            aVCDecoderConfigurationRecord.AVCProfileIndication = spsInfo.profileIdc;
            aVCDecoderConfigurationRecord.ProfileCompatibility = (byte)spsInfo.profileCompat;
            aVCDecoderConfigurationRecord.AVCLevelIndication = spsInfo.levelIdc;
            aVCDecoderConfigurationRecord.NumOfPictureParameterSets = 1;
            aVCDecoderConfigurationRecord.PPSBuffer = ppsRawData;
            aVCDecoderConfigurationRecord.SPSBuffer = spsRawData;

            currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
            CreateVideoTag0Frame(ref flvMessagePackWriter, scriptTagFramePreviousTagSize, aVCDecoderConfigurationRecord);
            nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

            //flv body video tag 0
            uint videoTag0PreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
            //cache PreviousTagSize
            FlvFrameInfoDict.AddOrUpdate(key, new FlvFrameInfo {  PreviousTagSize= videoTag0PreviousTagSize }, (a, b) => {
                b.PreviousTagSize = videoTag0PreviousTagSize;
                return b;
            });
        }
        private void CreateScriptTagFrame(ref FlvMessagePackWriter flvMessagePackWriter, int width, int height,uint previousTagSize, double frameRate = 25d)
        {
            //flv body PreviousTagSize awalys 0
            flvMessagePackWriter.WriteUInt32(previousTagSize);
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
        }
        private void CreateVideoTag0Frame(ref FlvMessagePackWriter flvMessagePackWriter, uint previousTagSize, AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord)
        {
            //flv body PreviousTagSize ScriptTag
            flvMessagePackWriter.WriteUInt32(previousTagSize);
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
            flvTags.VideoTagsData.VideoData.AVCDecoderConfiguration = aVCDecoderConfigurationRecord;
            flvMessagePackWriter.WriteFlvTag(flvTags);
        }
        private void CreateVideoTagOtherFrame(ref FlvMessagePackWriter flvMessagePackWriter, FlvFrameInfo flvFrameInfo, H264NALU nALU)
        {
            //flv body PreviousTagSize
            flvMessagePackWriter.WriteUInt32(flvFrameInfo.PreviousTagSize);
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
            flvTags.Timestamp = flvFrameInfo.LastFrameInterval;
            if (nALU.NALUHeader.NalUnitType == 5 || nALU.DataType== JT1078DataType.视频I帧)
            {
                flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
            }
            else
            {
                flvTags.VideoTagsData.FrameType = FrameType.InterFrame;
            }
            flvTags.VideoTagsData.VideoData.Data = nALU.RawData;
            flvMessagePackWriter.WriteFlvTag(flvTags);
        }
        public byte[] CreateFlvFrame(List<H264NALU> nALUs, int minimumLength = 65535)
        {
            byte[] buffer = FlvArrayPool.Rent(minimumLength);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                int currentMarkPosition = 0;
                int nextMarkPosition = 0;
                H264NALU sps=null, pps=null;
                SPSInfo spsInfo = new SPSInfo();
                foreach (var naln in nALUs)
                {
                    string key = naln.GetKey();
                    if (sps != null && pps != null)
                    {
                        if (VideoSPSDict.TryGetValue(key, out var spsInfoCache))
                        {
                            if (spsInfoCache.height != spsInfo.height && spsInfoCache.width != spsInfo.width)
                            {
                                if(FlvFrameInfoDict.TryGetValue(key, out FlvFrameInfo flvFrameInfo))
                                {
                                    CreateFlvKeyFrame(ref flvMessagePackWriter, key, sps.RawData, pps.RawData, spsInfo, flvFrameInfo.PreviousTagSize);
                                    VideoSPSDict.TryUpdate(key, spsInfo, spsInfo);
                                    flvFrameInfo.LastFrameInterval = 0;
                                    FlvFrameInfoDict.TryUpdate(key, flvFrameInfo, flvFrameInfo);
                                }
                            }
                        }
                        else
                        {
                            CreateFlvKeyFrame(ref flvMessagePackWriter, key, sps.RawData, pps.RawData, spsInfo, 0);
                            VideoSPSDict.TryAdd(key, spsInfo);
                        }
                    }
                    //7 8 6 5 1 1 1 1 7 8 6 5 1 1 1 1 1 7 8 6 5 1 1 1 1 1
                    switch (naln.NALUHeader.NalUnitType)
                    {
                        case 5:// IDR
                        case 1:// I/P/B
                            if (FlvFrameInfoDict.TryGetValue(key, out FlvFrameInfo  flvFrameInfo))
                            {
                                currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                                CreateVideoTagOtherFrame(ref flvMessagePackWriter, flvFrameInfo, naln);
                                nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                                uint tmpPreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                                flvFrameInfo.PreviousTagSize = tmpPreviousTagSize;
                                flvFrameInfo.LastFrameInterval += naln.LastFrameInterval;
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
    }
}
