using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// clap
    /// </summary>
    public class CleanApertureBox : Mp4Box,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// clap
        /// </summary>
        public CleanApertureBox() : base("clap")
        {
        }

        public uint CleanApertureWidthN { get; set; }
        public uint CleanApertureWidthD { get; set; }
        public uint CleanApertureHeightN { get; set; }
        public uint CleanApertureHeightD { get; set; }
        public uint HorizOffN { get; set; }
        public uint HorizOffD { get; set; }
        public uint VertOffN { get; set; }
        public uint VertOffD { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            writer.WriteUInt32(CleanApertureWidthN);
            writer.WriteUInt32(CleanApertureWidthD);
            writer.WriteUInt32(CleanApertureHeightN);
            writer.WriteUInt32(CleanApertureHeightD);
            writer.WriteUInt32(HorizOffN);
            writer.WriteUInt32(HorizOffD);
            writer.WriteUInt32(VertOffN);
            writer.WriteUInt32(VertOffD);
            End(ref writer);
        }
    }
}
