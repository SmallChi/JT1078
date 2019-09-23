using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public static class Amf0
    {
        public readonly static byte[] Buffer;

        public readonly static byte[] FixedData = new byte[] { 0x6F, 0x6E, 0x4D, 0x65, 0x74, 0x61, 0x44, 0x61, 0x74, 0x61 };
        static Amf0()
        {
            Buffer = new byte[13];
            Buffer[0] = 0x02;
            Buffer[1] = 0;
            Buffer[2] = 10;
            Array.Copy(FixedData, 0, Buffer, 3, 10);
        }
    }
}
