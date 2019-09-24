using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Enums
{
    public enum AvcPacketType:byte
    {
        SequenceHeader = 0,
        Raw,
        AVCEndSequence
    }
}
