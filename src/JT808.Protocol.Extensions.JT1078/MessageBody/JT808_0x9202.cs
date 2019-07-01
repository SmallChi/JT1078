using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 平台下发远程录像回放控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9202_Formatter))]
    public class JT808_0x9202 : JT808Bodies
    {
        /// <summary>
        /// 音视频通道号
        /// </summary>
        public byte AVChannelNo { get; set; }
        /// <summary>
        /// 回放控制
        /// </summary>
        public byte PlayBackControl { get; set; }
        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        public byte FastForwardOrFastRewindMultiples { get; set; }
        /// <summary>
        /// 拖动回放位置
        /// </summary>
        public DateTime DragPlaybackPosition { get; set; }
    }
}
