using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询资源列表
    /// </summary>
    public class JT808_0x9205 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9205>, IJT808Analyze
    {
        public override string Description => "查询资源列表";
        public override ushort MsgId => 0x9205;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime  { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警标志
        /// </summary>
        public ulong AlarmFlag { get; set; }
        /// <summary>
        /// 音视频资源类型
        /// </summary>
        public byte AVResourceType { get; set; }
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储器类型
        /// </summary>
        public byte MemoryType { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9205 value = new JT808_0x9205();
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.BeginTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]起始时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.AlarmFlag = reader.ReadUInt64();
            writer.WriteNumber($"[{value.AlarmFlag.ReadNumber()}]报警标志", value.AlarmFlag);
            value.AVResourceType = reader.ReadByte();
            writer.WriteString($"[{value.AVResourceType.ReadNumber()}]音视频类型", AVResourceTypeDisplay(value.AVResourceType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemoryType = reader.ReadByte();
            writer.WriteString($"[{value.MemoryType.ReadNumber()}]存储器类型", MemoryTypeDisplay(value.MemoryType));

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
                    case 3:
                        return "音频或视频";
                    default:
                        return "未知";
                }
            }
            string StreamTypeDisplay(byte StreamType)
            {
                switch (StreamType)
                {
                    case 0:
                        return "所有码流";
                    case 1:
                        return "主码流";
                    case 2:
                        return "子码流";
                    default:
                        return "未知";
                }
            }
            string MemoryTypeDisplay(byte MemType)
            {
                switch (MemType)
                {
                    case 0:
                        return "所有存储器";
                    case 1:
                        return "主存储器";
                    case 2:
                        return "灾备服务器";
                    default:
                        return "未知";
                }
            }
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
        }

        public JT808_0x9205 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9205 jT808_0x9205 = new JT808_0x9205();
            jT808_0x9205.LogicChannelNo = reader.ReadByte();
            jT808_0x9205.BeginTime = reader.ReadDateTime6();
            jT808_0x9205.EndTime = reader.ReadDateTime6();
            jT808_0x9205.AlarmFlag = reader.ReadUInt64();
            jT808_0x9205.AVResourceType = reader.ReadByte();
            jT808_0x9205.StreamType = reader.ReadByte();
            jT808_0x9205.MemoryType = reader.ReadByte();
            return jT808_0x9205;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9205 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt64(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
        }
    }
}
