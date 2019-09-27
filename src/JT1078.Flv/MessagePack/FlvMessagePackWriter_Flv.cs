using JT1078.Flv.Enums;
using JT1078.Flv.Metadata;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.MessagePack
{
    ref partial struct FlvMessagePackWriter
    {
        public void WriteFlvBody(FlvBody body)
        {
            WriteUInt32(body.PreviousTagSize);
            if (body.Tag != null)
            {
                WriteFlvTag(body.Tag);
            }
        }

        public void WriteFlvTag(FlvTags tag)
        {
            WriteByte((byte)tag.Type);
            Skip(3, out int DataSizePosition);
            WriteUInt24(tag.Timestamp);
            WriteByte(tag.TimestampExt);
            WriteUInt24(tag.StreamId);
            switch (tag.Type)
            {
                case TagType.Video:
                    //VideoTag
                    WriteVideoTags(tag.VideoTagsData);
                    break;
                case TagType.ScriptData:
                    //DataTags
                    //flv Amf0
                    WriteAmf0();
                    //flv Amf3
                    WriteAmf3(tag.DataTagsData);
                    break;
                case TagType.Audio:
                    //VIDEODATA 
                    break;
            }
            WriteInt24Return(GetCurrentPosition() - DataSizePosition -3-7, DataSizePosition);
        }

        public void WriteUInt24(uint value)
        {
            var span = writer.Free;
            span[0] = (byte)(value >> 16);
            span[1] = (byte)(value >> 8);
            span[2] = (byte)value;
            writer.Advance(3);
        }

        public void WriteInt24Return(int value, int position)
        {
            var span = writer.Written.Slice(position, 3);
            span[0] = (byte)(value >> 16);
            span[1] = (byte)(value >> 8);
            span[2] = (byte)value;
        }

        public void WriteVideoTags(VideoTags videoTags)
        {
            WriteByte((byte)((byte)videoTags.FrameType | (byte)videoTags.CodecId));
#warning 只处理H.264媒体数据
            if (videoTags.CodecId== CodecId.AvcVideoPacke)
            {
                WriteAvcVideoPacke(videoTags.VideoData);
            }
        }

        public void WriteAvcVideoPacke(AvcVideoPacke videoPacke)
        {
            WriteByte((byte)videoPacke.AvcPacketType);
            if (videoPacke.AvcPacketType== AvcPacketType.SequenceHeader)
            {
                //videoPacke.CompositionTime = 0;
                WriteUInt24(0);
                //AVCDecoderConfigurationRecord
                WriteAVCDecoderConfigurationRecord(videoPacke.AVCDecoderConfiguration);
            }
            else if(videoPacke.AvcPacketType == AvcPacketType.Raw)
            {
                WriteUInt24(videoPacke.CompositionTime);
                //One or more NALUs
                //WriteArray(new byte[] {0,0,0,1});
                WriteInt32(videoPacke.Data.Length);
                WriteArray(videoPacke.Data);
            }
            else
            {
                videoPacke.CompositionTime = 0;
                WriteUInt24(videoPacke.CompositionTime);
                //Empty
            }
        }

        public void WriteAVCDecoderConfigurationRecord(AVCDecoderConfigurationRecord configurationRecord)
        {
            WriteByte(configurationRecord.ConfigurationVersion);
            WriteByte(configurationRecord.AVCProfileIndication);
            WriteByte(configurationRecord.ProfileCompatibility);
            WriteByte(configurationRecord.AVCLevelIndication);
            //reserved(6bits)+LengthSizeMinusOne(2bits)
            WriteByte(0xFF);
            WriteByte((byte)configurationRecord.NumOfSequenceParameterSets);

            WriteUInt16((ushort)(configurationRecord.SPSBuffer.Length));

            //WriteUInt16((ushort)(configurationRecord.SPSBuffer.Length + 4));
            //WriteArray(new byte[] { 0, 0, 0, 1 });

            WriteArray(configurationRecord.SPSBuffer);
            WriteByte(configurationRecord.NumOfPictureParameterSets);

            WriteUInt16((ushort)(configurationRecord.PPSBuffer.Length));

            //WriteUInt16((ushort)(configurationRecord.PPSBuffer.Length+4));
            //WriteArray(new byte[] { 0, 0, 0, 1 });
            WriteArray(configurationRecord.PPSBuffer);
        }
    }
}
