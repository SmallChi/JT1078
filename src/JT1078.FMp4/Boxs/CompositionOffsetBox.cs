using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CompositionOffsetBox : FullBox, IFMp4MessagePackFormatter
    {
        public CompositionOffsetBox(byte version=0, uint flags=0) : base("ctts", version, flags)
        {
        }
        public uint EntryCount { get; set; }

        public List<CompositionOffsetInfo> CompositionOffsetInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            throw new NotImplementedException();
        }

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
