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
    /// 云台旋转
    /// </summary>
    public class JT808_0x9301 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9301>, IJT808Analyze
    {
        public override string Description => "云台旋转";
        public override ushort MsgId => 0x9301;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public byte Direction { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public byte Speed { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9301 value = new JT808_0x9301();
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.Direction = reader.ReadByte();
            writer.WriteString($"[{value.Direction.ReadNumber()}]方向", DirectionDisplay(value.Direction));
            value.Speed = reader.ReadByte();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]速度", value.Speed);
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
            string DirectionDisplay(byte Direction) {
                switch (Direction)
                {
                    case 0:
                        return "停止";
                    case 1:
                        return "上";
                    case 2:
                        return "下";
                    case 3:
                        return "左";
                    case 4:
                        return "右";
                    default:
                        return "未知";
                }
            }
        }

        public JT808_0x9301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9301 jT808_0x9301 = new JT808_0x9301();
            jT808_0x9301.LogicChannelNo = reader.ReadByte();
            jT808_0x9301.Direction = reader.ReadByte();
            jT808_0x9301.Speed = reader.ReadByte();
            return jT808_0x9301;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9301 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.Direction);
            writer.WriteByte(value.Speed);
        }
    }
}
