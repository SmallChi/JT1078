using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// mett
    /// </summary>
    public class TextMetaDataSampleEntry : SampleEntry
    {
        /// <summary>
        /// mett
        /// </summary>
        public TextMetaDataSampleEntry() : base("mett")
        {
        }
        /// <summary>
        /// 
        /// optional
        /// </summary>
        public string ContentEncoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MimeFormat { get; set; }
        /// <summary>
        /// btrt
        /// optional
        /// </summary>
        public MPEG4BitRateBox BitRateBox { get; set; }

        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            if (!string.IsNullOrEmpty(ContentEncoding))
            {
                writer.WriteUTF8(ContentEncoding);
            }
            writer.WriteUTF8(MimeFormat??"\0");
            if (BitRateBox != null)
            {
                BitRateBox.ToBuffer(ref writer);
            }
        }
    }
}
