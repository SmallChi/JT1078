using JT1078.FMp4.Samples;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleDescriptionBox : FullBox
    {
        public string HandlerType { get; set; }
        public SampleDescriptionBox(string handlerType,byte version=0, uint flags=0) : base("stsd", version, flags)
        {
            HandlerType = handlerType;
        }

        public uint EntryCount { get; set; }
        
        public List<SampleEntry> SampleEntries { get; set; }
    }
}
