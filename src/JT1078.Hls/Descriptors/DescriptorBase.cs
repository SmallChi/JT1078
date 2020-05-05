using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Descriptors
{
    public abstract class DescriptorBase : ITSMessagePackFormatter
    {
        public abstract byte Tag { get; set; }
        public abstract byte Length { get; set; }
        public abstract void ToBuffer(ref TSMessagePackWriter writer);
    }
}
