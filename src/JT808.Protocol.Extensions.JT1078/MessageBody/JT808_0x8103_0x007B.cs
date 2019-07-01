using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 图像分析报警参数设置
    /// 0x8103_0x007B
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x007B_Formatter))]
    public class JT808_0x8103_0x007B : JT808_0x8103_CustomBodyBase
    {
        public override uint ParamId { get; set; } = 0x007B;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆核载人数
        /// </summary>
        public byte NuclearLoadNumber { get; set; }
        /// <summary>
        /// 疲劳程度阈值
        /// </summary>
        public byte FatigueThreshold  { get; set; }
    }
}
