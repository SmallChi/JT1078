using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载请求应答消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1B00_0x1B01_Formatter))]
    public class JT809_JT1078_0x1B00_0x1B01 : JT809SubBodies
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }
    }
}