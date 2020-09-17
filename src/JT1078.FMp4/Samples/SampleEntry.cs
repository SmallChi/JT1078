using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public abstract class SampleEntry : Mp4Box
    {
        public SampleEntry(string boxType) : base(boxType)
        {
        }
        public byte[] Reserved { get; set; } = new byte[6];
        public ushort DataReferenceIndex { get; set; }
    }
}
