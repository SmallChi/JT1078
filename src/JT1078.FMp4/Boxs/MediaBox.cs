using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mdia
    /// </summary>
    public class MediaBox : Mp4Box
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
    }
}
