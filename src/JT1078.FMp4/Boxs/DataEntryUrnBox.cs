using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class DataEntryUrnBox : DataEntryBox
    {
        public DataEntryUrnBox(byte version, uint flags) : base("urn ", version, flags)
        {
        }
        public DataEntryUrnBox(uint flags) : this(0, flags)
        {
        }

        public string Name { get; set; }
        public string Location { get; set; }
        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if (!string.IsNullOrEmpty(Name))
            {
                writer.WriteUTF8(Name);
            }
            if (!string.IsNullOrEmpty(Location))
            {
                writer.WriteUTF8(Location);
            }
            End(ref writer);
        }
    }
}
