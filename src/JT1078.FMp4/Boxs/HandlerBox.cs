using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class HandlerBox : FullBox
    {
        public HandlerBox(byte version=0, uint flags=0) : base("hdlr", version, flags)
        {
        }

        public uint PreDefined { get; set; }
        public string HandlerType { get; set; }
        public uint[] Reserved { get; set; } = new uint[3];
        public string Name { get; set; }
    }
}
