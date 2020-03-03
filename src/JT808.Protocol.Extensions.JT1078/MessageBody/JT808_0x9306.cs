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
    /// 云台变倍控制
    /// </summary>
    public class JT808_0x9306 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9306>, IJT808Analyze
    {
        public override string Description => "云台变倍控制";
        public override ushort MsgId => 0x9306;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 变倍控制
        /// </summary>
        public byte ChangeMultipleControl { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9306 value = new JT808_0x9306();
            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.ChangeMultipleControl = reader.ReadByte();
            writer.WriteString($"[{value.ChangeMultipleControl.ReadNumber()}]变倍控制", value.ChangeMultipleControl == 0 ? "调大" : "调小");
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

        public JT808_0x9306 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9306 jT808_0x9306 = new JT808_0x9306();
            jT808_0x9306.LogicChannelNo = reader.ReadByte();
            jT808_0x9306.ChangeMultipleControl = reader.ReadByte();
            return jT808_0x9306;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9306 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.ChangeMultipleControl);
        }
    }
}
