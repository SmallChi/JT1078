using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ChunkLargeOffsetBox : FullBox
    {
        public ChunkLargeOffsetBox(byte version=0, uint flags=0) : base("co64", version, flags)
        {
        }
        public uint EntryCount { get; set; }
        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<ulong> ChunkOffset { get; set; }
    }
}
