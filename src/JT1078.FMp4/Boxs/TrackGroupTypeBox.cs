using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackGroupTypeBox : FullBox
    {
        public TrackGroupTypeBox(string boxType, byte version=0, uint flags=0) : base(boxType, version, flags)
        {
        }

        public uint TrackGroupId { get; set; }
    }
}
