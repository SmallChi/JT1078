using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// uri 
    /// </summary>
    public class URIBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// uri 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public URIBox(byte version=0, uint flags=0) : base("uri ", version, flags)
        {
        }

        public string TheURI { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUTF8(TheURI ?? "\0");
            End(ref writer);
        }
    }
}
