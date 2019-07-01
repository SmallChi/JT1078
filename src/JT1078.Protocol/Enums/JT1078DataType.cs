using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Enums
{
    public enum JT1078DataType:byte
    {
        视频I帧=0,
        视频P帧=1,
        视频B帧=2,
        音频帧=3,
        透传数据=4,
    }
}
