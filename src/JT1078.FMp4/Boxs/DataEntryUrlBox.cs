using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class DataEntryUrlBox : DataEntryBox
    {
        public DataEntryUrlBox(byte version, uint flags) : base("url ", version, flags)
        {
        }

        public DataEntryUrlBox(uint flags) : this(0, flags)
        {
        }

        public string Location { get; set; }

        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (!string.IsNullOrEmpty(Location))
            {
                writer.WriteUTF8(Location);
            }
            End(ref writer);
        }
    }
}
