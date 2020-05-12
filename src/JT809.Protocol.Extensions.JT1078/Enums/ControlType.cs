using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Enums
{
    /// <summary>
    /// 控制类型
    /// </summary>
    public enum ControlType:byte
    {
        正常回放=0x00,
        暂停回放 = 0x01,
        结束回放 = 0x02,
        快进回放 = 0x03,
        关键帧快退回放 = 0x04,
        拖动回放 = 0x05,
        关键帧播放 = 0x06,
    }
}
