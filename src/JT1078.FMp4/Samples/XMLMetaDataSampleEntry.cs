using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// metx
    /// </summary>
    public class XMLMetaDataSampleEntry : SampleEntry
    {
        /// <summary>
        /// metx
        /// </summary>
        public XMLMetaDataSampleEntry() : base("metx")
        {
        }
        /// <summary>
        /// 
        /// optional
        /// </summary>
        public string ContentEncoding { get; set; }
        /// <summary>
        /// Namespace
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 
        /// optional
        /// </summary>
        public string SchemaLocation { get; set; }
        /// <summary>
        /// 
        /// optional
        /// </summary>
        public MPEG4BitRateBox BitRateBox { get; set; }

        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterSampleEntryToBuffer(ref writer);
            if (!string.IsNullOrEmpty(ContentEncoding))
            {
                writer.WriteUTF8(ContentEncoding);
            }
            writer.WriteUTF8(Namespace ?? "\0");
            if (!string.IsNullOrEmpty(SchemaLocation))
            {
                writer.WriteUTF8(SchemaLocation);
            }
            if (BitRateBox != null)
            {
                BitRateBox.ToBuffer(ref writer);
            }
            End(ref writer);
        }
    }
}
