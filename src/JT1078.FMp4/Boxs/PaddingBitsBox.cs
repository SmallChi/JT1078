using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class PaddingBitsBox : FullBox
    {
        public PaddingBitsBox(byte version=0, uint flags=0) : base("padb", version, flags)
        {
        }

        public uint SampleCount { get; set; }

        public List<PaddingBitsInfo> PaddingBitsInfos { get; set; }

        public class PaddingBitsInfo
        {
            public bool Reserved1 { get; set; }
            public byte Pad1 { get; set; }
            public bool Reserved2 { get; set; }
            public byte Pad2 { get; set; }
        }
    }
}
