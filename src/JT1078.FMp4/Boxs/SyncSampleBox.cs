using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stss
    /// </summary>
    public class SyncSampleBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stss
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SyncSampleBox(byte version=0, uint flags=0) : base("stss", version, flags)
        {
        }

        //private uint EntryCount { get; set; }

        public List<uint> SampleNumbers { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(SampleNumbers!=null && SampleNumbers.Count > 0)
            {
                writer.WriteUInt32((uint)SampleNumbers.Count);
                foreach(var item in SampleNumbers)
                {
                    writer.WriteUInt32(item);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
        }
    }
}
