using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stco
    /// </summary>
    public class ChunkOffsetBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stco
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public ChunkOffsetBox( byte version=0, uint flags=0) : base("stco", version, flags)
        {
        }

        public uint EntryCount { get; set; }
        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<uint> ChunkOffset { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(ChunkOffset!=null && ChunkOffset.Count > 0)
            {
                writer.WriteUInt32((uint)ChunkOffset.Count);
                foreach(var item in ChunkOffset)
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
