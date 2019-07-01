using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9800_0x9801_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9800_0x9801>
    {
        public JT809_JT1078_0x9800_0x9801 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9800_0x9801 jT808_JT1078_0x9800_0x9801 = new JT809_JT1078_0x9800_0x9801();
            jT808_JT1078_0x9800_0x9801.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9800_0x9801.AVitemType = reader.ReadByte();
            jT808_JT1078_0x9800_0x9801.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9800_0x9801.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9800_0x9801;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9800_0x9801 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVitemType);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}
