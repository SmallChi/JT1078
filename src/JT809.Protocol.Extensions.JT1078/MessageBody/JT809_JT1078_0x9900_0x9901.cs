using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动上传音视频资源目录信息应答消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9900_0x9901_Formatter))]
    public class JT809_JT1078_0x9900_0x9901 : JT809SubBodies
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 资源目录总数
        /// </summary>
        public byte ItemNumber { get; set; }
    }
}