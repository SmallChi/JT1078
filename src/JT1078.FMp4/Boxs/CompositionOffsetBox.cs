using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CompositionOffsetBox : FullBox
    {
        public CompositionOffsetBox(byte version=0, uint flags=0) : base("ctts", version, flags)
        {
        }
        public uint EntryCount { get; set; }

        public List<CompositionOffsetInfo> CompositionOffsetInfos { get; set; }

        public class CompositionOffsetInfo
        {
            public uint SampleCount { get; set; }
            public uint SampleOffset { get; set; }
            /// <summary>
            /// version == 1
            /// </summary>
            public int SignedSampleOffset { get; set; }
        }
    }
}
