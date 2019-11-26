using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 音视频通道列表设置
    /// 0x8103_0x0076_AVChannelRefTable
    /// </summary>
    public class JT808_0x8103_0x0076_AVChannelRefTable: IJT808MessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>
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
        public JT808_0x8103_0x0076_AVChannelRefTable Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0076_AVChannelRefTable jT808_0X8103_0X0076_AVChannelRefTable = new JT808_0x8103_0x0076_AVChannelRefTable();
            jT808_0X8103_0X0076_AVChannelRefTable.PhysicalChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.LogicChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.ChannelType = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.IsConnectCloudPlat = reader.ReadByte();
            return jT808_0X8103_0X0076_AVChannelRefTable;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0076_AVChannelRefTable value, IJT808Config config)
        {
            writer.WriteByte(value.PhysicalChannelNo);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.ChannelType);
            writer.WriteByte(value.IsConnectCloudPlat);
        }
    }
}
