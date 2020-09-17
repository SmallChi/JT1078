using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public class XMLMetaDataSampleEntry : SampleEntry
    {
        public XMLMetaDataSampleEntry() : base("metx")
        {
        }

        public string ContentEncoding { get; set; }
        public string Namespace { get; set; }
        public string SchemaLocation { get; set; }
        public MPEG4BitRateBox BitRateBox { get; set; }
    }
}
