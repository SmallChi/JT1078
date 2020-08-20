using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Options
{
    /// <summary>
    /// m3u8配置文件
    /// </summary>
    public class M3U8Option
    {
        /// <summary>
        /// m3u8文件中默认包含的ts文件数
        /// </summary>
        public int TsFileCapacity { get; set; } = 10;
        /// <summary>
        /// 每个ts文件的最大时长
        /// </summary>
        public int TsFileMaxSecond { get; set; } = 10;
        /// <summary>
        /// 当前ts文件序号
        /// </summary>
        public int TsFileSerialNo { get; set; } = 0;
        /// <summary>
        /// m3u8文件
        /// </summary>
        public string M3U8FileName { get; set; }
        /// <summary>
        /// hls文件路径
        /// </summary>
        public string HlsFileDirectory { get; set; }
    }
}
