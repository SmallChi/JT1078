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
    /// 实时音视频传输请求
    /// </summary>
    public class JT808_0x9101: JT808Bodies, IJT808MessagePackFormatter<JT808_0x9101>, IJT808Analyze
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

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9101 value = new JT808_0x9101();
            value.ServerIPAddressLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ServerIPAddressLength.ReadNumber()}]服务器IP地址长度", value.ServerIPAddressLength);
            string ipHex = reader.ReadVirtualArray(value.ServerIPAddressLength).ToArray().ToHexString();
            value.ServerIPAddress = reader.ReadString(value.ServerIPAddressLength);
            writer.WriteString($"[{ipHex}]服务器IP地址", value.ServerIPAddress);
            value.ServerVideoChannelTcpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ServerVideoChannelTcpPort.ReadNumber()}]服务器视频通道监听端口号(TCP)", value.ServerVideoChannelTcpPort);
            value.ServerVideoChannelUdpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ServerVideoChannelUdpPort.ReadNumber()}]服务器视频通道监听端口号（UDP）", value.ServerVideoChannelUdpPort);
            value.LogicalChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicalChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicalChannelNo));
            value.DataType = reader.ReadByte();
            writer.WriteString($"[{value.DataType.ReadNumber()}]数据类型", DataTypeDisplay(value.DataType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", value.StreamType==0?"主码流":"子码流");
            string DataTypeDisplay(byte DataType) {
                switch (DataType)
                {
                    case 0:
                        return "音视频";
                    case 1:
                        return "视频";
                    case 2:
                        return "双向对讲";
                    case 3:
                        return "监听";
                    case 4:
                        return "中心广播";
                    case 5:
                        return "透传";
                    default:
                        break;
                }

                return "未知";
            }
            string LogicalChannelNoDisplay(byte LogicalChannelNo) {
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
                        return  "行李舱";
                    case 8:
                        return  "车辆左侧";
                    case 9:
                        return  "车辆右侧";
                    case 10:
                        return  "车辆正后方";
                    case 11:
                        return  "车厢中部";
                    case 12:
                        return  "车中门";
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
