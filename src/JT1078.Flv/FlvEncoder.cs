using JT1078.Flv.Enums;
using JT1078.Flv.H264;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Flv
{
    public class FlvEncoder
    {
        private static readonly FlvHeader VideoFlvHeader = new FlvHeader(true, false);
        private static readonly H264Decoder h264Decoder = new H264Decoder();
        public byte[] FlvFirstFrame(List<H264NALU> nALUs)
        {
            byte[] buffer = FlvArrayPool.Rent(10240);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv header
                flvMessagePackWriter.WriteArray(VideoFlvHeader.ToArray());
                //SPS -> 7
                var spsNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 7);
                spsNALU.RawData = h264Decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
                ExpGolombReader h264GolombReader = new ExpGolombReader(spsNALU.RawData);
                var spsInfo = h264GolombReader.ReadSPS();
                //PPS -> 8
                var ppsNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 8);
                //IDR -> 5  关键帧
                var idrNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 5);
                //SEI -> 6  
                //var seiNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 6);
                //flv body script tag
                var scriptTag = CreateScriptTagFrame(spsInfo.width, spsInfo.height);
                //flv body video tag
                AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord();
                aVCDecoderConfigurationRecord.AVCProfileIndication = spsInfo.profileIdc;
                aVCDecoderConfigurationRecord.ProfileCompatibility =(byte)spsInfo.profileCompat;
                aVCDecoderConfigurationRecord.AVCLevelIndication = spsInfo.levelIdc;
                aVCDecoderConfigurationRecord.NumOfPictureParameterSets = 1;
                aVCDecoderConfigurationRecord.PPSBuffer = spsNALU.RawData;
                aVCDecoderConfigurationRecord.SPSBuffer = ppsNALU.RawData;
                var videoTag = CreateVideoTag0Frame((uint)scriptTag.Length, aVCDecoderConfigurationRecord);


                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        public byte[] CreateScriptTagFrame(int width,int height)
        {
            byte[] buffer = FlvArrayPool.Rent(1024);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body PreviousTagSize awalys 0
                flvMessagePackWriter.WriteUInt32(0);
                //flv body script tag
                //flv body tag header
                FlvTags flvTags = new FlvTags();
                flvTags.Type = TagType.ScriptData;
                flvTags.Timestamp = 0;
                flvTags.TimestampExt = 0;
                flvTags.StreamId = 0;
                //flv body tag body
                flvTags.DataTagsData = new Amf3();
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
                    Value = 25d
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

        public byte[] CreateVideoTag0Frame(uint previousTagSize,AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord)
        {
            byte[] buffer = FlvArrayPool.Rent(1024);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
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
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        public byte[] CreateVideoTagOtherFrame(uint previousTagSize, H264NALU nALU)
        {
            byte[] buffer = FlvArrayPool.Rent(2048);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body PreviousTagSize ScriptTag
                flvMessagePackWriter.WriteUInt32(previousTagSize);
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags();
                flvTags.Type = TagType.Video;
                flvTags.Timestamp = nALU.LastIFrameInterval;
                flvTags.TimestampExt = 0;
                flvTags.StreamId = 0;
                //flv body tag body
                flvTags.VideoTagsData = new VideoTags();
                flvTags.VideoTagsData.FrameType = FrameType.InterFrame;
                flvTags.VideoTagsData.VideoData = new AvcVideoPacke();
                flvTags.VideoTagsData.VideoData.AvcPacketType = AvcPacketType.Raw;
                flvTags.VideoTagsData.VideoData.CompositionTime = nALU.LastIFrameInterval;
                flvTags.VideoTagsData.VideoData.Data = nALU.RawData;
                flvMessagePackWriter.WriteFlvTag(flvTags);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        public byte[] FlvOtherFrame()
        {
            byte[] buffer = FlvArrayPool.Rent(10240);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);

#warning 目前只支持视频
                // NalUnitType == 1

                //flv body
                //flv body PreviousTagSize

                //flv body tag

                //flv body tag header

                //flv body tag body

                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
    }
}
