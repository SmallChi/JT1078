using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieFragmentHeaderBox : FullBox
    {
        public MovieFragmentHeaderBox(byte version=0, uint flags=0) : base("mfhd", version, flags)
        {
        }

        public uint SequenceNumber { get; set; }
    }
}
