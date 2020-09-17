using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class URIInitBox : FullBox
    {
        public URIInitBox(byte version=0, uint flags=0) : base("uriI", version, flags)
        {
        }

        public byte[] UriInitializationData { get; set; }
    }
}
