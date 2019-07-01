using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动请求停止实时音视频传输消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9800_0x9802_Formatter))]
    public class JT809_JT1078_0x9800_0x9802 : JT809SubBodies
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVitemType { get; set; }
    }
}