using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public abstract class FullBox : Mp4Box
    {
        public FullBox(string boxType,byte version,uint flags) : base(boxType)
        {
            Version = version;
            Flags = flags;
        }
        public byte Version { get; set; }
        public uint Flags { get; set; }
    }
}
