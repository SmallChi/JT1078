using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频属性
    /// </summary>
    public class JT808_0x1003 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x1003>
    {
        /// <summary>
        /// 输入音频编码方式
        /// </summary>
        public byte EnterAudioEncoding { get; set; }
        /// <summary>
        /// 输入音频声道数
        /// </summary>
        public byte EnterAudioChannelsNumber { get; set; }
        /// <summary>
        /// 输入音频采样率
        /// </summary>
        public byte EnterAudioSampleRate  { get; set; }
        /// <summary>
        /// 输入音频采样位数
        /// </summary>
        public byte EnterAudioSampleDigits { get; set; }
        /// <summary>
        /// 音频帧长度
        /// </summary>
        public ushort AudioFrameLength { get; set; }
        /// <summary>
        /// 是否支持音频输出
        /// </summary>
        public byte IsSupportedAudioOutput { get; set; }
        /// <summary>
        /// 视频编码方式
        /// </summary>
        public byte VideoEncoding { get; set; }
        /// <summary>
        /// 终端支持的最大音频物理通道数量
        /// </summary>
        public byte TerminalSupportedMaxNumberOfAudioPhysicalChannels{ get; set; }
        /// <summary>
        /// 终端支持的最大视频物理通道数量
        /// </summary>
        public byte TerminalSupportedMaxNumberOfVideoPhysicalChannels { get; set; }

        public override ushort MsgId => 0x1003;

        public override string Description => "终端上传音视频属性";

        public JT808_0x1003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1003 jT808_0x1003 = new JT808_0x1003();
            jT808_0x1003.EnterAudioEncoding = reader.ReadByte();
            jT808_0x1003.EnterAudioChannelsNumber = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleRate = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleDigits = reader.ReadByte();
            jT808_0x1003.AudioFrameLength = reader.ReadUInt16();
            jT808_0x1003.IsSupportedAudioOutput = reader.ReadByte();
            jT808_0x1003.VideoEncoding = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfAudioPhysicalChannels = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfVideoPhysicalChannels = reader.ReadByte();
            return jT808_0x1003;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1003 value, IJT808Config config)
        {
            writer.WriteByte(value.EnterAudioEncoding);
            writer.WriteByte(value.EnterAudioChannelsNumber);
            writer.WriteByte(value.EnterAudioSampleRate);
            writer.WriteByte(value.EnterAudioSampleDigits);
            writer.WriteUInt16(value.AudioFrameLength);
            writer.WriteByte(value.IsSupportedAudioOutput);
            writer.WriteByte(value.VideoEncoding);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfAudioPhysicalChannels);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfVideoPhysicalChannels);
        }
    }
}
