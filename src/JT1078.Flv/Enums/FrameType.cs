using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Enums
{
    public enum FrameType:byte
    {
        /// <summary>
        /// ‭00010000‬
        /// </summary>
        KeyFrame = 16,
        /// <summary>
        /// ‭00100000‬
        /// </summary>
        InterFrame = 32,
        /// <summary>
        /// ‭00110000‬
        /// </summary>
        DisposableInterFrame = 48,
        /// <summary>
        /// 01000000
        /// </summary>
        GeneratedKeyFrame = 64,
        /// <summary>
        /// 01010000
        /// </summary>
        VideoInfoOrCommandFrame=80
    }
}
