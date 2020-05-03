using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    public enum AdaptationFieldControl
    {
        保留= 0000_0000,
        无自适应域_仅含有效负载 = 0001_0000,
        仅含自适应域_无有效负载 = 0010_0000,
        同时带有自适应域和有效负载 = 0011_0000,
    }
}
