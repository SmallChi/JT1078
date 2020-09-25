using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stsc
    /// </summary>
    public class SampleToChunkBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stsc
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SampleToChunkBox(byte version=0, uint flags=0) : base("stsc", version, flags)
        {
        }
        public uint EntryCount { get; set; }
        public List<SampleToChunkInfo> SampleToChunkInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(SampleToChunkInfos!=null && SampleToChunkInfos.Count > 0)
            {
                writer.WriteUInt32((uint)SampleToChunkInfos.Count);
                foreach(var item in SampleToChunkInfos)
                {
                    writer.WriteUInt32(item.FirstChunk);
                    writer.WriteUInt32(item.SamplesPerChunk);
                    writer.WriteUInt32(item.SampleDescriptionIindex);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
        }

        public class SampleToChunkInfo
        {
            public uint FirstChunk { get; set; }
            public uint SamplesPerChunk { get; set; }
            public uint SampleDescriptionIindex { get; set; }
        }
    }
}
