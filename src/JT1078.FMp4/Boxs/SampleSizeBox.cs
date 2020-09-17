using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleSizeBox : FullBox
    {
        public SampleSizeBox(byte version=0, uint flags=0) : base("stsz", version, flags)
        {
        }

        public uint SampleSize { get; set; }
        public uint SampleCount { get; set; }
        /// <summary>
        ///  if (sample_size==0)
        ///  length:sample_count
        /// </summary>
        public List<uint> EntrySize { get; set; }
    }
}
