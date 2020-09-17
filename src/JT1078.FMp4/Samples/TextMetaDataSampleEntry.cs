using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public class TextMetaDataSampleEntry : SampleEntry
    {
        public TextMetaDataSampleEntry() : base("mett")
        {
        }
        public string ContentEncoding { get; set; }
        public string MimeFormat { get; set; }
        public MPEG4BitRateBox BitRateBox { get; set; }
    }
}
