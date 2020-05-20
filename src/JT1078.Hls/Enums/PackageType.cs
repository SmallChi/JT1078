using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    public enum PackageType
    {
        PAT=1,
        PMT=2,
        Data_Start=3,
        Data_Segment = 4,
        Data_End = 5,
        SDT=6
    }
}
