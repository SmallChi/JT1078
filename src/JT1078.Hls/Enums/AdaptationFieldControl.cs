using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    public enum AdaptationFieldControl:byte
    {
        保留= 0b_0000_0000,
        无自适应域_仅含有效负载 = 0b_0001_0000,
        仅含自适应域_无有效负载 = 0b_0010_0000,
        同时带有自适应域和有效负载 = 0b_0011_0000,
    }
}
