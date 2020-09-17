using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public abstract class SampleGroupDescriptionEntry
    {
        public uint GroupingType { get; set; }

        public SampleGroupDescriptionEntry(uint groupingType)
        {
            GroupingType = groupingType;
        }
    }
}
