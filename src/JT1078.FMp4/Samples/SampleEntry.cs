using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// SampleEntry
    /// </summary>
    public abstract class SampleEntry : Mp4Box,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// SampleEntry
        /// </summary>
        /// <param name="boxType"></param>
        public SampleEntry(string boxType) : base(boxType)
        {
        }
        public byte[] Reserved { get; set; } = new byte[6];
        public ushort DataReferenceIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected void WriterSampleEntryToBuffer(ref FMp4MessagePackWriter writer)
        {
            foreach(var item in Reserved)
            {
                writer.WriteByte(item);
            }
            writer.WriteUInt16(DataReferenceIndex);
        }

        public abstract void ToBuffer(ref FMp4MessagePackWriter writer);
    }
}
