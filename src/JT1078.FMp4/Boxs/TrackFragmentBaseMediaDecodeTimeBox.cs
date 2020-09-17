using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackFragmentBaseMediaDecodeTimeBox : FullBox
    {
        public TrackFragmentBaseMediaDecodeTimeBox(byte version, uint flags=0) : base("tfdt", version, flags)
        {
        }
        public uint BaseMediaDecodeTime { get; set; }
    }
}
