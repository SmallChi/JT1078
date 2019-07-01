using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 终端上传音视频属性
    /// </summary>
    [JT808Formatter(typeof(JT808_0x1003_Formatter))]
    public class JT808_0x1003 : JT808Bodies
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
    }
}
