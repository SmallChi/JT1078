using JT1078.Hls.MessagePack;
using System;

namespace JT1078.Hls.Interfaces
{
    public interface ITSMessagePackFormatter
    {
        void ToBuffer(ref TSMessagePackWriter writer);
    }
}
