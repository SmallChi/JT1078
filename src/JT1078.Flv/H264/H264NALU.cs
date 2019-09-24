using JT1078.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.H264
{
    public class H264NALU
    {
        public readonly static byte[] Start1 = new byte[3] { 0, 0, 1 };
        public readonly static byte[] Start2 = new byte[4] { 0, 0, 0, 1 };
        public byte[] StartCodePrefix { get; set; }
        public NALUHeader NALUHeader { get; set; }
        public JT1078Package JT1078Package { get; set; }
    }
}
