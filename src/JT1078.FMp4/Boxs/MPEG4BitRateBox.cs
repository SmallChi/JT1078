using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// btrt
    /// </summary>
    public class MPEG4BitRateBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// btrt
        /// </summary>
        public MPEG4BitRateBox() : base("btrt")
        {
        }

        public uint BufferSizeDB { get; set; }
        public uint MaxBitRate { get; set; }
        public uint AvgBitRate { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            writer.WriteUInt32(BufferSizeDB);
            writer.WriteUInt32(MaxBitRate);
            writer.WriteUInt32(AvgBitRate);
            End(ref writer);
        }
    }
}
