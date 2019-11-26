using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 平台下发远程录像回放请求
    /// </summary>
    public class JT808_0x9201 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9201>
    {
        public override ushort MsgId => 0x9201;
        /// <summary>
        /// 服务器IP地址服务
        /// </summary>
        public byte ServerIpLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 服务器音视频通道监听端口号TCP
        /// </summary>
        public ushort TcpPort { get; set; }
        /// <summary>
        /// 服务器音视频通道监听端口号UDP
        /// </summary>
        public ushort UdpPort { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVItemType { get; set; }
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储器类型
        /// </summary>
        public byte MemType { get; set; }
        /// <summary>
        /// 回放方式
        /// </summary>
        public byte PlayBackWay { get; set; }
        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        public byte FastForwardOrFastRewindMultiples1 { get; set; }
        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        public byte FastForwardOrFastRewindMultiples2 { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public JT808_0x9201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9201 jT808_0x9201 = new JT808_0x9201();
            jT808_0x9201.ServerIpLength = reader.ReadByte();
            jT808_0x9201.ServerIp = reader.ReadString(jT808_0x9201.ServerIpLength);
            jT808_0x9201.TcpPort = reader.ReadUInt16();
            jT808_0x9201.UdpPort = reader.ReadUInt16();
            jT808_0x9201.LogicChannelNo = reader.ReadByte();
            jT808_0x9201.AVItemType = reader.ReadByte();
            jT808_0x9201.StreamType = reader.ReadByte();
            jT808_0x9201.MemType = reader.ReadByte();
            jT808_0x9201.PlayBackWay = reader.ReadByte();
            jT808_0x9201.FastForwardOrFastRewindMultiples1 = reader.ReadByte();
            jT808_0x9201.FastForwardOrFastRewindMultiples2 = reader.ReadByte();
            jT808_0x9201.BeginTime = reader.ReadDateTime6();
            jT808_0x9201.EndTime = reader.ReadDateTime6();
            return jT808_0x9201;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9201 value, IJT808Config config)
        {
            writer.Skip(1, out int position);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);//计算完字符串后，回写字符串长度
            writer.WriteUInt16(value.TcpPort);
            writer.WriteUInt16(value.UdpPort);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteByte(value.PlayBackWay);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples1);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples2);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
        }
    }
}
