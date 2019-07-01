using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x1205_AVResouce_Formatter : IJT808MessagePackFormatter<JT808_0x1205_AVResouce>
    {
        public JT808_0x1205_AVResouce Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1205_AVResouce jT808_0x1205_AVResouce = new JT808_0x1205_AVResouce();
            jT808_0x1205_AVResouce.LogicChannelNo = reader.ReadByte();
            jT808_0x1205_AVResouce.BeginTime = reader.ReadDateTime6();
            jT808_0x1205_AVResouce.EndTime = reader.ReadDateTime6();
            jT808_0x1205_AVResouce.AlarmFlag = reader.ReadUInt32();
            jT808_0x1205_AVResouce.AVResourceType = reader.ReadByte();
            jT808_0x1205_AVResouce.StreamType = reader.ReadByte();
            jT808_0x1205_AVResouce.MemoryType = reader.ReadByte();
            jT808_0x1205_AVResouce.FileSize = reader.ReadUInt32();
            return jT808_0x1205_AVResouce;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1205_AVResouce value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
            writer.WriteUInt32(value.FileSize);
        }
    }
}