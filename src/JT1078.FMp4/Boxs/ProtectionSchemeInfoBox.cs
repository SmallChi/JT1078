using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ProtectionSchemeInfoBox : Mp4Box
    {
        public ProtectionSchemeInfoBox(string fmt) : base("sinf")
        {
            OriginalFormatBox = new OriginalFormatBox(fmt);
        }

        public OriginalFormatBox OriginalFormatBox { get; set; }

        public SchemeTypeBox SchemeTypeBox { get; set; }

        public SchemeInformationBox SchemeInformationBox { get; set; }
    }
}
