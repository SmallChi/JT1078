using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9202_Formatter : IJT808MessagePackFormatter<JT808_0x9202>
    {
        public JT808_0x9202 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9202 jT808_0x9202 = new JT808_0x9202();
            jT808_0x9202.AVChannelNo = reader.ReadByte();
            jT808_0x9202.PlayBackControl = reader.ReadByte();
            jT808_0x9202.FastForwardOrFastRewindMultiples = reader.ReadByte();
            jT808_0x9202.DragPlaybackPosition = reader.ReadDateTime6();
            return jT808_0x9202;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9202 value, IJT808Config config)
        {
            writer.WriteByte(value.AVChannelNo);
            writer.WriteByte(value.PlayBackControl);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples);
            writer.WriteDateTime6(value.DragPlaybackPosition);
        }
    }
}
