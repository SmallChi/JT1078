using JT1078.Flv.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class AudioTags
    {
        public AudioTags(AACPacketType packetType = AACPacketType.AudioSpecificConfig, byte[] aacFrameData = null)
        {
            AacPacke = new AacPacke(packetType, aacFrameData);
        }
        /// <summary>
        /// 采样率
        /// AAC固定为3
        /// 0 = 5.5-kHz
        /// 1 = 11-kHz
        /// 2 = 22-kHz
        /// 3 = 44-kHz
        /// </summary>
        public int SampleRate => 3;
        /// <summary>
        /// 采样位深
        /// </summary>
        public SampleBit SampleBit { get; set; } = SampleBit.Bit_16;
        /// <summary>
        /// 声道
        /// AAC永远是1
        /// </summary>
        public ChannelType Channel => ChannelType.Stereo;
        /// <summary>
        /// 音频格式
        /// </summary>
        public AudioFormat SoundType => AudioFormat.AAC;

        /// <summary>
        /// 元数据
        /// </summary>
        private AacPacke AacPacke { get; set; }

        public byte[] ToArray()
        {
            var value = $"{Convert.ToString((int)SoundType, 2).PadLeft(4, '0')}{Convert.ToString(SampleRate, 2).PadLeft(2, '0')}{(int)SampleBit}{(int)Channel}";
            var data = new List<byte>
            {
                Convert.ToByte(value, 2)
            };
            data.AddRange(AacPacke.RawData);
            return data.ToArray();
        }
    }
}
