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
            if(SubSampleInformations!=null && SubSampleInformations.Count > 0)
            {
                foreach(var item in SubSampleInformations)
                {
                    writer.WriteUInt32(item.SampleDelta);
                    if (item.InnerSubSampleInformations != null && item.InnerSubSampleInformations.Count > 0)
                    {
                        writer.WriteUInt16((ushort)item.InnerSubSampleInformations.Count);
                        foreach(var subItem in item.InnerSubSampleInformations)
                        {
                            if (Version == 1)
                            {
                                writer.WriteUInt32(subItem.SubsampleSize);
                            }
                            else
                            {
                                writer.WriteUInt16((ushort)subItem.SubsampleSize);
                            }
                            writer.WriteByte(subItem.SubsamplePriority);
                            writer.WriteByte(subItem.Discardable);
                            writer.WriteUInt32(subItem.Reserved);
                        }
                    }
                    else
                    {
                        writer.WriteUInt16(0);
                    }
                }
            }
            else
            {
                writer.WriteUInt32((uint)SubSampleInformations.Count);
            }
        }

        public class SubSampleInformation
        {
            public uint SampleDelta { get; set; }
            public ushort SubsampleCount { get; set; }

            public List<InnerSubSampleInformation> InnerSubSampleInformations { get; set; }

            public class InnerSubSampleInformation
            {
                /// <summary>
                /// version == 1 uint32
                /// version != 1 uint16
                /// </summary>
                public uint SubsampleSize { get; set; }
                public byte SubsamplePriority { get; set; }
                public byte Discardable { get; set; }
                public uint Reserved { get; set; }
            }
        }
    }
}
