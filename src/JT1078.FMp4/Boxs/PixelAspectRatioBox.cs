using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// pasp
    /// </summary>
    public class PixelAspectRatioBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// pasp
        /// </summary>
        public PixelAspectRatioBox() : base("pasp")
        {
        }
        public uint HSpacing { get; set; }
        public uint VSpacing { get; set; }
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            writer.WriteUInt32(HSpacing);
            writer.WriteUInt32(VSpacing);
            End(ref writer);
        }
    }
}
