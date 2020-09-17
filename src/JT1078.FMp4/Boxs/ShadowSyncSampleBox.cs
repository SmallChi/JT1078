using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ShadowSyncSampleBox : FullBox
    {
        public ShadowSyncSampleBox(byte version, uint flags) : base("stsh", version, flags)
        {
        }

        public uint EntryCount { get; set; }

        public List<ShadowSyncSampleInfo> ShadowSyncSampleInfos { get; set; }

        public class ShadowSyncSampleInfo
        {
            public uint ShadowedSampleNumber { get; set; }
            public uint SyncSampleNumber { get; set; }
        }
    }
}
