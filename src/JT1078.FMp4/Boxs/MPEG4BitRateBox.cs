using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MPEG4BitRateBox : Mp4Box
    {
        public MPEG4BitRateBox() : base("btrt")
        {
        }

        public uint BufferSizeDB { get; set; }
        public uint MaxBitRate { get; set; }
        public uint AvgBitRate { get; set; }
    }
}
