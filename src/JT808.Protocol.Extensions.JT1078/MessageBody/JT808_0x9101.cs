using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频传输请求
    /// </summary>
    public class JT808_0x9101:JT808Bodies, IJT808MessagePackFormatter<JT808_0x9101>
    {
        public override string Description => "实时音视频传输请求";
        public override ushort MsgId => 0x9101;
        /// <summary>
        /// 服务器IP地址长度
        /// </summary>
        public byte ServerIPAddressLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIPAddress { get; set; }
        /// <summary>
        /// 服务器视频通道监听端口号(TCP)
        /// </summary>
        public ushort ServerVideoChannelTcpPort { get; set; }
        /// <summary>
        /// 服务器视频通道监听端口号（UDP）
        /// </summary>
        public ushort ServerVideoChannelUdpPort { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicalChannelNo { get; set; }
        /// <summary>
        /// 数据类型
        /// 0:音视频
        /// 1:视频
        /// 2:双向对讲
        /// 3:监听
        /// 4:中心广播
        /// 5:透传
        /// </summary>
        public byte DataType { get; set; }
        /// <summary>
        /// 码流类型
        /// 0:主码流
        /// 1:子码流
        /// </summary>
        public byte StreamType { get; set; }

        public JT808_0x9101 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9101 jT808_0X9101 = new JT808_0x9101();
            jT808_0X9101.ServerIPAddressLength = reader.ReadByte();
            jT808_0X9101.ServerIPAddress = reader.ReadString(jT808_0X9101.ServerIPAddressLength);
            jT808_0X9101.ServerVideoChannelTcpPort = reader.ReadUInt16();
            jT808_0X9101.ServerVideoChannelUdpPort = reader.ReadUInt16();
            jT808_0X9101.LogicalChannelNo = reader.ReadByte();
            jT808_0X9101.DataType = reader.ReadByte();
            jT808_0X9101.StreamType = reader.ReadByte();
            return jT808_0X9101;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9101 value, IJT808Config config)
        {
            writer.Skip(1, out int position);
            writer.WriteString(value.ServerIPAddress);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
            writer.WriteUInt16(value.ServerVideoChannelTcpPort);
            writer.WriteUInt16(value.ServerVideoChannelUdpPort);
            writer.WriteByte(value.LogicalChannelNo);
            writer.WriteByte(value.DataType);
            writer.WriteByte(value.StreamType);
        }
    }
}
