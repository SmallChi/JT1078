using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1800_0x1802_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1800_0x1802>
    {
        public JT809_JT1078_0x1800_0x1802 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1800_0x1802 jT808_JT1078_0x1800_0x1802 = new JT809_JT1078_0x1800_0x1802();
            jT808_JT1078_0x1800_0x1802.Result = reader.ReadByte();
            return jT808_JT1078_0x1800_0x1802;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1800_0x1802 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
        }
    }
}
