using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// m3u8配置文件
    /// </summary>
    public class M3U8Config
    {
        /// <summary>
        /// m3u8文件中包含的ts文件数
        /// </summary>
        public int TsFileCount { get; set; } = 10;
        /// <summary>
        /// 每个ts文件的最大时长
        /// </summary>
        public int TsFileMaxSecond { get; set; } = 10;
        /// <summary>
        ///  m3u8文件中第一个ts文件序号
        /// </summary>
        public int FirstTsSerialNo { get; set; } = 0;
    }
}
