using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleToChunkBox : FullBox
    {
        public SampleToChunkBox(byte version=0, uint flags=0) : base("stsc", version, flags)
        {
        }
        public uint EntryCount { get; set; }
        public List<SampleToChunkInfo> SampleToChunkInfos { get; set; }
        public class SampleToChunkInfo
        {
            public uint FirstChunk { get; set; }
            public uint SamplesPerChunk { get; set; }
            public uint SampleDescriptionIindex { get; set; }
        }
    }
}
