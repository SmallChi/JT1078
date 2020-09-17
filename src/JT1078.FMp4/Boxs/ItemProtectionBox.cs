using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ItemProtectionBox : FullBox
    {
        public ItemProtectionBox(byte version=0, uint flags=0) : base("ipro", version, flags)
        {
        }
        public ushort ProtectionCount { get; set; }


    }
}
