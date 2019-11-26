using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询资源列表
    /// </summary>
    public class JT808_0x9205 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9205>
    {
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
        public uint AlarmFlag { get; set; }
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

        public JT808_0x9205 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9205 jT808_0x9205 = new JT808_0x9205();
            jT808_0x9205.LogicChannelNo = reader.ReadByte();
            jT808_0x9205.BeginTime = reader.ReadDateTime6();
            jT808_0x9205.EndTime = reader.ReadDateTime6();
            jT808_0x9205.AlarmFlag = reader.ReadUInt32();
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
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
        }
    }
}
