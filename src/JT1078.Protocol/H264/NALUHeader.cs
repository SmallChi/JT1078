using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.H264
{
    public struct NALUHeader
    {
        public NALUHeader(byte value)
        {
            ForbiddenZeroBit = (value & 0x80) >> 7;
            NalRefIdc = (value & 0x60) >> 5;
            NalUnitType = (NalUnitType)(value & 0x1f);
            KeyFrame=NalUnitType== NalUnitType.IDR;
        }
        public NALUHeader(ReadOnlySpan<byte> value)
        {
            ForbiddenZeroBit = (value[0] & 0x80) >> 7;
            NalRefIdc = (value[0] & 0x60) >> 5;
            NalUnitType = (NalUnitType)(value[0] & 0x1f);
            KeyFrame=NalUnitType== NalUnitType.IDR;
        }
        public int ForbiddenZeroBit { get; set; }
        public int NalRefIdc { get; set; }
        public NalUnitType NalUnitType { get; set; }
        public bool KeyFrame { get; set; }
    }

    public enum NalUnitType : int
    {
        None=0,
        SLICE = 1,
        DPA = 2,
        DPB = 3,
        DPC = 4,
        IDR = 5,
        SEI = 6,
        SPS = 7,
        PPS = 8,
        AUD = 9,
        EOSEQ = 10,
        EOSTREAM = 11,
        FILL = 12,
    }
}
