using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Metadata
{
    public class Amf3Metadata_FileSize : IAmf3Metadata
    {
        public ushort FieldNameLength { get; set; }
        public string FieldName { get; set; } = "filesize";
        public byte DataType { get; set; } = 0x00;
        public object Value { get; set; }

        public ReadOnlySpan<byte> ToBuffer()
        {
            Span<byte> tmp = new byte[2+8+1+8];
            var b1 = Encoding.ASCII.GetBytes(FieldName);
            BinaryPrimitives.WriteUInt16BigEndian(tmp, (ushort)b1.Length);
            b1.CopyTo(tmp.Slice(2));
            tmp[10] = DataType;
            this.WriteDouble(tmp.Slice(11));
            return tmp;
        }
    }
}
