using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频传输状态通知
    /// </summary>
    public class JT808_0x9105 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9105>
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
