using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mdhd
    /// </summary>
    public class MediaHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mdhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public MediaHeaderBox(byte version=0, uint flags=0) : base("mdhd", version, flags)
        {
        }
        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public uint Timescale { get; set; }
        public ulong Duration { get; set; } = 1;
        //public bool Pad { get; set; }
        /// <summary>
        /// ISO-639-2/T language code
        /// ref:doc\fmp4\ISO Language Codes.txt
        /// und-undetermined
        /// </summary>
        public string Language { get; set; } = "und";
        public ushort PreDefined { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (Version == 1)
            {
                writer.WriteUInt64(CreationTime);
                writer.WriteUInt64(ModificationTime);
                writer.WriteUInt32(Timescale);
                writer.WriteUInt64(Duration);
            }
            else
            {
                writer.WriteUInt32((uint)CreationTime);
                writer.WriteUInt32((uint)ModificationTime);
                writer.WriteUInt32(Timescale);
                writer.WriteUInt32((uint)Duration);
            }
            writer.WriteIso639(Language);
            writer.WriteUInt16(PreDefined);
            End(ref writer);
        }
    }
}
