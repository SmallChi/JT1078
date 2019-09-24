using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;

namespace JT1078.Flv.Metadata
{
    public class Amf3Metadata_FrameRate : IAmf3Metadata
    {
        public ushort FieldNameLength { get; set; }
        public string FieldName { get; set; } = "framerate";
        public byte DataType { get; set; } = 0x00;
        public object Value { get; set; }

        public ReadOnlySpan<byte> ToBuffer()
        {
            Span<byte> tmp = new byte[4+9+1+8];
            var b1 = Encoding.ASCII.GetBytes(FieldName);
            BinaryPrimitives.WriteUInt16BigEndian(tmp, (ushort)b1.Length);
            b1.CopyTo(tmp.Slice(4));
            tmp[11] = DataType;
            BinaryPrimitives.WriteInt64BigEndian(tmp.Slice(12), (long)Value);
            return tmp;
        }
    }
}
