using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Enums
{
    public enum FastTime : byte
    {
        无效 = 0x00,
        _1倍 = 0x01,
        _2倍 = 0x02,
        _4倍 = 0x03,
        _8倍 = 0x04,
        _16倍 = 0x05,
    }
}
