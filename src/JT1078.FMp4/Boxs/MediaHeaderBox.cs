using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MediaHeaderBox : FullBox
    {
        public MediaHeaderBox(byte version, uint flags=0) : base("mdhd", version, flags)
        {
        }
        public uint CreationTime { get; set; }
        public uint ModificationTime { get; set; }
        public uint Timescale { get; set; }
        public uint Duration { get; set; } = 1;
        //public bool Pad { get; set; }
        /// <summary>
        /// ISO-639-2/T language code
        /// und-undetermined
        /// </summary>
        public string Language { get; set; }
        public ushort PreDefined { get; set; }
    }
}
