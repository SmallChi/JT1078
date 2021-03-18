using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mfro
    /// </summary>
    public class MovieFragmentRandomAccessOffsetBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mfro
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public MovieFragmentRandomAccessOffsetBox(byte version, uint flags=0) : base("mfro", version, flags)
        {
        }
        /// <summary>
        /// mfra 盒子大小
        /// </summary>
        public uint MfraSize { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(MfraSize);
            End(ref writer);
        }
    }
}
