using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9800_0x9802_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9800_0x9802>
    {
        public JT809_JT1078_0x9800_0x9802 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9800_0x9802 jT808_JT1078_0x9800_0x9802 = new JT809_JT1078_0x9800_0x9802();
            jT808_JT1078_0x9800_0x9802.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9800_0x9802.AVitemType = reader.ReadByte();
            return jT808_JT1078_0x9800_0x9802;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9800_0x9802 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVitemType);
        }
    }
}
