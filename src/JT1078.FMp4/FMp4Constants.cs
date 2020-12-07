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
        /// 
        /// </summary>
        public const int TFHD_FLAG_BASE_DATA_OFFSET = 0x00000001;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_SAMPLE_DESCRIPTION_INDEX = 0x00000002;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_DURATION = 0x00000008;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_SIZE = 0x00000010;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_FLAGS = 0x00000020;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_DURATION_IS_EMPTY = 0x00010000;
        /// <summary>
        /// 
        /// </summary>
        public const int TFHD_FLAG_DEFAULT_BASE_IS_MOOF = 0x00020000;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_DATA_OFFSET_PRESENT = 0x0001;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT = 0x0004;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_DURATION_PRESENT = 0x0100;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_SIZE_PRESENT = 0x0200;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_FLAGS_PRESENT = 0x0400;
        /// <summary>
        /// 
        /// </summary>
        public const int TRUN_FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT = 0x0800;
    }
}
