using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x0200_0x16_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x16>
    {
        public JT808_0x0200_0x16 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x16 jT808_0x0200_0x16 = new JT808_0x0200_0x16();
            jT808_0x0200_0x16.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x16.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x16.VideoSignalOcclusionAlarmStatus = reader.ReadUInt32();
            return jT808_0x0200_0x16;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x16 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoSignalOcclusionAlarmStatus);
        }
    }
}
