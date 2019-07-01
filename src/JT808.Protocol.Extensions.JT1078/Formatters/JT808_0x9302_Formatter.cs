using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9302_Formatter : IJT808MessagePackFormatter<JT808_0x9302>
    {
        public JT808_0x9302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9302 jT808_0x9302 = new JT808_0x9302();
            jT808_0x9302.LogicChannelNo = reader.ReadByte();
            jT808_0x9302.FocusAdjustmentDirection = reader.ReadByte();
            return jT808_0x9302;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9302 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.FocusAdjustmentDirection);
        }
    }
}