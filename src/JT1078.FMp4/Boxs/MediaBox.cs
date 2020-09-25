using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mdia
    /// </summary>
    public class MediaBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mdia
        /// </summary>
        public MediaBox() : base("mdia")
        {
        }
        /// <summary>
        /// mdhd
        /// </summary>
        public MediaHeaderBox MediaHeaderBox { get; set; }
        /// <summary>
        /// hdlr
        /// </summary>
        public HandlerBox HandlerBox { get; set; }
        /// <summary>
        /// minf
        /// </summary>
        public MediaInformationBox MediaInformationBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            MediaHeaderBox.ToBuffer(ref writer);
            HandlerBox.ToBuffer(ref writer);
            MediaInformationBox.ToBuffer(ref writer);
            End(ref writer);
        }
    }
}
