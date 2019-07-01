using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9B00_0x9B02_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9B00_0x9B02>
    {
        public JT809_JT1078_0x9B00_0x9B02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9B00_0x9B02 jT808_JT1078_0x9B00_0x9B02 = new JT809_JT1078_0x9B00_0x9B02();
            jT808_JT1078_0x9B00_0x9B02.Result = reader.ReadByte();
            jT808_JT1078_0x9B00_0x9B02.SessionId = reader.ReadUInt16();
            return jT808_JT1078_0x9B00_0x9B02;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9B00_0x9B02 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteUInt16(value.SessionId);
        }
    }
}
