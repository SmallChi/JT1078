using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT1078.FMp4.Test")]

namespace JT1078.FMp4
{
    public static class FMp4Constants
    {
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        public const int DateLimitYear = 2000;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DateTime UTCBaseTime = new DateTime(1904, 1, 1);
        /// <summary>
        /// fmp4 FLAG_SEGMENT
        /// </summary>
        public const int FLAG_SEGMENT = 0x00000002;
        /// <summary>
        /// key frame
        /// </summary>

        public const int AV_FLAG_KEYFREAME = 0x0001;
        /// <summary>
        /// I frame
        /// </summary>

        public const uint TREX_FLAG_SAMPLE_DEPENDS_ON_I_PICTURE = 0x02000000;
        /// <summary>
        /// p b frame
        /// </summary>

        public const uint TREX_FLAG_SAMPLE_DEPENDS_ON_NOT_I_PICTURE = 0x01000000;
        /// <summary>
        /// TKHD_FLAG_ENABLED
        /// </summary>
        public const int TKHD_FLAG_ENABLED = 0x000001;
        /// <summary>
        /// TKHD_FLAG_IN_MOVIE
        /// </summary>
        public const int TKHD_FLAG_IN_MOVIE = 0x000002;
        /// <summary>                                                                      
        /// TKHD_FLAG_IN_PREVIEW                                                    
        /// </summary>
        public const int TKHD_FLAG_IN_PREVIEW = 0x000004;
        /// <summary>
        /// TFHD_FLAG_BASE_DATA_OFFSET
        /// </summary>
        public const int TFHD_FLAG_BASE_DATA_OFFSET = 0x00000001;
        /// <summary>
        /// TFHD_FLAG_SAMPLE_DESC
        /// </summary>
        public const int TFHD_FLAG_SAMPLE_DESCRIPTION_INDEX = 0x00000002;
        /// <summary>
        /// TFHD_FLAG_AUDIO_TPYE
        /// </summary>
        public const int TFHD_FLAG_AUDIO_TPYE = 0x02000000;
        /// <summary>
        /// TFHD_FLAG_VIDEO_TPYE
        /// </summary>
        public const int TFHD_FLAG_VIDEO_TPYE = (0x00010000| 0x01000000);
        /// <summary>
        /// TFHD_FLAG_SAMPLE_DUR
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_DURATION = 0x00000008;
        /// <summary>
        /// TFHD_FLAG_SAMPLE_SIZE
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_SIZE = 0x00000010;
        /// <summary>
        /// TFHD_FLAG_SAMPLE_FLAGS
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_FLAGS = 0x00000020;
        /// <summary>
        /// TFHD_FLAG_DUR_EMPTY
        /// </summary>
        public const int TFHD_FLAG_DURATION_IS_EMPTY = 0x00010000;
        /// <summary>
        /// TFHD_FLAG_DEFAULT_BASE_IS_MOOF
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_BASE_IS_MOOF = 0x00020000;
        /// <summary>
        /// TRUN_FLAGS_DATA_OFFSET
        /// </summary>
        public const int TRUN_FLAG_DATA_OFFSET_PRESENT = 0x0001;
        /// <summary>
        /// TRUN_FLAGS_FIRST_FLAG
        /// </summary>
        public const int TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT = 0x0004;
        /// <summary>
        /// TRUN_FLAGS_DURATION
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_DURATION_PRESENT = 0x0100;
        /// <summary>
        /// TRUN_FLAGS_SIZE
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_SIZE_PRESENT = 0x0200;
        /// <summary>
        /// TRUN_FLAGS_FLAGS
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_FLAGS_PRESENT = 0x0400;
        /// <summary>
        /// TRUN_FLAGS_CTS_OFFSET
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT = 0x0800;
    }
}
