using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x1005_Formatter : IJT808MessagePackFormatter<JT808_0x1005>
    {
        public JT808_0x1005 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1005 jT808_0x1005 = new JT808_0x1005();
            jT808_0x1005.BeginTime = reader.ReadDateTime6();
            jT808_0x1005.EndTime = reader.ReadDateTime6();
            jT808_0x1005.GettingOnNumber = reader.ReadUInt16();
            jT808_0x1005.GettingOffNumber = reader.ReadUInt16();
            return jT808_0x1005;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1005 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt16(value.GettingOnNumber);
            writer.WriteUInt16(value.GettingOffNumber);
        }
    }
}
