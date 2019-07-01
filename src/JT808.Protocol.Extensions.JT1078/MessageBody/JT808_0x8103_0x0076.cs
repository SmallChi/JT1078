using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
#warning todo:继承非自定义类的时候，没有添加到相应的字典中
    /// <summary>
    /// 音视频通道列表设置
    /// 0x8103_0x0076
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0076_Formatter))]
    public class JT808_0x8103_0x0076 : JT808_0x8103_CustomBodyBase
    {
        public override uint ParamId { get; set; } = 0x0076;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 音视频通道总数
        /// l
        /// </summary>
        public byte AVChannelTotal { get; set; }
        /// <summary>
        /// 音频通道总数
        /// m
        /// </summary>
        public byte AudioChannelTotal { get; set; }
        /// <summary>
        /// 视频通道总数
        /// n
        /// </summary>
        public byte VudioChannelTotal { get; set; }
        /// <summary>
        /// 音视频通道对照表
        /// 4*(l+m+n)
        /// </summary>
        public List<JT808_0x8103_0x0076_AVChannelRefTable> AVChannelRefTables { get; set; }
    }
}
