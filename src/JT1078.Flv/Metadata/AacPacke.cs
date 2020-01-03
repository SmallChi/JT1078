using JT1078.Flv.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class AacPacke
    {
        public AacPacke(AACPacketType packetType, byte[] data = null)
        {
            AACPacketType = packetType;
            if (packetType == AACPacketType.AudioSpecificConfig)
            {
                Data.Add(0);
                Data.AddRange(new AudioSpecificConfig().ToArray());
            }
            else
            {
                Data.Add(1);
                Data.AddRange(data ?? throw new NullReferenceException("data cannot be null"));
            }
        }
        public AACPacketType AACPacketType { get; private set; }

        List<byte> Data { get; set; } = new List<byte>();
        /// <summary>
        /// 元数据
        /// </summary>
        public byte[] RawData => Data.ToArray();
    }
}
