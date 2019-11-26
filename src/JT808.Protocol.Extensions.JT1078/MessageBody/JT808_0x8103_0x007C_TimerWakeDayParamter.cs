using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///终端休眠模式唤醒设置
    /// 0x8103_0x007C
    /// </summary>
    public class JT808_0x8103_0x007C_TimerWakeDayParamter:IJT808MessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>
    {
        /// <summary>
        /// 定时唤醒启用标志
        /// </summary>
        public byte TimerWakeEnableFlag { get; set; }
        /// <summary>
        /// 时间段1唤醒时间
        /// 2
        /// </summary>
        public string TimePeriod1WakeTime  { get; set; }
        /// <summary>
        /// 时间段1关闭时间
        /// 2
        /// </summary>
        public string TimePeriod1CloseTime { get; set; }
        /// <summary>
        /// 时间段2唤醒时间
        /// 2
        /// </summary>
        public string TimePeriod2WakeTime { get; set; }
        /// <summary>
        /// 时间段2关闭时间
        /// 2
        /// </summary>
        public string TimePeriod2CloseTime { get; set; }
        /// <summary>
        /// 时间段3唤醒时间
        /// 2
        /// </summary>
        public string TimePeriod3WakeTime { get; set; }
        /// <summary>
        /// 时间段3关闭时间
        /// 2
        /// </summary>
        public string TimePeriod3CloseTime { get; set; }
        /// <summary>
        /// 时间段4唤醒时间
        /// 2
        /// </summary>
        public string TimePeriod4WakeTime { get; set; }
        /// <summary>
        /// 时间段4关闭时间
        /// 2
        /// </summary>
        public string TimePeriod4CloseTime { get; set; }

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
