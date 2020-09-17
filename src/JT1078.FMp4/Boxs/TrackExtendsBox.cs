using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackExtendsBox : FullBox
    {
        public TrackExtendsBox(byte version=0, uint flags=0) : base("trex", version, flags)
        {
        }

        public uint TrackID { get; set; }
        public uint DefaultSampleDescriptionIndex { get; set; }
        public uint DefaultSampleDuration { get; set; }
        public uint DefaultSampleSize { get; set; }
        public uint DefaultSampleFlags { get; set; }
    }
}
