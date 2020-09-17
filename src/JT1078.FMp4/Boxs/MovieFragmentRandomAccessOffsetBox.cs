using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieFragmentRandomAccessOffsetBox : FullBox
    {
        public MovieFragmentRandomAccessOffsetBox(byte version, uint flags=0) : base("mfro", version, flags)
        {
        }

        public uint MfraSize { get; set; }
    }
}
