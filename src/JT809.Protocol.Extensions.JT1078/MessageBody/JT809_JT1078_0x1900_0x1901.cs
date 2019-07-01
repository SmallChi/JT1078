using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动上传音视频资源目录信息消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1900_0x1901_Formatter))]
    public class JT809_JT1078_0x1900_0x1901 : JT809SubBodies
    {
        /// <summary>
        /// 资源目录项数目
        /// </summary>
        public uint ItemNum { get; set; }
        /// <summary>
        /// 资源目录项列表
        /// </summary>
        public List<JT809_JT1078_0x1900_Record> ItemList { get; set; }
    }
}