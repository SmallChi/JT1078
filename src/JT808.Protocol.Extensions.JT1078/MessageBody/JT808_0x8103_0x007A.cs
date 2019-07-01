using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频相关报警屏蔽字
    /// 0x8103_0x007A
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x007A_Formatter))]
    public class JT808_0x8103_0x007A : JT808_0x8103_CustomBodyBase
    {
        public override uint ParamId { get; set; } = 0x007A;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 视频相关屏蔽报警字
        /// </summary>
        public uint AlarmShielding { get; set; }
    }
}
