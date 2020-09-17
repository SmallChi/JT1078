using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SubTrackSampleGroupBox : FullBox
    {
        public SubTrackSampleGroupBox(byte version=0, uint flags=0) : base("stsg", version, flags)
        {
        }
        public uint GroupingType { get; set; }
        public ushort ItemCount { get; set; }
        /// <summary>
        /// length:ItemCount
        /// </summary>
        public List<uint> GroupDescriptionIndex { get; set; }
    }
}
