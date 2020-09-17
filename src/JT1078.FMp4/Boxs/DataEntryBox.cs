using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public abstract class DataEntryBox : FullBox
    {
        public DataEntryBox(string boxType, byte version, uint flags) : base(boxType, version, flags)
        {
        }
    }
}
