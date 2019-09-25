using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Metadata
{
    public class Amf3Metadata_VideoCodecId : IAmf3Metadata
    {
        public ushort FieldNameLength { get; set; }
        public string FieldName { get; set; } = "videocodecid";
        public byte DataType { get; set; } = 0x00;
        /// <summary>
        /// <see cref="typeof(JT1078.Flv.Enums.CodecId.AvcVideoPacke)"/>
        /// </summary>
        public object Value { get; set; }

        public ReadOnlySpan<byte> ToBuffer()
        {
            Span<byte> tmp = new byte[2+12+1+8];
            var b1 = Encoding.ASCII.GetBytes(FieldName);
            BinaryPrimitives.WriteUInt16BigEndian(tmp, (ushort)b1.Length);
            b1.CopyTo(tmp.Slice(2));
            tmp[14] = DataType;
            this.WriteDouble(tmp.Slice(15));
            return tmp;
        }
    }
}
