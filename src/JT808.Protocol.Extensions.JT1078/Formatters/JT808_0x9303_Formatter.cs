using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9303_Formatter : IJT808MessagePackFormatter<JT808_0x9303>
    {
        public JT808_0x9303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9303 jT808_0x9303 = new JT808_0x9303();
            jT808_0x9303.LogicChannelNo = reader.ReadByte();
            jT808_0x9303.IrisAdjustment = reader.ReadByte();
            return jT808_0x9303;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9303 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.IrisAdjustment);
        }
    }
}