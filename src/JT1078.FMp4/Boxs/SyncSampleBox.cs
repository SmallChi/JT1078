using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SyncSampleBox : FullBox
    {
        public SyncSampleBox(byte version=0, uint flags=0) : base("stss", version, flags)
        {
        }

        public uint EntryCount { get; set; }

        public List<uint> SampleNumber { get; set; }
    }
}
