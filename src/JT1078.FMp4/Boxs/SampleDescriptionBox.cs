using JT1078.FMp4.Enums;
using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using JT1078.FMp4.Samples;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stsd
    /// </summary>
    public class SampleDescriptionBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stsd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SampleDescriptionBox(byte version=0, uint flags=0) : base("stsd", version, flags)
        {

        }
        private uint EntryCount { get; set; }
        public List<SampleEntry> SampleEntries { get; set; }
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if(SampleEntries!=null && SampleEntries.Count > 0)
            {
                writer.WriteUInt32((uint)SampleEntries.Count);
                foreach (var item in SampleEntries)
                {
                    item.ToBuffer(ref writer);
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
