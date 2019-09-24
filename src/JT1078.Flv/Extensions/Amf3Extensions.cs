using JT1078.Flv.Metadata;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JT1078.Flv.Extensions
{
    public static  class Amf3Extensions
    {
        public static void WriteDouble(this IAmf3Metadata metadata, Span<byte> value)
        {
            var flvBuffer = BitConverter.GetBytes((double)metadata.Value).AsSpan();
            flvBuffer.Reverse();
            flvBuffer.CopyTo(value);
        }
    }
}
