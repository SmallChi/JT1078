using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TimeToSampleBox : FullBox
    {
        public TimeToSampleBox(byte version = 0, uint flags = 0) : base("stts", version, flags)
        {
        }

        public uint EntryCount{get;set;}

        public List<TimeToSampleInfo> TimeToSampleInfos { get; set; }

        public class TimeToSampleInfo
        {
            public uint SampleCount { get; set; }

            public uint SampleDelta { get; set; }
        }
    }
}
