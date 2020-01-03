using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Enums
{
    /// <summary>
    /// 音频格式
    /// </summary>
    public enum AudioFormat
    {
        /// <summary>
        /// Linear PCM, platform endian
        /// </summary>
        Pcm_Platform = 0,
        /// <summary>
        /// ADPCM
        /// </summary>
        ADPCM = 1,
        /// <summary>
        /// MP3
        /// </summary>
        MP3,
        /// <summary>
        /// Linear PCM, little endian
        /// </summary>
        Pcm_Little = 3,
        /// <summary>
        ///  16-kHz mono
        /// </summary>
        Nellymoser_16Khz = 4,
        /// <summary>
        ///  8-kHz mono
        /// </summary>
        Nellymoser_8Khz = 5,
        /// <summary>
        /// Nellymoser
        /// </summary>
        Nellymoser = 6,
        /// <summary>
        ///  A-law logarithmic PCM
        /// </summary>
        G711_A_law = 7,
        /// <summary>
        /// mu-law logarithmic PCM
        /// </summary>
        G711_mu_law = 8,
        /// <summary>
        /// AAC
        /// </summary>
        AAC = 10,
        /// <summary>
        /// Speex
        /// </summary>
        Speex = 11,
        /// <summary>
        /// MP3 8-Khz
        /// </summary>
        MP3_8Khz = 14
    }
}
