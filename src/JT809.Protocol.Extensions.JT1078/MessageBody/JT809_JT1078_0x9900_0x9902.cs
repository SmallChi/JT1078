using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询音视频资源目录请求消息
    /// </summary>
    public class JT809_JT1078_0x9900_0x9902 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9900_0x9902>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public ulong AlarmType { get; set; }
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
        /// 时效口令
        /// 64
        /// </summary>
        public byte[] AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内任一位置，仅跨域访问请求时，使用此字段
        /// 36
        /// </summary>
        public byte[] GnssData { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.查询音视频资源目录请求消息.ToUInt16Value();

        public override string Description { get; } = "查询音视频资源目录请求消息";

        public JT809_JT1078_0x9900_0x9902 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9902 jT808_JT1078_0x9900_0x9902 = new JT809_JT1078_0x9900_0x9902();
            jT808_JT1078_0x9900_0x9902.ChannelId = reader.ReadByte();
#warning 此处时间8个字节，暂使用utc时间代替
            jT808_JT1078_0x9900_0x9902.StartTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9900_0x9902.EndTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9900_0x9902.AlarmType = reader.ReadUInt64();
            jT808_JT1078_0x9900_0x9902.AVItemType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.StreamType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.MemType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9900_0x9902.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9900_0x9902;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9900_0x9902 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUTCDateTime(value.StartTime);
            writer.WriteUTCDateTime(value.EndTime);
            writer.WriteUInt64(value.AlarmType);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}