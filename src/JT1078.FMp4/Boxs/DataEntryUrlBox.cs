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
    }
}
