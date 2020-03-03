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
    /// 平台下发远程录像回放请求
    /// </summary>
    public class JT808_0x9201 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9201>, IJT808Analyze
    {
        public override string Description => "平台下发远程录像回放请求";
        public override ushort MsgId => 0x9201;
        /// <summary>
        /// 服务器IP地址长度
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
        public byte FastForwardOrFastRewindMultiples { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9201 value = new JT808_0x9201();
            value.ServerIpLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ServerIpLength.ReadNumber()}]服务器IP地址长度", value.ServerIpLength);
            string ipHex = reader.ReadVirtualArray(value.ServerIpLength).ToArray().ToHexString();
            value.ServerIp = reader.ReadString(value.ServerIpLength);
            writer.WriteString($"[{ipHex}]服务器IP地址", value.ServerIp);
            value.TcpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TcpPort.ReadNumber()}]服务器视频通道监听端口号(TCP)", value.TcpPort);
            value.UdpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.UdpPort.ReadNumber()}]服务器视频通道监听端口号（UDP）", value.UdpPort);
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.AVItemType = reader.ReadByte();
            writer.WriteString($"[{value.AVItemType.ReadNumber()}]音视频类型", AVItemTypeDisplay(value.AVItemType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemType = reader.ReadByte();
            writer.WriteString($"[{value.MemType.ReadNumber()}]存储器类型", MemTypeDisplay(value.MemType));
            value.PlayBackWay = reader.ReadByte();
            writer.WriteString($"[{value.PlayBackWay.ReadNumber()}]回访方式", PlayBackWayDisplay(value.PlayBackWay));
            value.FastForwardOrFastRewindMultiples = reader.ReadByte();
            writer.WriteString($"[{value.FastForwardOrFastRewindMultiples.ReadNumber()}]快进或快退倍数", FastForwardOrFastRewindMultiplesDisplay(value.FastForwardOrFastRewindMultiples));
            value.BeginTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]结束时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            string AVItemTypeDisplay(byte AVItemType)
            {
                switch (AVItemType)
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
                        return "主码流或子码流";
                    case 1:
                        return "主码流";
                    case 2:
                        return "子码流";
                    default:
                        return "未知";
                }
            }
            string MemTypeDisplay(byte MemType)
            {
                switch (MemType)
                {
                    case 0:
                        return "主存储器或灾备服务器";
                    case 1:
                        return "主存储器";
                    case 2:
                        return "灾备服务器";
                    default:
                        return "未知";
                }
            }
            string PlayBackWayDisplay(byte PlayBackWay)
            {
                switch (PlayBackWay)
                {
                    case 0:
                        return "正常回放";
                    case 1:
                        return "快进回放";
                    case 2:
                        return "关键帧快退回访";
                    case 3:
                        return "关键帧播放";
                    case 4:
                        return "单帧上传";
                    default:
                        return "未知";
                }
            }
            string FastForwardOrFastRewindMultiplesDisplay(byte FastForwardOrFastRewindMultiples)
            {
                switch (FastForwardOrFastRewindMultiples)
                {
                    case 0:
                        return "无效";
                    case 1:
                        return "1倍";
                    case 2:
                        return "2倍";
                    case 3:
                        return "4倍";
                    case 4:
                        return "8倍";
                    case 5:
                        return "16倍";
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
            jT808_0x9201.FastForwardOrFastRewindMultiples = reader.ReadByte();
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
            writer.WriteByte(value.FastForwardOrFastRewindMultiples);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
        }
    }
}
