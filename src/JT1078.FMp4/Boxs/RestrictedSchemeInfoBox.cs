using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class RestrictedSchemeInfoBox : Mp4Box
    {
        public RestrictedSchemeInfoBox(string fmt) : base("rinf")
        {
            OriginalFormatBox = new OriginalFormatBox(fmt);
        }

        public OriginalFormatBox OriginalFormatBox { get; set; }

        public SchemeTypeBox SchemeTypeBox { get; set; }

        public SchemeInformationBox SchemeInformationBox { get; set; }
    }
}
