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
    /// 实时音视频传输状态通知
    /// </summary>
    public class JT808_0x9105 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9105>, IJT808Analyze
    {
        public override string Description => "实时音视频传输状态通知";
        public override ushort MsgId => 0x9105;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 丢包率
        /// </summary>
        public byte DropRate  { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9105 value = new JT808_0x9105();
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.DropRate = reader.ReadByte();
            writer.WriteNumber($"[{value.DropRate.ReadNumber()}]丢包率", value.DropRate);
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

        public JT808_0x9105 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9105 jT808_0x9105 = new JT808_0x9105();
            jT808_0x9105.LogicChannelNo = reader.ReadByte();
            jT808_0x9105.DropRate = reader.ReadByte();
            return jT808_0x9105;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9105 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.DropRate);
        }
    }
}
