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
        private static readonly  byte[] VideoFlvHeaderBuffer;
        private const uint PreviousTagSizeFixedLength = 4;
        private static readonly ConcurrentDictionary<string, uint> PreviousTagSizeDict;
        static FlvEncoder()
        {
            FlvHeader VideoFlvHeader = new FlvHeader(true, false);
            VideoFlvHeaderBuffer = VideoFlvHeader.ToArray().ToArray();
            PreviousTagSizeDict = new ConcurrentDictionary<string, uint>();
        }

        public byte[] FlvFirstFrame(List<H264NALU> nALUs)
        {
            byte[] buffer = FlvArrayPool.Rent(65535);
            int currentMarkPosition = 0;
            int nextMarkPosition = 0;
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv header
                flvMessagePackWriter.WriteArray(VideoFlvHeaderBuffer);

                //SPS -> 7
                var spsNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 7);
                ExpGolombReader h264GolombReader = new ExpGolombReader(spsNALU.RawData);
                var spsInfo = h264GolombReader.ReadSPS();
                //PPS -> 8
                var ppsNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 8);

                currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                //flv body script tag
                CreateScriptTagFrame(ref flvMessagePackWriter,spsInfo.width, spsInfo.height);
                nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

                //flv body video tag
                uint scriptTagFramePreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord();
                aVCDecoderConfigurationRecord.AVCProfileIndication = spsInfo.profileIdc;
                aVCDecoderConfigurationRecord.ProfileCompatibility =(byte)spsInfo.profileCompat;
                aVCDecoderConfigurationRecord.AVCLevelIndication = spsInfo.levelIdc;
                aVCDecoderConfigurationRecord.NumOfPictureParameterSets = 1;
                aVCDecoderConfigurationRecord.PPSBuffer = ppsNALU.RawData;
                aVCDecoderConfigurationRecord.SPSBuffer = spsNALU.RawData;

                currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                CreateVideoTag0Frame(ref flvMessagePackWriter, scriptTagFramePreviousTagSize, aVCDecoderConfigurationRecord);
                nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

                //flv body video tag 0
                uint videoTag0PreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                //IDR -> 5  关键帧
                var idrNALU = nALUs.FirstOrDefault(n => n.NALUHeader.NalUnitType == 5);

                currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                CreateVideoTagKeyFram(ref flvMessagePackWriter, videoTag0PreviousTagSize, idrNALU);
                nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();

                //cache PreviousTagSize
                uint videoTagKeyFramPreviousTagSize = (uint)(nextMarkPosition - currentMarkPosition - PreviousTagSizeFixedLength);
                PreviousTagSizeDict.AddOrUpdate(idrNALU.GetKey(), videoTagKeyFramPreviousTagSize, (a, b) => videoTagKeyFramPreviousTagSize);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
        private void CreateScriptTagFrame(ref FlvMessagePackWriter flvMessagePackWriter,int width,int height)
        {
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
        }
        private void CreateVideoTag0Frame(ref FlvMessagePackWriter flvMessagePackWriter, uint previousTagSize,AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord)
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
        private void CreateVideoTagKeyFram(ref FlvMessagePackWriter flvMessagePackWriter,  uint previousTagSize, H264NALU nALU)
        {
            //flv body PreviousTagSize
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
            flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
            flvTags.VideoTagsData.VideoData = new AvcVideoPacke();
            flvTags.VideoTagsData.VideoData.AvcPacketType = AvcPacketType.Raw;
            flvTags.VideoTagsData.VideoData.CompositionTime = 0;
            flvTags.VideoTagsData.VideoData.Data = nALU.RawData;
            flvMessagePackWriter.WriteFlvTag(flvTags);
        }
        private void CreateVideoTagOtherFrame(ref FlvMessagePackWriter flvMessagePackWriter, uint previousTagSize, H264NALU nALU)
        {
            //flv body PreviousTagSize
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
            if (nALU.NALUHeader.NalUnitType == 5)
            {
                flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
            }
            else
            {
                flvTags.VideoTagsData.FrameType = FrameType.InterFrame;
            }
            flvTags.VideoTagsData.VideoData = new AvcVideoPacke();
            flvTags.VideoTagsData.VideoData.AvcPacketType = AvcPacketType.Raw;
            flvTags.VideoTagsData.VideoData.CompositionTime = nALU.LastIFrameInterval;
            flvTags.VideoTagsData.VideoData.Data = nALU.RawData;
            flvMessagePackWriter.WriteFlvTag(flvTags);
        }
        public byte[] FlvOtherFrame(List<H264NALU> nALUs,int minimumLength=10240)
        {
            byte[] buffer = FlvArrayPool.Rent(minimumLength);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                int currentMarkPosition = 0;
                int nextMarkPosition = 0;
                foreach (var naln in nALUs)
                {
                    string key = naln.GetKey();
                    if (PreviousTagSizeDict.TryGetValue(key, out uint previousTagSize))
                    {
                        currentMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                        CreateVideoTagOtherFrame(ref flvMessagePackWriter, previousTagSize, naln);
                        nextMarkPosition = flvMessagePackWriter.GetCurrentPosition();
                        uint tmpPreviousTagSize = (uint)(nextMarkPosition- currentMarkPosition - PreviousTagSizeFixedLength);
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
