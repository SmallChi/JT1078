using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///终端休眠模式唤醒设置
    /// 0x8103_0x007C
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x007C_TimerWakeDayParamter_Formatter))]
    public class JT808_0x8103_0x007C_TimerWakeDayParamter
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
    }
}
