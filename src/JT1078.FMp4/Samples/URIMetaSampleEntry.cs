using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public class URIMetaSampleEntry : SampleEntry
    {
        public URIMetaSampleEntry() : base("urim")
        {
        }
        public URIBox TheLabel { get; set; }
        public URIInitBox Init { get; set; }
        public MPEG4BitRateBox BitRateBox { get; set; }
    }
}
