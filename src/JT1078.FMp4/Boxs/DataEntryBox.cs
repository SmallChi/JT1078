using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public abstract class DataEntryBox : FullBox,IFMp4MessagePackFormatter
    {
        public DataEntryBox(string boxType, byte version, uint flags) : base(boxType, version, flags)
        {
        }

        public abstract void ToBuffer(ref FMp4MessagePackWriter writer);
    }
}
