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
    public class JT808_0x8103_0x007B_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x007B>
    {
        public JT808_0x8103_0x007B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007B jT808_0x8103_0x007B = new JT808_0x8103_0x007B();
            jT808_0x8103_0x007B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007B.NuclearLoadNumber = reader.ReadByte();
            jT808_0x8103_0x007B.FatigueThreshold = reader.ReadByte();
            return jT808_0x8103_0x007B;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.NuclearLoadNumber);
            writer.WriteByte(value.FatigueThreshold);
        }
    }
}
