using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleAuxiliaryInformationOffsetsBox : FullBox
    {
        public SampleAuxiliaryInformationOffsetsBox(byte version, uint flags) : base("saio", version, flags)
        {
        }
        /// <summary>
        /// if (flags & 1)
        /// </summary>
        public uint AuxInfoType { get; set; }
        /// <summary>
        /// if (flags & 1)
        /// </summary>
        public uint AuxInfoTypeParameter { get; set; }
        public uint EntryCount { get; set; }
        /// <summary>
        /// length:entry_count
        /// </summary>
        public uint[] Offset { get; set; }
        /// <summary>
        /// length:entry_count
        /// </summary>
        public ulong[] OffsetLarge { get; set; }
    }
}
