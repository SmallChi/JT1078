using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// dref
    /// </summary>
    public class DataReferenceBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// dref
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public DataReferenceBox(byte version=0, uint flags=0) : base("dref", version, flags)
        {
        }

        public uint EntryCount { get; set; }

        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<DataEntryBox> DataEntryBoxes { get; set; }= new List<DataEntryBox>();

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (DataEntryBoxes!=null && DataEntryBoxes.Count > 0)
            {
                writer.WriteUInt32((uint)DataEntryBoxes.Count);
                foreach(var item in DataEntryBoxes)
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
