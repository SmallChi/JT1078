using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 存储器故障报警状态
    /// 0x0200_0x17
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0200_0x17_Formatter))]
    public class JT808_0x0200_0x17 : JT808_0x0200_BodyBase
    {
        public override byte AttachInfoId { get; set; } = 0x17;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 存储器故障报警状态
        /// </summary>
        public ushort StorageFaultAlarmStatus{ get; set; }
    }
}
