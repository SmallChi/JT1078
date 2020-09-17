using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class URIBox : FullBox
    {
        public URIBox(byte version=0, uint flags=0) : base("uri ", version, flags)
        {
        }

        public string TheURI { get; set; }
    }
}
