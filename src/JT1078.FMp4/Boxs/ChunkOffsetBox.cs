using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ChunkOffsetBox : FullBox
    {
        public ChunkOffsetBox( byte version=0, uint flags=0) : base("stco", version, flags)
        {
        }

        public uint EntryCount { get; set; }
        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<uint> ChunkOffset { get; set; }
    }
}
