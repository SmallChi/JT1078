using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x0200_0x14_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x14>
    {
        public JT808_0x0200_0x14 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x14 jT808_0x0200_0x14 = new JT808_0x0200_0x14();
            jT808_0x0200_0x14.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x14.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x14.VideoRelateAlarm = reader.ReadUInt32();
            return jT808_0x0200_0x14;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x14 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoRelateAlarm);
        }
    }
}
