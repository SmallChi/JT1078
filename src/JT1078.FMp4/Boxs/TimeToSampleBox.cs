using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TimeToSampleBox : FullBox, IFMp4MessagePackFormatter
    {
        public TimeToSampleBox(byte version = 0, uint flags = 0) : base("stts", version, flags)
        {
        }

        public uint EntryCount{get;set;}

        public List<TimeToSampleInfo> TimeToSampleInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            throw new NotImplementedException();
        }

        public class TimeToSampleInfo
        {
            public uint SampleCount { get; set; }

            public uint SampleDelta { get; set; }
        }
    }
}
