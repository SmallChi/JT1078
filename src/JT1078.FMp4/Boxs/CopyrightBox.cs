using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CopyrightBox : FullBox
    {
        public CopyrightBox(byte version=0, uint flags=0) : base("cprt", version, flags)
        {
        }
        /// <summary>
        /// 16-1
        /// </summary>
        public bool Pad { get; set; }
        /// <summary>
        /// 16-15
        /// ISO-639-2/T language code
        /// </summary>
        public byte Language { get; set; }

        public string Notice { get; set; }
    }
}
