using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public enum FrameType
    {
        KeyFrame = 1,
        InterFrame,
        DisposableInterFrame,
        GeneratedKeyFrame,
        VideoInfoOrCommandFrame
    }
}
