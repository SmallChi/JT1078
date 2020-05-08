using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像回放请求消息
    /// </summary>
    public class JT809_JT1078_0x9A00_0x9A01 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9A00_0x9A01>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
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
        /// 回放起始时间
        /// </summary>
        public DateTime PlayBackStartTime { get; set; }
        /// <summary>
        /// 回放结束时间
        /// </summary>
        public DateTime PlayBackEndTime { get; set; }
        /// <summary>
        /// 时效口令
        /// 64
        /// </summary>
        public byte[] AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内任一位置，仅跨域访问请求时，使用此字段
        /// 36
        /// </summary>
        public byte[] GnssData { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像回放请求消息.ToUInt16Value();

        public override string Description { get; } = "远程录像回放请求消息";

        public JT809_JT1078_0x9A00_0x9A01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A01 jT808_JT1078_0x9A00_0x9A01 = new JT809_JT1078_0x9A00_0x9A01();
            jT808_JT1078_0x9A00_0x9A01.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.AVItemType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.StreamType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.MemType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.PlayBackStartTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9A00_0x9A01.PlayBackEndTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9A00_0x9A01.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9A00_0x9A01.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9A00_0x9A01;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9A00_0x9A01 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteUTCDateTime(value.PlayBackStartTime);
            writer.WriteUTCDateTime(value.PlayBackEndTime);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}