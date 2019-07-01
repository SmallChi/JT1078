using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 特殊报警录像参数设置
    /// 0x8103_0x0079
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0079_Formatter))]
    public class JT808_0x8103_0x0079 : JT808_0x8103_CustomBodyBase
    {
        public override uint ParamId { get; set; } = 0x0079;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 3;
        /// <summary>
        /// 特殊报警录像存储阈值
        /// </summary>
        public byte StorageThresholds  { get; set; }
        /// <summary>
        /// 特殊报警录像持续时间
        /// </summary>
        public byte Duration { get; set; }
        /// <summary>
        /// 特殊报警标识起始时间
        /// 分钟min
        /// </summary>
        public byte BeginMinute { get; set; }
    }
}
