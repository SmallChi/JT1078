using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Enums
{
    public enum JT809_JT1078_0x1A00_0x1A02_Result:byte
    {
        成功 = 0x00,
        失败 = 0x01,
        不支持 = 0x02,
        会话结束 = 0x03,
    }
}
