using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleToChunkBox : FullBox, IFMp4MessagePackFormatter
    {
        public SampleToChunkBox(byte version=0, uint flags=0) : base("stsc", version, flags)
        {
        }
        public uint EntryCount { get; set; }
        public List<SampleToChunkInfo> SampleToChunkInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            throw new NotImplementedException();
        }

        public class SampleToChunkInfo
        {
            public uint FirstChunk { get; set; }
            public uint SamplesPerChunk { get; set; }
            public uint SampleDescriptionIindex { get; set; }
        }
    }
}
