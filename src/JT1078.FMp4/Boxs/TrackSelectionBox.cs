using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackSelectionBox : FullBox
    {
        public TrackSelectionBox(byte version=0, uint flags=0) : base("tsel", version, flags)
        {
        }

        public uint SwitchGroup { get; set; }

        public List<uint> AttributeList { get; set; }
    }
}
