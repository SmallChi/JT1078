using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CleanApertureBox : Mp4Box
    {
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
    }
}
