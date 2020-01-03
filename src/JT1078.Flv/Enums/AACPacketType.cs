using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Enums
{
    /// <summary>
    /// Aac tag-body数据包类型
    /// </summary>
    public enum AACPacketType
    {
        /// <summary>
        /// 音频序列配置
        /// </summary>
        AudioSpecificConfig = 0,
        /// <summary>
        /// 音频帧
        /// </summary>
        AudioFrame = 1
    }
}
