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
    public class JT808_0x8103_0x007C_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x007C>
    {
        public JT808_0x8103_0x007C Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007C jT808_0x8103_0x007C = new JT808_0x8103_0x007C();
            jT808_0x8103_0x007C.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007C.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007C.SleepWakeMode = reader.ReadByte();
            jT808_0x8103_0x007C.WakeConditionType = reader.ReadByte();
            jT808_0x8103_0x007C.TimerWakeDaySet = reader.ReadByte();
            jT808_0x8103_0x007C.jT808_0X8103_0X007C_TimerWakeDayParamter = config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Deserialize(ref reader, config);
            return jT808_0x8103_0x007C;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007C value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out var position);       
            writer.WriteByte(value.SleepWakeMode);
            writer.WriteByte(value.WakeConditionType);
            writer.WriteByte(value.TimerWakeDaySet);
            config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Serialize(ref writer, value.jT808_0X8103_0X007C_TimerWakeDayParamter, config);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition()-position-1),position);
        }
    }
}
