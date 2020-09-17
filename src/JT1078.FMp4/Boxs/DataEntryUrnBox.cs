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
    }
}
