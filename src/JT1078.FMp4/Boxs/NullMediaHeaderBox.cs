using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class NullMediaHeaderBox : FullBox
    {
        public NullMediaHeaderBox(byte version=0, uint flags=0) : base("nmhd", version, flags)
        {
        }
    }
}
