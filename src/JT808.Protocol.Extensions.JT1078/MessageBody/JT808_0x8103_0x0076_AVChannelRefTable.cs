using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 音视频通道列表设置
    /// 0x8103_0x0076_AVChannelRefTable
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0076_AVChannelRefTable_Formatter))]
    public class JT808_0x8103_0x0076_AVChannelRefTable
    {
        /// <summary>
        /// 物理通道号
        /// </summary>
        public byte PhysicalChannelNo { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 通道类型
        /// </summary>
        public byte ChannelType { get; set; }
        /// <summary>
        /// 是否链接云台
        /// </summary>
        public byte IsConnectCloudPlat { get; set; }
    }
}
