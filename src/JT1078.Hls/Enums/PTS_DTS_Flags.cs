using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum PTS_DTS_Flags:byte
    {
        all = 0xc0,
        pts = 0x80,
        dts = 0x40
    }
}
