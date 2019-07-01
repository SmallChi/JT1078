using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1B00_0x1B02_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B02>
    {
        public JT809_JT1078_0x1B00_0x1B02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B02 jT808_JT1078_0x1B00_0x1B02 = new JT809_JT1078_0x1B00_0x1B02();
            jT808_JT1078_0x1B00_0x1B02.Result = reader.ReadByte();
            jT808_JT1078_0x1B00_0x1B02.SessionId = reader.ReadUInt16();
            jT808_JT1078_0x1B00_0x1B02.ServerIp = reader.ReadString(32);
            jT808_JT1078_0x1B00_0x1B02.TcpPort = reader.ReadUInt16();
            jT808_JT1078_0x1B00_0x1B02.UserName = reader.ReadString(49);
            jT808_JT1078_0x1B00_0x1B02.Password = reader.ReadString(22);
            jT808_JT1078_0x1B00_0x1B02.FilePath = reader.ReadString(200);
            return jT808_JT1078_0x1B00_0x1B02;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B02 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteUInt16(value.SessionId);
            writer.WriteStringPadLeft(value.ServerIp, 32);
            writer.WriteUInt16(value.TcpPort);
            writer.WriteStringPadLeft(value.UserName, 49);
            writer.WriteStringPadLeft(value.Password, 22);
            writer.WriteStringPadLeft(value.FilePath, 200);
        }
    }
}
