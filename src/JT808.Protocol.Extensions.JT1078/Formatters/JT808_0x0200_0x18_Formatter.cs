using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x0200_0x18_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x18>
    {
        public JT808_0x0200_0x18 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x18 jT808_0x0200_0x18 = new JT808_0x0200_0x18();
            jT808_0x0200_0x18.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x18.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x18.AbnormalDrivingBehaviorAlarmInfo = reader.ReadUInt16();
            return jT808_0x0200_0x18;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x18 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.AbnormalDrivingBehaviorAlarmInfo);
        }
    }
}
