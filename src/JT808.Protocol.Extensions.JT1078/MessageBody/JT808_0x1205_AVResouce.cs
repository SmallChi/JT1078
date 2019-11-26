using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频资源列表
    /// </summary>
    public class JT808_0x1205_AVResouce:IJT808MessagePackFormatter<JT808_0x1205_AVResouce>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
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
        /// <summary>
        /// 文件大小
        /// </summary>
        public uint FileSize { get; set; }

        public JT808_0x1205_AVResouce Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1205_AVResouce jT808_0x1205_AVResouce = new JT808_0x1205_AVResouce();
            jT808_0x1205_AVResouce.LogicChannelNo = reader.ReadByte();
            jT808_0x1205_AVResouce.BeginTime = reader.ReadDateTime6();
            jT808_0x1205_AVResouce.EndTime = reader.ReadDateTime6();
            jT808_0x1205_AVResouce.AlarmFlag = reader.ReadUInt32();
            jT808_0x1205_AVResouce.AVResourceType = reader.ReadByte();
            jT808_0x1205_AVResouce.StreamType = reader.ReadByte();
            jT808_0x1205_AVResouce.MemoryType = reader.ReadByte();
            jT808_0x1205_AVResouce.FileSize = reader.ReadUInt32();
            return jT808_0x1205_AVResouce;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1205_AVResouce value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryType);
            writer.WriteUInt32(value.FileSize);
        }
    }
}
