using JT1078.FMp4.MessagePack;
using System;

namespace JT1078.FMp4.Interfaces
{
    public interface IFMp4MessagePackFormatter
    {
        void ToBuffer(ref FMp4MessagePackWriter writer);
    }
}
