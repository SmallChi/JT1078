using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9B00_0x9B03_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9B00_0x9B03>
    {
        public JT809_JT1078_0x9B00_0x9B03 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9B00_0x9B03 jT808_JT1078_0x9B00_0x9B03 = new JT809_JT1078_0x9B00_0x9B03();
            jT808_JT1078_0x9B00_0x9B03.SessionId = reader.ReadUInt16();
            jT808_JT1078_0x9B00_0x9B03.Type = (JT809_JT1078_0x9B00_0x9B03_Type)reader.ReadByte();
            return jT808_JT1078_0x9B00_0x9B03;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9B00_0x9B03 value, IJT809Config config)
        {
            writer.WriteUInt16(value.SessionId);
            writer.WriteByte((byte)value.Type);
        }
    }
}
