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
            if (MfraSize > 0)
            {
                //人工
                writer.WriteUInt32(MfraSize);
            }
            else
            {
                //程序自动计算
                writer.CreateMfraSizePosition();
                writer.Skip(4, out _);
            }
            End(ref writer);
        }
    }
}
