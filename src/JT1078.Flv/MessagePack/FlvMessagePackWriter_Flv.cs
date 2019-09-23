using JT1078.Flv.Enums;
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

        public void WriteFlvTag(FlvTag tag)
        {
            WriteByte(tag.Type);
            Skip(3, out int DataSizePosition);
            WriteUInt24(tag.Timestamp);
            WriteByte(tag.TimestampExt);
            WriteUInt24(tag.StreamId);
            switch ((TagType)tag.Type)
            {
                case TagType.Video:

                    break;
                case TagType.ScriptData:

                    //flv Amf0
                    WriteAmf0();
                    //flv Amf3
                    break;
                case TagType.Audio:
                    break;
            }
            WriteInt32Return(GetCurrentPosition() - DataSizePosition - 3, DataSizePosition);
        }

        public void WriteUInt24(uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Free, value);
            writer.Advance(3);
        }

        public void WriteInt32Return(int value, int position)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Written.Slice(position, 3), value);
        }

    }
}
