using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mfhd
    /// </summary>
    public class MovieFragmentHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mfhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public MovieFragmentHeaderBox(byte version=0, uint flags=0) : base("mfhd", version, flags)
        {
        }

        public uint SequenceNumber { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(SequenceNumber);
            End(ref writer);
        }
    }
}
