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
    [JT808Formatter(typeof(JT808_0x8103_0x007C_Formatter))]
    public class JT808_0x8103_0x007C : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x007C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 20;
        /// <summary>
        /// 休眠唤醒模式
        /// </summary>
        public byte SleepWakeMode { get; set; }
        /// <summary>
        /// 唤醒条件类型
        /// </summary>
        public byte WakeConditionType  { get; set; }
        /// <summary>
        /// 定时唤醒日设置
        /// </summary>
        public byte TimerWakeDaySet { get; set; }
        /// <summary>
        /// 日定时唤醒参数列表
        /// </summary>
        public JT808_0x8103_0x007C_TimerWakeDayParamter TimerWakeDayParamter { get; set; }
    }
}
