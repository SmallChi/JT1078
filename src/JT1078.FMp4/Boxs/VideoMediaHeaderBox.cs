using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// vmhd
    /// </summary>
    public class VideoMediaHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// vmhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public VideoMediaHeaderBox(byte version = 0, uint flags = 1) : base("vmhd", version, flags)
        {
        }

        public ushort GraphicsMode { get; set; }
        public ushort Red { get; set; }
        public ushort Green { get; set; }
        public ushort Blue { get; set; }
        public ushort[] OpColor
        {
            get
            {
                return new ushort[] {
                    Red,Green,Blue
                };
            }
        }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt16(GraphicsMode);
            foreach(var item in OpColor)
            {
                writer.WriteUInt16(item);
            }
            End(ref writer);
        }
    }
}
