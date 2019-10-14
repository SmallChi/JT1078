using JT1078.Flv.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class AvcVideoPacke
    {
        public AvcPacketType AvcPacketType { get; set; }

        public uint CompositionTime { get; set; }

        public AVCDecoderConfigurationRecord AVCDecoderConfiguration { get; set; }
        public List<byte[]> MultiData { get; set; }
    }
}
