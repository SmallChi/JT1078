using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频资源列表
    /// </summary>
    [JT808Formatter(typeof(JT808_0x1205_Formatter))]
    public class JT808_0x1205 : JT808Bodies
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 音视频资源总数
        /// </summary>
        public uint AVResouceTotal{ get; set; }
        /// <summary>
        /// 音视频资源列表
        /// </summary>
        public List<JT808_0x1205_AVResouce> AVResouces { get; set; }
    }
}
