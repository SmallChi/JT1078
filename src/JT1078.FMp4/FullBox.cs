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
        /// <summary>
        /// unsigned int(8)
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// bit(24)
        /// </summary>
        public uint Flags { get; set; }
    }
}
