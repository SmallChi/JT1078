using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///终端休眠模式唤醒设置
    /// 0x8103_0x007C
    /// </summary>
    public class JT808_0x8103_0x007C : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x007C>
    {
        public override uint ParamId { get; set; } = 0x007C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 20;
        /// <summary>
        /// 休眠唤醒模式
        /// </summary>
        public byte SleepWakeMode { get; set; }
        /// <summary>
        /// 唤醒条件类型
        /// </summary>
        public byte WakeConditionType  { get; set; }
        /// <summary>
        /// 定时唤醒日设置
        /// </summary>
        public byte TimerWakeDaySet { get; set; }
        /// <summary>
        /// 日定时唤醒参数列表
        /// </summary>
        public JT808_0x8103_0x007C_TimerWakeDayParamter TimerWakeDayParamter { get; set; }

        public JT808_0x8103_0x007C Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007C jT808_0x8103_0x007C = new JT808_0x8103_0x007C();
            jT808_0x8103_0x007C.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007C.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007C.SleepWakeMode = reader.ReadByte();
            jT808_0x8103_0x007C.WakeConditionType = reader.ReadByte();
            jT808_0x8103_0x007C.TimerWakeDaySet = reader.ReadByte();
            jT808_0x8103_0x007C.TimerWakeDayParamter = config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Deserialize(ref reader, config);
            return jT808_0x8103_0x007C;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007C value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out var position);
            writer.WriteByte(value.SleepWakeMode);
            writer.WriteByte(value.WakeConditionType);
            writer.WriteByte(value.TimerWakeDaySet);
            config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Serialize(ref writer, value.TimerWakeDayParamter, config);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
        }
    }
}
