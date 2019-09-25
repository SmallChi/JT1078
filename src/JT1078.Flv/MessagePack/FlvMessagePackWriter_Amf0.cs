using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.MessagePack
{
    ref partial struct FlvMessagePackWriter
    {
        private readonly static byte[] FixedAmf0Data = new byte[] { 0x6F, 0x6E, 0x4D, 0x65, 0x74, 0x61, 0x44, 0x61, 0x74, 0x61 };
        public void WriteAmf0()
        {
            var span = writer.Free;
            span[0] = 0x02;
            span[1] = 0;
            span[2] = 10;
            FixedAmf0Data.CopyTo(span.Slice(3));
            writer.Advance(13);
        }
    }
}
