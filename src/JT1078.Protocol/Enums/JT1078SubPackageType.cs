using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Enums
{
    /// <summary>
    /// 分包处理标记
    /// </summary>
    public enum JT1078SubPackageType:byte
    {
        原子包_不可被拆分=0,
        分包处理时的第一个包=1,
        分包处理时的最后一个包=2,
        分包处理时的中间包=3
    }
}
