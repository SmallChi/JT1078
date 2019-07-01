using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x0200_0x15_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x15>
    {
        public JT808_0x0200_0x15 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x15 jT808_0x0200_0x15 = new JT808_0x0200_0x15();
            jT808_0x0200_0x15.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x15.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x15.VideoSignalLoseAlarmStatus = reader.ReadUInt32();
            return jT808_0x0200_0x15;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x15 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoSignalLoseAlarmStatus);
        }
    }
}
