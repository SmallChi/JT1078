using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///单独视频通道参数设置
    /// 0x8103_0x0077
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0077_Formatter))]
    public class JT808_0x8103_0x0077 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0077;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 需单独设置视频参数的通道数量 用n表示
        /// </summary>
        public byte NeedSetChannelTotal { get; set; }

        public List<JT808_0x8103_0x0077_SignalChannel> SignalChannels { get; set; }
    }
}
