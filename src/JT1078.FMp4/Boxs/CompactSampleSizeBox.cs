using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class CompactSampleSizeBox : FullBox
    {
        public CompactSampleSizeBox(byte version=0, uint flags=0) : base("stz2", version, flags)
        {
        }

        public byte[] Reserved { get; set; } = new byte[3];
        /// <summary>
        /// 4, 8 or 16
        /// </summary>
        public byte FieldSize { get; set; }

        public uint SampleCount { get; set; }

        /// <summary>
        /// length:SampleCount
        /// DepOn:field_size=>DataType
        /// </summary>
        public List<ushort> EntrySize { get; set; }
    }
}
