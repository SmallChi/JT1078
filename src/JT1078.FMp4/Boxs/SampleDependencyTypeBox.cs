using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// sdtp
    /// </summary>
    public class SampleDependencyTypeBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// sdtp
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SampleDependencyTypeBox(byte version=0, uint flags=0) : base("sdtp", version, flags)
        {
        }
        /// <summary>
        /// is taken from the sample_count in the Sample Size Box ('stsz') or Compact Sample Size Box(‘stz2’).
        /// </summary>
        public List<SampleDependencyType> SampleDependencyTypes { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(SampleDependencyTypes!=null && SampleDependencyTypes.Count > 0)
            {
                //todo: wait ref doc
                foreach(var item in SampleDependencyTypes)
                {
                    writer.WriteByte(item.IsLeading);
                    writer.WriteByte(item.SampleDependsOn);
                    writer.WriteByte(item.SampleIsDependedOn);
                    writer.WriteByte(item.SampleHasRedundancy);
                    writer.WriteByte(item.DegradPrio);
                    writer.WriteByte(item.IsNonSync);
                    writer.WriteByte(item.PaddingValue);
                }
            }
            End(ref writer);
        }

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
