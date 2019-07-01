using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1B00_0x1B01_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B01>
    {
        public JT809_JT1078_0x1B00_0x1B01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B01 jT808_JT1078_0x1B00_0x1B01 = new JT809_JT1078_0x1B00_0x1B01();
            jT808_JT1078_0x1B00_0x1B01.Result = reader.ReadByte();
            jT808_JT1078_0x1B00_0x1B01.SessionId = reader.ReadUInt16();
            return jT808_JT1078_0x1B00_0x1B01;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B01 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteUInt16(value.SessionId);
        }
    }
}
