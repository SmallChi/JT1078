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
    public class JT808_0x8103_0x007C_TimerWakeDayParamter_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>
    {
        public JT808_0x8103_0x007C_TimerWakeDayParamter Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007C_TimerWakeDayParamter jT808_0x8103_0x007C_TimerWakeDayParamter = new JT808_0x8103_0x007C_TimerWakeDayParamter();
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimerWakeEnableFlag = reader.ReadByte();
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod1WakeTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod1CloseTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod2WakeTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod2CloseTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod3WakeTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod3CloseTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod4WakeTime = reader.ReadBCD(4);
            jT808_0x8103_0x007C_TimerWakeDayParamter.TimePeriod4CloseTime = reader.ReadBCD(4);
            return jT808_0x8103_0x007C_TimerWakeDayParamter;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007C_TimerWakeDayParamter value, IJT808Config config)
        {
            writer.WriteByte(value.TimerWakeEnableFlag);
            writer.WriteBCD(value.TimePeriod1WakeTime, 4);
            writer.WriteBCD(value.TimePeriod1CloseTime, 4);
            writer.WriteBCD(value.TimePeriod2WakeTime, 4);
            writer.WriteBCD(value.TimePeriod2CloseTime, 4);
            writer.WriteBCD(value.TimePeriod3WakeTime, 4);
            writer.WriteBCD(value.TimePeriod3CloseTime, 4);
            writer.WriteBCD(value.TimePeriod4WakeTime, 4);
            writer.WriteBCD(value.TimePeriod4CloseTime, 4);
        }
    }
}
