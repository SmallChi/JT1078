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
    public class JT808_0x8103_0x007A_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x007A>
    {
        public JT808_0x8103_0x007A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007A jT808_0x8103_0x007A = new JT808_0x8103_0x007A();
            jT808_0x8103_0x007A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007A.AlarmShielding = reader.ReadUInt32();
            return jT808_0x8103_0x007A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.AlarmShielding);
        }
    }
}
