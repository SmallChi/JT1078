using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mehd
    /// </summary>
    public class MovieExtendsHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mehd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public MovieExtendsHeaderBox( byte version, uint flags=0) : base("mehd", version, flags)
        {
        }

        public uint FragmentDuration { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(FragmentDuration);
            End(ref writer);
        }
    }
}
