using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// ctts
    /// </summary>
    public class CompositionOffsetBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// ctts
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public CompositionOffsetBox(byte version=0, uint flags=0) : base("ctts", version, flags)
        {
        }
        public uint EntryCount { get; set; }

        public List<CompositionOffsetInfo> CompositionOffsetInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(CompositionOffsetInfos!=null && CompositionOffsetInfos.Count > 0)
            {
                writer.WriteUInt32((uint)CompositionOffsetInfos.Count);
                foreach(var item in CompositionOffsetInfos)
                {
                    if (Version == 0)
                    {
                        writer.WriteUInt32(item.SampleCount);
                        writer.WriteUInt32(item.SampleOffset);
                    }
                    else
                    {
                        writer.WriteUInt32(item.SampleCount);
                        writer.WriteInt32(item.SignedSampleOffset);
                    }
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
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
