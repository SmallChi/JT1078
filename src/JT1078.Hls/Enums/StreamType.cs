using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    public enum StreamType:byte
    {
        h264 = 0x1B,
        aac = 0x0f,
        mp3 = 0x03
    }
}
