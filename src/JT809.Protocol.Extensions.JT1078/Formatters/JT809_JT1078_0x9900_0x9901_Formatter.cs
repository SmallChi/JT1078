using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9900_0x9901_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9900_0x9901>
    {
        public JT809_JT1078_0x9900_0x9901 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9901 jT808_JT1078_0x9900_0x9901 = new JT809_JT1078_0x9900_0x9901();
            jT808_JT1078_0x9900_0x9901.Result = reader.ReadByte();
            jT808_JT1078_0x9900_0x9901.ItemNumber = reader.ReadByte();
            return jT808_JT1078_0x9900_0x9901;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9900_0x9901 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteByte(value.ItemNumber);
        }
    }
}
