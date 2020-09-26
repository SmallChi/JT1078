using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// subs
    /// </summary>
    public class SubSampleInformationBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// subs
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SubSampleInformationBox(byte version, uint flags=0) : base("subs", version, flags)
        {
        }

        public uint EntryCount { get; set; }

        public List<SubSampleInformation> SubSampleInformations { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            //todo:subs
        }

        public class SubSampleInformation
        {
            public uint SampleDelta { get; set; }
            public ushort SubsampleCount { get; set; }

            public List<InnerSubSampleInformation> InnerSubSampleInformations { get; set; }

            public class InnerSubSampleInformation
            {
                /// <summary>
                /// version == 1
                /// </summary>
                public uint SubsampleSizeLarge { get; set; }
                public ushort SubsampleSize { get; set; }
                public byte SubsamplePriority { get; set; }
                public byte Discardable { get; set; }
                public uint Reserved { get; set; }
            }
        }
    }
}
