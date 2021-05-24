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
        /// ts路径sim参数名称
        /// </summary>
        public string TsPathSimParamName { get; set; } = "sim";
        /// <summary>
        /// ts路径通道参数名称
        /// </summary>
        public string TsPathChannelParamName { get; set; } = "channel";
        /// <summary>
        /// m3u8文件
        /// </summary>
        public string M3U8FileName { get; set; } = "live.m3u8";
        /// <summary>
        /// hls文件路径（包括m3u8路径，ts路径）
        /// </summary>
        public string HlsFileDirectory { get; set; } = "wwwroot";
    }
}
