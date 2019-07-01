using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9207_Formatter))]
    public class JT808_0x9207 : JT808Bodies
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MgsNum { get; set; }
        /// <summary>
        /// 上传控制
        /// </summary>
        public byte UploadControl { get; set; }
    }
}
