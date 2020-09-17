using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class EditListBox : FullBox
    {
        public EditListBox(byte version, uint flags=0) : base("elst", version, flags)
        {
        }
        public uint EntryCount { get; set; }

        public ulong SegmentDurationLarge { get; set; }
        public long MediaTimeLarge { get; set; }
        public uint SegmentDuration { get; set; }
        public int MediaTime { get; set; }
        public short MediaRateInteger { get; set; }
        public short MediaRateFraction { get; set; }
    }
}
