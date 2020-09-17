using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieHeaderBox : FullBox
    {
        public MovieHeaderBox(byte version, uint flags=0) : base("mvhd", version, flags)
        {
        }
        public uint CreationTime { get; set; } 
        public uint ModificationTime { get; set; }
        public uint Timescale { get; set;}
        public uint Duration { get; set; }
        public int Rate { get; set; } = 0x00010000;
        public short Volume { get; set; } = 0x0100;
        public byte[] Reserved1 { get; set; } = new byte[2];
        public uint[] Reserved2 { get; set; } = new uint[2];
        public int[] Matrix { get; set; }=new int [9]{ 0x00010000, 0, 0, 0, 0x00010000, 0, 0, 0, 0x40000000 };
        public byte[] PreDefined { get; set; } = new byte[24];
        public uint NextTrackID { get; set; }= uint.MaxValue;
    }
}
