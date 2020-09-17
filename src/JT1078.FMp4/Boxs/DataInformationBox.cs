using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class DataInformationBox : Mp4Box
    {
        public DataInformationBox() : base("dinf")
        {
        }
        public DataReferenceBox DataReferenceBox { get; set; }
    }
}
