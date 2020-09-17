using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackHeaderBox : FullBox
    {
        public TrackHeaderBox(byte version, uint flags) : base("tkhd", version, flags)
        {
        }
        public uint CreationTime { get; set; }
        public uint ModificationTime { get; set; }
        public uint TrackID { get; set; }
        public uint Reserved1 { get; set; }
        public uint Duration { get; set; }
        public uint[] Reserved2 { get; set; } = new uint[2];
        public ushort Layer { get; set; }
        public ushort AlternateGroup { get; set; }
        public ushort Volume { get; set; }
        public ushort Reserved3 { get; set; }
        public int[] Matrix { get; set; } = new int[9] { 0x00010000, 0, 0, 0, 0x00010000, 0, 0, 0, 0x40000000 };
        public uint Width { get; set; }
        public uint Height { get; set; }
    }
}
