using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class PrimaryItemBox : FullBox
    {
        public PrimaryItemBox(byte version=0, uint flags=0) : base("pitm", version, flags)
        {
        }

        public ushort ItemID { get; set; }
    }
}
