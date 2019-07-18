using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 异常驾驶行为报警详细描述
    /// 0x0200_0x18
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0200_0x18_Formatter))]
    public class JT808_0x0200_0x18 : JT808_0x0200_BodyBase
    {
        public override byte AttachInfoId { get; set; } = 0x18;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 异常驾驶行为报警详细描述
        /// </summary>
        public ushort AbnormalDrivingBehaviorAlarmInfo { get; set; }
    }
}
