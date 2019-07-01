using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1800_0x1801_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1800_0x1801>
    {
        public JT809_JT1078_0x1800_0x1801 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1800_0x1801 jT808_JT1078_0x1800_0x1801 = new JT809_JT1078_0x1800_0x1801();
            jT808_JT1078_0x1800_0x1801.Result = reader.ReadByte();
            jT808_JT1078_0x1800_0x1801.ServerIp = reader.ReadString(32);
            jT808_JT1078_0x1800_0x1801.ServerPort = reader.ReadUInt16();
            return jT808_JT1078_0x1800_0x1801;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1800_0x1801 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteStringPadLeft(value.ServerIp,32);
            writer.WriteUInt16(value.ServerPort);
        }
    }
}
