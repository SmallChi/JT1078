using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class DegradationPriorityBox : FullBox
    {
        public DegradationPriorityBox(byte version=0, uint flags=0) : base("stdp", version, flags)
        {
        }
        /// <summary>
        /// sample_count is taken from the sample_count in the Sample Size Box ('stsz').
        /// </summary>
        public List<ushort> Priorities { get; set; }
    }
}
