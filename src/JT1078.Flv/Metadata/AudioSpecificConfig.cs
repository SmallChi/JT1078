using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class AudioSpecificConfig
    {
        public AudioObjectType AudioType { get; set; } = AudioObjectType.AAC_LC;
        public SamplingFrequency SamplingFrequencyIndex { get; set; } = SamplingFrequency.Index_8000;
        /// <summary>
        /// 其实有很多，这里就固定为立体声
        /// </summary>
        public int ChannelConfiguration { get; set; } = 1;

        public byte[] ToArray()
        {
            var value = Convert.ToInt16($"{Convert.ToString((int)AudioType, 2).PadLeft(5, '0')}{Convert.ToString((int)SamplingFrequencyIndex, 2).PadLeft(4, '0')}{Convert.ToString(ChannelConfiguration, 2).PadLeft(4, '0')}000", 2);
            return new byte[] { (byte)(value >> 8), (byte)value, 0x56, 0xe5, 0x00 };
        }
        /// <summary>
        /// 音频类型
        /// 其实有很多，这里就列几个，如有需要再加
        /// </summary>
        public enum AudioObjectType
        {
            AAC_MAIN = 1,
            AAC_LC = 2,
            AAC_SSR = 3,
            AAC_LTP = 4,
            SBR = 5,
            AAC_SCALABLE
        }
        public enum SamplingFrequency
        {
            Index_96000 = 0x00,
            Index_88200 = 0x01,
            Index_64000 = 0x02,
            Index_48000 = 0x03,
            Index_44100 = 0x04,
            Index_32000 = 0x05,
            Index_24000 = 0x06,
            Index_22050 = 0x07,
            Index_16000 = 0x08,
            Index_12000 = 0x09,
            Index_11025 = 0x0a,
            Index_8000 = 0x0b,
            Index_7350 = 0x0c,
            ESCAPE = 0x0f
        }
    }
}
