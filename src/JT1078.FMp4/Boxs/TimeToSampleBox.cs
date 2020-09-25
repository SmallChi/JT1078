using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stts
    /// </summary>
    public class TimeToSampleBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stts
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TimeToSampleBox(byte version = 0, uint flags = 0) : base("stts", version, flags)
        {
        }

        public uint EntryCount{get;set;}

        public List<TimeToSampleInfo> TimeToSampleInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(TimeToSampleInfos!=null && TimeToSampleInfos.Count > 0)
            {
                writer.WriteUInt32((uint)TimeToSampleInfos.Count);
                foreach (var item in TimeToSampleInfos)
                {
                    writer.WriteUInt32(item.SampleCount);
                    writer.WriteUInt32(item.SampleDelta);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
        }

        public class TimeToSampleInfo
        {
            public uint SampleCount { get; set; }

            public uint SampleDelta { get; set; }
        }
    }
}
