using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9A00_0x9A02_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9A00_0x9A02>
    {
        public JT809_JT1078_0x9A00_0x9A02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A02 jT808_JT1078_0x9A00_0x9A02 = new JT809_JT1078_0x9A00_0x9A02();
            jT808_JT1078_0x9A00_0x9A02.ControlType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A02.FastTime = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A02.DateTime = reader.ReadUTCDateTime();
            return jT808_JT1078_0x9A00_0x9A02;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9A00_0x9A02 value, IJT809Config config)
        {
            writer.WriteByte(value.ControlType);
            writer.WriteByte(value.FastTime);
            writer.WriteUTCDateTime(value.DateTime);
        }
    }
}