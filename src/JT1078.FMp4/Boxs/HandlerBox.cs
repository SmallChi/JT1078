using JT1078.FMp4.Enums;
using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// hdlr
    /// </summary>
    public class HandlerBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// hdlr
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public HandlerBox(byte version=0, uint flags=0) : base("hdlr", version, flags)
        {
        }

        public uint PreDefined { get; set; }
        public HandlerType HandlerType { get; set; }
        public uint[] Reserved { get; set; } = new uint[3];
        public string Name { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(PreDefined);
            if(HandlerType== HandlerType.none)
            {
                writer.WriteASCII("null");
            }
            else
            {
                writer.WriteASCII(HandlerType.ToString());
            }
            foreach(var r in Reserved)
            {
                writer.WriteUInt32(r);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                writer.WriteUTF8(Name);
                writer.WriteUTF8("\0");
            }
            else
            {
                writer.WriteUTF8("\0");
            }
            End(ref writer);
        }
    }
}
