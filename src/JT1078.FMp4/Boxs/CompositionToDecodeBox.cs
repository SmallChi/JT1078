using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CompositionToDecodeBox : FullBox
    {
        public CompositionToDecodeBox(byte version=0, uint flags=0) : base("cslg", version, flags)
        {
        }

        public int CompositionToDTSShift { get; set; }
        public int LeastDecodeToDisplayDelta { get; set; }
        public int GreatestDecodeToDisplayDelta { get; set; }
        public int CompositionStartTime { get; set; }
        public int CompositionEndTime { get; set; }
    }
}
