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
        private static readonly byte[] VideoFlvHeaderBuffer;
        private const uint PreviousTagSizeFixedLength = 4;
        private static readonly ConcurrentDictionary<string, uint> PreviousTagSizeDict;
        private static readonly ConcurrentDictionary<string, bool> FrameInitDict;
        private static readonly ConcurrentDictionary<string, SPSInfo> VideoSPSDict;
        private static readonly Flv.H264.H264Decoder H264Decoder;
        static FlvEncoder()
        {
            FlvHeader VideoFlvHeader = new FlvHeader(true, false);
            VideoFlvHeaderBuffer = VideoFlvHeader.ToArray().ToArray();
            PreviousTagSizeDict = new ConcurrentDictionary<string, uint>();
            FrameInitDict = new ConcurrentDictionary<string, bool>();
            VideoSPSDict = new ConcurrentDictionary<string, SPSInfo>();
            H264Decoder = new Flv.H264.H264Decoder();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sps">NalUnitType->7</param>
        /// <param name="pps">NalUnitType->8</param>
        /// <returns></returns>
        public byte[] CreateFlvFirstFrame(H264NALU sps, H264NALU pps,bool isSendFlvHeader=true,uint previousTagSize=0)
        {
            byte[] buffer = FlvArrayPool.Rent(65535);
            int currentMarkPosition = 0;
            int nextMarkPosition = 0;
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                if (isSendFlvHeader)
                {
                    //flv header
                    flvMessagePackWriter.WriteArray(VideoFlvHeaderBuffer);
                }
                //SPS -> 7
                ExpGolombReader h264GolombReader = new ExpGolombReader(sps.RawData);
                var spsInfo = h264GolombReader.ReadSPS();

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
                aVCDecoderConfigurationRecord.PPSBuffer = pps.RawData;
                aVCDecoderConfigurationRecord.SPSBuffer = sps.RawData;

                currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                CreateVideoTag0Frame(ref flvMessagePackWriter, scriptTagFramePreviousTagSize, aVCDecoderConfigurationRecord);
                nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

                //flv body video tag 0
                uint videoTag0PreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                //cache PreviousTagSize
                PreviousTagSizeDict.AddOrUpdate(sps.GetKey(), videoTag0PreviousTagSize, (a, b) => videoTag0PreviousTagSize);

                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
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

        public static uint LastFrameInterval = 0;

        private void CreateVideoTagOtherFrame(ref FlvMessagePackWriter flvMessagePackWriter, uint previousTagSize, H264NALU nALU)
        {
            //flv body PreviousTagSize
            flvMessagePackWriter.WriteUInt32(previousTagSize);
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
 
            LastFrameInterval += nALU.LastFrameInterval;
            flvTags.Timestamp = LastFrameInterval;
            if (nALU.NALUHeader.NalUnitType == 5)
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
                H264NALU sps = nALUs.FirstOrDefault(f => f.NALUHeader.NalUnitType == 7);
                H264NALU pps = nALUs.FirstOrDefault(f => f.NALUHeader.NalUnitType == 8);
                H264NALU sei = nALUs.FirstOrDefault(f => f.NALUHeader.NalUnitType == 6);
                if (sps!=null && pps != null)
                {
                    string key = sps.GetKey();
                    ExpGolombReader h264GolombReader = new ExpGolombReader(sps.RawData);
                    var spsInfo = h264GolombReader.ReadSPS();
                    if(VideoSPSDict.TryGetValue(key,out var spsInfoCache))
                    {
                        if(spsInfoCache.height!= spsInfo.height && spsInfoCache.width!= spsInfo.width)
                        {
                            uint previousTagSize = 0;
                            PreviousTagSizeDict.TryGetValue(key, out previousTagSize);
                            flvMessagePackWriter.WriteArray(CreateFlvFirstFrame(sps, pps,false, previousTagSize));
                            VideoSPSDict.TryUpdate(key, spsInfo, spsInfo);
                        }
                    }
                    else
                    {
                        flvMessagePackWriter.WriteArray(CreateFlvFirstFrame(sps, pps));
                        VideoSPSDict.TryAdd(key, spsInfo);
                    }
                }
                foreach (var naln in nALUs.Where(w=> w.NALUHeader.NalUnitType != 7 && w.NALUHeader.NalUnitType != 8 && w.NALUHeader.NalUnitType != 6))
                {
                    string key = naln.GetKey();
                    if (PreviousTagSizeDict.TryGetValue(key, out uint previousTagSize))
                    {
                        currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                        CreateVideoTagOtherFrame(ref flvMessagePackWriter, previousTagSize, naln);
                        nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                        uint tmpPreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                        PreviousTagSizeDict.TryUpdate(key, tmpPreviousTagSize, previousTagSize);
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
