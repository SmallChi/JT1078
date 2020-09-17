using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public abstract class VisualSampleEntry : SampleEntry
    {
        public VisualSampleEntry(string boxType) : base(boxType)
        {
        }

        public ushort PreDefined1 { get; set; }
        public ushort Reserved1 { get; set; }
        public uint[] PreDefined2 { get; set; } = new uint[3];
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public uint HorizreSolution { get; set; }= 0x00480000;
        public uint VertreSolution { get; set; }= 0x00480000;
        public uint Reserved3{ get; set; }
        public ushort FrameCount { get; set; } = 1;
        public string CompressorName { get; set; }
        public ushort Depth { get; set; } = 0x0018;
        public short PreDefined3 { get; set; } = 0x1111;
        public CleanApertureBox Clap { get; set; }
        public PixelAspectRatioBox Pasp { get; set; }
    }
}
