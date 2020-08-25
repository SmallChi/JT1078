using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Audio
{
    public interface IAudioCodec
    {
        byte[] ToPcm(byte[] audio, IAudioAttachData audioAttachData);
    }
}
