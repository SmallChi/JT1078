using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SubTrackInformationBox : FullBox
    {
        public SubTrackInformationBox(byte version=0, uint flags=0) : base("stri", version, flags)
        {
        }

        public ushort SwitchGroup { get; set; }
        public ushort AlternateGroup { get; set; }
        public uint SubTrackID { get; set; }
        public List<uint> AttributeList { get; set; }
    }
}
