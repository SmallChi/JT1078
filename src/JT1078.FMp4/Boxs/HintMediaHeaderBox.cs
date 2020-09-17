using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class HintMediaHeaderBox : FullBox
    {
        public HintMediaHeaderBox(byte version=0, uint flags=0) : base("hmhd", version, flags)
        {
        }

        public ushort MaxPDUSize { get; set; }
        public ushort AvgPDUSize { get; set; }
        public ushort MaxBitRate { get; set; }
        public ushort AvgBitRate { get; set; }
        public ushort Reserved { get; set; }
    }
}
