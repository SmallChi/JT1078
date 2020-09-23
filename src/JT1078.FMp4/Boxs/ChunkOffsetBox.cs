using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ChunkOffsetBox : FullBox, IFMp4MessagePackFormatter
    {
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
            throw new NotImplementedException();
        }
    }
}
