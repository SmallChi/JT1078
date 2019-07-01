using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频信号丢失报警状态
    /// 0x0200_0x15
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0200_0x15_Formatter))]
    public class JT808_0x0200_0x15 : JT808_0x0200_CustomBodyBase
    {
        public override byte AttachInfoId { get; set; } = 0x15;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 视频信号丢失报警状态
        /// </summary>
        public uint VideoSignalLoseAlarmStatus { get; set; }
    }
}
