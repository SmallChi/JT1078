using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleToGroupBox : FullBox
    {
        public SampleToGroupBox(byte version, uint flags) : base("sbgp", version, flags)
        {
        }

        public uint GroupingType { get; set; }
        /// <summary>
        /// version == 1
        /// </summary>
        public uint GroupingTypeParameter { get; set; }

        public uint EntryCount { get; set; }

        public List<SampleToGroupInfo> SampleToGroupInfos { get; set; }

        public class SampleToGroupInfo
        {
            public uint SampleCount { get; set; }
            public uint GroupDescriptionIndex { get; set; }
        }
    }
}
