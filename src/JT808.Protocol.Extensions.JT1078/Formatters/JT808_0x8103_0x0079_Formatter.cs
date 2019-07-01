using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x8103_0x0079_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0079>
    {
        public JT808_0x8103_0x0079 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0079 jT808_0x8103_0x0079 = new JT808_0x8103_0x0079();
            jT808_0x8103_0x0079.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0079.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0079.StorageThresholds = reader.ReadByte();
            jT808_0x8103_0x0079.Duration = reader.ReadByte();
            jT808_0x8103_0x0079.BeginMinute = reader.ReadByte();  
            return jT808_0x8103_0x0079;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0079 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.StorageThresholds);
            writer.WriteByte(value.Duration);
            writer.WriteByte(value.BeginMinute);
        }
    }
}
