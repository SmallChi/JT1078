using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.H264
{
    public struct NALUHeader
    {
        public NALUHeader(byte value)
        {
            ForbiddenZeroBit = (value & 0x80) >> 7;
            NalRefIdc = (value & 0x60) >> 5;
            NalUnitType = value & 0x1f;
        }
        public NALUHeader(ReadOnlySpan<byte> value)
        {
            ForbiddenZeroBit = (value[0] & 0x80) >> 7;
            NalRefIdc = (value[0] & 0x60) >> 5;
            NalUnitType = value[0] & 0x1f;
        }
        public int ForbiddenZeroBit { get; set; }
        public int NalRefIdc { get; set; }
        public int NalUnitType { get; set; }
    }
}
