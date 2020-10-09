using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// sbgp
    /// </summary>
    public class SampleToGroupBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// sbgp
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
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

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            writer.WriteUInt32(GroupingType);
            if (Version == 1)
            {
                writer.WriteUInt32(GroupingTypeParameter);
            }
            if(SampleToGroupInfos!=null && SampleToGroupInfos.Count > 0)
            {
                writer.WriteUInt32((uint)SampleToGroupInfos.Count);
                foreach(var item in SampleToGroupInfos)
                {
                    writer.WriteUInt32(item.SampleCount);
                    writer.WriteUInt32(item.GroupDescriptionIndex);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
        }

        public class SampleToGroupInfo
        {
            public uint SampleCount { get; set; }
            public uint GroupDescriptionIndex { get; set; }
        }
    }
}
