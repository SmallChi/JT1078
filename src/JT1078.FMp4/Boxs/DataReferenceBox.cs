using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class DataReferenceBox : FullBox
    {
        public DataReferenceBox(byte version=0, uint flags=0) : base("dref", version, flags)
        {
        }

        public uint EntryCount { get; set; }

        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<DataEntryBox> DataEntryBoxes { get; set; }
    }
}
