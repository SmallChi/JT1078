using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    /// <summary>
    /// 取0x50表示包含PCR或0x40表示不包含PCR
    /// 注意:关键帧需要加pcr
    /// </summary>
    public enum PCRInclude:byte
    {
        包含= 0x50,
        不包含= 0x40
    }
}
