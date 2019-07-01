using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传乘客流量
    /// </summary>
    [JT808Formatter(typeof(JT808_0x1005_Formatter))]
    public class JT808_0x1005 : JT808Bodies
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 上车人数
        /// </summary>
        public ushort GettingOnNumber { get; set; }
        /// <summary>
        /// 下车人数
        /// </summary>
        public ushort GettingOffNumber { get; set; }
    }
}
