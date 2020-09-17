using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class PixelAspectRatioBox : Mp4Box
    {
        public PixelAspectRatioBox() : base("pasp")
        {
        }
        public uint HSpacing { get; set; }
        public uint VSpacing { get; set; }
    }
}
