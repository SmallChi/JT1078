using JT1078.Flv.Extensions;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class Amf3Metadata_AudioSampleRate : IAmf3Metadata
    {
        public ushort FieldNameLength { get; set; }
        public string FieldName { get; set; } = "audiosamplerate";
        public byte DataType { get; set; } = 0x00;
        public object Value { get; set; } = 8000d;

        public ReadOnlySpan<byte> ToBuffer()
        {
            var b1 = Encoding.ASCII.GetBytes(FieldName);
            Span<byte> tmp = new byte[2 + b1.Length + 1 + 8];
            BinaryPrimitives.WriteUInt16BigEndian(tmp, (ushort)b1.Length);
            b1.CopyTo(tmp.Slice(2));
            tmp[FieldName.Length + 2] = DataType;
            this.WriteDouble(tmp.Slice(b1.Length + 3));
            return tmp;
        }
    }
}
