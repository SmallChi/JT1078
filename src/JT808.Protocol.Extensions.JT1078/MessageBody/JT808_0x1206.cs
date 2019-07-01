using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传完成通知
    /// </summary>
    [JT808Formatter(typeof(JT808_0x1206_Formatter))]
    public class JT808_0x1206 : JT808Bodies
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public byte Result{ get; set; }
    }
}
