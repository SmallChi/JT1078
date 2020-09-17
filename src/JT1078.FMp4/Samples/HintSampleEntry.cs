using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public class HintSampleEntry : SampleEntry
    {
        public HintSampleEntry(string protocol) : base(protocol)
        {
        }
        public List<byte> Data { get; set; }
    }
}
