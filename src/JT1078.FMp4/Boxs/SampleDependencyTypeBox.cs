using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleDependencyTypeBox : FullBox
    {
        public SampleDependencyTypeBox(byte version=0, uint flags=0) : base("sdtp", version, flags)
        {
        }
        /// <summary>
        /// is taken from the sample_count in the Sample Size Box ('stsz') or Compact Sample Size Box(‘stz2’).
        /// </summary>
        public List<SampleDependencyType> SampleDependencyTypes { get; set; }

        public class SampleDependencyType
        {
            public byte IsLeading { get; set; }
            public byte SampleDependsOn { get; set; }
            public byte SampleIsDependedOn { get; set; }
            public byte SampleHasRedundancy { get; set; }
            public byte DegradPrio { get; set; }
            public byte IsNonSync { get; set; }
            public byte PaddingValue { get; set; }
        }
    }
}
