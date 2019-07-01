using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1A00_0x1A01_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1A00_0x1A01>
    {
        public JT809_JT1078_0x1A00_0x1A01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1A00_0x1A01 jT808_JT1078_0x1A00_0x1A01 = new JT809_JT1078_0x1A00_0x1A01();
            jT808_JT1078_0x1A00_0x1A01.ServerIp = reader.ReadString(32);
            jT808_JT1078_0x1A00_0x1A01.ServerPort = reader.ReadUInt16();
            jT808_JT1078_0x1A00_0x1A01.Result = reader.ReadByte();
            return jT808_JT1078_0x1A00_0x1A01;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1A00_0x1A01 value, IJT809Config config)
        {
            writer.WriteStringPadLeft(value.ServerIp, 32);
            writer.WriteUInt16(value.ServerPort);
            writer.WriteByte(value.Result);
        }
    }
}
