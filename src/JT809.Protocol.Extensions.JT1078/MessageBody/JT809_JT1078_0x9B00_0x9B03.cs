using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载控制消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9B00_0x9B03_Formatter))]
    public class JT809_JT1078_0x9B00_0x9B03 : JT809SubBodies
    {
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public byte Type { get; set; }
    }
}