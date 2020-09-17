using JT1078.FMp4.Samples;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleGroupDescriptionBox : FullBox
    {
        public SampleGroupDescriptionBox(string handlerType, byte version, uint flags=0) : base("sgpd", version, flags)
        {
            HandlerType = handlerType;
        }

        public string HandlerType { get; set; }
        /// <summary>
        /// if (version==1)
        /// </summary>
        public uint DefaultLength { get; set; }
        public uint EntryCount { get; set; }
        public List<SampleGroupDescription> SampleGroupDescriptions { get; set; }
        public class SampleGroupDescription
        {
            public uint DescriptionLength { get; set; }

            public SampleGroupDescriptionEntry SampleGroupDescriptionEntry { get; set; }
        }
    }
}
