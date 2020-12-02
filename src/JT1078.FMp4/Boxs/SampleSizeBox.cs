using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stsz
    /// </summary>
    public class SampleSizeBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stsz
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SampleSizeBox(byte version=0, uint flags=0) : base("stsz", version, flags)
        {
        }

        public uint SampleSize { get; set; }
        public uint SampleCount { get; set; }
        /// <summary>
        ///  if (sample_size==0)
        ///  length:sample_count
        /// </summary>
        public List<uint> EntrySize { get; set; }
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(SampleSize);
            if (EntrySize != null && EntrySize.Count > 0)
            {
                writer.WriteUInt32((uint)EntrySize.Count);
                foreach (var item in EntrySize)
                {
                    writer.WriteUInt32(item);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
        }
    }
}
