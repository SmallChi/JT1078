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
        ///  生成的ts的文件数
        /// </summary>
        public int TsFileCount { get; set; } = 0;
        /// <summary>
        /// 1078包的时间戳 毫秒
        /// </summary>
        public ulong TimestampMilliSecond { get; set; } = 0;
        /// <summary>
        /// 累计时长  如果大于文件时长就存储一个ts文件
        /// </summary>
        public double AccumulateSeconds { get; set; } = 0;
        /// <summary>
        /// 是否需要头部，每个ts文件都需要头部，重置为true
        /// </summary>
        public bool IsNeedFirstHeadler { get; set; } = true;
        /// <summary>
        /// m3u8文件
        /// </summary>
        public string M3U8Filepath { get; set; }
        /// <summary>
        /// hls文件路径
        /// </summary>
        public string HlsFileDirectory { get; set; }
    }
}
