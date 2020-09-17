using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class OriginalFormatBox : Mp4Box
    {
        public OriginalFormatBox(string codingname) : base("frma")
        {
            DataFormat = codingname;
        }

        public string DataFormat { get; set; }

    }
}
