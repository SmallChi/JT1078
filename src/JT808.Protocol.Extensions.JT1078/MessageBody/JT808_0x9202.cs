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
    /// 平台下发远程录像回放控制
    /// </summary>
    public class JT808_0x9202 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9202>, IJT808Analyze
    {
        public override string Description => "平台下发远程录像回放控制";
        public override ushort MsgId => 0x9202;
        /// <summary>
        /// 音视频通道号
        /// </summary>
        public byte AVChannelNo { get; set; }
        /// <summary>
        /// 回放控制
        /// </summary>
        public byte PlayBackControl { get; set; }
        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        public byte FastForwardOrFastRewindMultiples { get; set; }
        /// <summary>
        /// 拖动回放位置
        /// </summary>
        public DateTime DragPlaybackPosition { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9202 value = new JT808_0x9202();
            value.AVChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.AVChannelNo.ReadNumber()}]音视频通道号", AVChannelNoDisplay(value.AVChannelNo));
            value.PlayBackControl = reader.ReadByte();
            writer.WriteString($"[{value.PlayBackControl.ReadNumber()}]回放控制", PlayBackControlDisplay(value.PlayBackControl));
            value.FastForwardOrFastRewindMultiples = reader.ReadByte();
            writer.WriteString($"[{value.FastForwardOrFastRewindMultiples.ReadNumber()}]快进或快退倍数", FastForwardOrFastRewindMultiplesDisplay(value.FastForwardOrFastRewindMultiples));
            value.DragPlaybackPosition = reader.ReadDateTime6();
            writer.WriteString($"[{value.DragPlaybackPosition.ToString("yyMMddHHmmss")}]拖动回放位置", value.DragPlaybackPosition.ToString("yyyy-MM-dd HH:mm:ss"));
            string AVChannelNoDisplay(byte LogicalChannelNo)
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
            string PlayBackControlDisplay(byte PlayBackControl) {
                switch (PlayBackControl)
                {
                    case 0:
                        return "开始回放";
                    case 1:
                        return "暂停回放";
                    case 2:
                        return "结束回放";
                    case 3:
                        return "快进回放";
                    case 4:
                        return "关键帧快退回放";
                    case 5:
                        return "拖动回放";
                    case 6:
                        return "关键帧播放";
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
        }

        public JT808_0x9202 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9202 jT808_0x9202 = new JT808_0x9202();
            jT808_0x9202.AVChannelNo = reader.ReadByte();
            jT808_0x9202.PlayBackControl = reader.ReadByte();
            jT808_0x9202.FastForwardOrFastRewindMultiples = reader.ReadByte();
            jT808_0x9202.DragPlaybackPosition = reader.ReadDateTime6();
            return jT808_0x9202;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9202 value, IJT808Config config)
        {
            writer.WriteByte(value.AVChannelNo);
            writer.WriteByte(value.PlayBackControl);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples);
            writer.WriteDateTime6(value.DragPlaybackPosition);
        }
    }
}
