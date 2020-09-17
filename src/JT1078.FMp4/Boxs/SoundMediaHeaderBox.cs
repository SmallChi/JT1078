using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SoundMediaHeaderBox : FullBox
    {
        public SoundMediaHeaderBox(byte version=0, uint flags=0) : base("smhd", version, flags)
        {
        }

        public ushort Balance { get; set; }
        public ushort Reserved { get; set; }
    }
}
