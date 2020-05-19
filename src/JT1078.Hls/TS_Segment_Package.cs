using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_Segment_Package : ITSMessagePackFormatter
    {
        public TS_Header Header { get; set; }
        public byte[] Payload { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            Header.ToBuffer(ref writer);
            writer.WriteArray(Payload);
        }
    }
}
