using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// uriI
    /// </summary>
    public class URIInitBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// uriI
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public URIInitBox(byte version=0, uint flags=0) : base("uriI", version, flags)
        {
        }

        public byte[] UriInitializationData { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(UriInitializationData!=null && UriInitializationData.Length>0)
            {
                foreach(var item in UriInitializationData)
                {
                    writer.WriteByte(item);
                }
            }
            End(ref writer);
        }
    }
}
