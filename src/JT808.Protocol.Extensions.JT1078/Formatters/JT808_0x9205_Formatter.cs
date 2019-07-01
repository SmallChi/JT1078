using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9205_Formatter : IJT808MessagePackFormatter<JT808_0x9205>
    {
        public JT808_0x9205 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9205 jT808_0x9205 = new JT808_0x9205();
            jT808_0x9205.LogicChannelNo = reader.ReadByte();
            jT808_0x9205.BeginTime = reader.ReadDateTime6();
            jT808_0x9205.EndTime = reader.ReadDateTime6();
            jT808_0x9205.AlarmFlag = reader.ReadUInt32();
            jT808_0x9205.AVResourceType = reader.ReadByte();
            jT808_0x9205.StreamType = reader.ReadByte();
            jT808_0x9205.MemoryType = reader.ReadByte();
            return jT808_0x9205;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9205 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
        }
    }
}
