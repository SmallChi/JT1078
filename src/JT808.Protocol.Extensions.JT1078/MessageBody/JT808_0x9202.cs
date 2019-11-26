using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 平台下发远程录像回放控制
    /// </summary>
    public class JT808_0x9202 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9202>
    {
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
