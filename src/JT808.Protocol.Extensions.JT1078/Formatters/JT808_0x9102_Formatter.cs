using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9102_Formatter : IJT808MessagePackFormatter<JT808_0x9102>
    {
        public JT808_0x9102 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9102 jT808_0X9102 = new JT808_0x9102();
            jT808_0X9102.LogicalChannelNo = reader.ReadByte();
            jT808_0X9102.ControlCmd = reader.ReadByte();
            jT808_0X9102.CloseAVData = reader.ReadByte();
            jT808_0X9102.SwitchStreamType = reader.ReadByte();
            return jT808_0X9102;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9102 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicalChannelNo);
            writer.WriteByte(value.ControlCmd);
            writer.WriteByte(value.CloseAVData);
            writer.WriteByte(value.SwitchStreamType);
        }
    }
}
