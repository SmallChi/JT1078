using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动请求停止实时音视频传输应答消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1800_0x1802_Formatter))]
    public class JT809_JT1078_0x1800_0x1802 : JT809SubBodies
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
    }
}