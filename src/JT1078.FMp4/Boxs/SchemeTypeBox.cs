using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SchemeTypeBox : FullBox
    {
        public SchemeTypeBox(byte version, uint flags) : base("schm", version, flags)
        {
        }

        public SchemeTypeBox(uint flags) : this(0,flags)
        {
        }

        public uint SchemeType { get; set; }

        public uint SchemeVersion { get; set; }
        /// <summary>
        /// if (flags & 0x000001)
        ///  UTF-8
        /// </summary>
        public string SchemeUri { get; set; }
    }
}
