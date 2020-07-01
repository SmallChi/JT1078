using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Enums
{
    /// <summary>
    /// 音视频类型
    /// </summary>
    public enum JT1078AVType : byte
    {
        保留=0,
        G721 = 1,
        G722 = 2,
        G723 = 3,
        G728 = 4,
        G729 = 5,
        G711A = 6,
        G711U = 7,
        G726 = 8,
        G729A = 9,
        DVI4_3 = 10,
        DVI_4 = 11,
        DVI4_8K = 12,
        DVI4_16K = 13,
        LPC = 14,
        S16BE_STEREO = 15,
        S16E_MONO = 16,
        MPEGAUDIO = 17,
        LPCM = 18,
        AAC = 19,
        WMA9STD = 20,
        HEAAC = 21,
        PCM_VOICE = 22,
        PCM_AUDIO = 23,
        AACLC = 24,
        MP3 = 25,
        ADPCM = 26,
        MP4AUDIO = 27,
        AMR = 28,
        透传=91,
        H264=98,
        H265=99,
        AVS=100,
        SVAC=101
    }
}
