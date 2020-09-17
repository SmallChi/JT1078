using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public abstract class AudioSampleGroupEntry : SampleGroupDescriptionEntry
    {
        public AudioSampleGroupEntry(uint groupingType) : base(groupingType)
        {
        }
    }
}
