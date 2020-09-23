using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// urim
    /// </summary>
    public class URIMetaSampleEntry : SampleEntry
    {
        /// <summary>
        /// urim
        /// </summary>
        public URIMetaSampleEntry() : base("urim")
        {
        }
        /// <summary>
        /// uri 
        /// </summary>
        public URIBox TheLabel { get; set; }
        /// <summary>
        /// uriI
        /// optional
        /// </summary>
        public URIInitBox Init { get; set; }
        /// <summary>
        /// btrt
        /// optional
        /// </summary>
        public MPEG4BitRateBox BitRateBox { get; set; }

        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterSampleEntryToBuffer(ref writer);
            if (TheLabel != null)
            {
                TheLabel.ToBuffer(ref writer);
            }
            if (Init != null)
            {
                Init.ToBuffer(ref writer);
            }
            if (BitRateBox != null)
            {
                BitRateBox.ToBuffer(ref writer);
            }
            End(ref writer);
        }
    }
}
