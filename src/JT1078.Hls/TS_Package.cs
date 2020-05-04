using JT1078.Hls.Formatters;
using JT1078.Hls.MessagePack;
using JT1078.Protocol.H264;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_Package : ITSMessagePackFormatter
    {
        public TS_Header Header { get; set; }
        public PES_Package Payload { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            Header.ToBuffer(ref writer);
            Payload.ToBuffer(ref writer);
        }
    }
}
