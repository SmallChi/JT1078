using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询音视频资源目录请求消息
    /// </summary>
    public class JT809_JT1078_0x9900_0x9902 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9900_0x9902>, IJT809Analyze
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
        public string AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内任一位置，仅跨域访问请求时，使用此字段
        /// 36
        /// </summary>
        public byte[] GnssData { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.查询音视频资源目录请求消息.ToUInt16Value();

        public override string Description { get; } = "查询音视频资源目录请求消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9902 value = new JT809_JT1078_0x9900_0x9902();
            value.ChannelId = reader.ReadByte();
            writer.WriteString($"[{value.ChannelId.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.ChannelId));
            var virtualHex = reader.ReadVirtualArray(8);
            value.StartTime = reader.ReadUTCDateTime();
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]开始时间", value.StartTime);
            virtualHex = reader.ReadVirtualArray(8);
            value.EndTime = reader.ReadUTCDateTime();
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]结束时间", value.StartTime);
            value.AlarmType = reader.ReadUInt64();
            writer.WriteNumber($"[{value.AlarmType.ReadNumber()}]报警标志物", value.AlarmType);
            value.AVItemType = reader.ReadByte();
            writer.WriteString($"[{value.AVItemType.ReadNumber()}]音视频资源类型", AVResourceTypeDisplay(value.AVItemType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemType = reader.ReadByte();
            writer.WriteString($"[{value.MemType.ReadNumber()}]存储器类型", MemoryTypeDisplay(value.MemType));
            virtualHex = reader.ReadVirtualArray(64);
            value.AuthorizeCode = reader.ReadString(64);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]时效口令", value.AuthorizeCode);
            virtualHex = reader.ReadVirtualArray(36);
            value.GnssData = reader.ReadArray(36).ToArray();
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]车辆进入跨域地区后5min之内任一位置", virtualHex.ToArray().ToHexString());
            string LogicalChannelNoDisplay(byte LogicalChannelNo)
            {
                switch (LogicalChannelNo)
                {
                    case 1:
                        return "驾驶员";
                    case 2:
                        return "车辆正前方";
                    case 3:
                        return "车前门";
                    case 4:
                        return "车厢前部";
                    case 5:
                        return "车厢后部";
                    case 7:
                        return "行李舱";
                    case 8:
                        return "车辆左侧";
                    case 9:
                        return "车辆右侧";
                    case 10:
                        return "车辆正后方";
                    case 11:
                        return "车厢中部";
                    case 12:
                        return "车中门";
                    case 13:
                        return "驾驶席车门";
                    case 33:
                        return "驾驶员";
                    case 36:
                        return "车厢前部";
                    case 37:
                        return "车厢后部";
                    default:
                        return "预留";
                }
            }
            string AVResourceTypeDisplay(byte AVResourceType)
            {
                switch (AVResourceType)
                {
                    case 0:
                        return "音视频";
                    case 1:
                        return "音频";
                    case 2:
                        return "视频";
                    default:
                        break;
                }
                return "未知";
            }
            string StreamTypeDisplay(byte StreamType)
            {
                switch (StreamType)
                {
                    case 1:
                        return "主码流";
                    case 2:
                        return "子码流";
                    default:
                        return "未知";
                }
            }
            string MemoryTypeDisplay(byte MemoryType)
            {
                switch (MemoryType)
                {
                    case 1:
                        return "主存储器";
                    case 2:
                        return "灾备存储器";
                    default:
                        return "未知";
                }
            }
        }

        public JT809_JT1078_0x9900_0x9902 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9902 value = new JT809_JT1078_0x9900_0x9902();
            value.ChannelId = reader.ReadByte();
            value.StartTime = reader.ReadUTCDateTime();
            value.EndTime = reader.ReadUTCDateTime();
            value.AlarmType = reader.ReadUInt64();
            value.AVItemType = reader.ReadByte();
            value.StreamType = reader.ReadByte();
            value.MemType = reader.ReadByte();
            value.AuthorizeCode = reader.ReadString(64);
            value.GnssData = reader.ReadArray(36).ToArray();
            return value;
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
            writer.WriteStringPadRight(value.AuthorizeCode, 64);
            writer.WriteArray(value.GnssData);
        }
    }
}