using JT1078.Protocol.H264;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_Package
    {
        public TS_Header Header { get; set; }
        public PES_Package Payload { get; set; }
        /// <summary>
        /// 填充字节,取值0xff
        /// </summary>
        public byte[] Fill { get; set; }
    }
}
