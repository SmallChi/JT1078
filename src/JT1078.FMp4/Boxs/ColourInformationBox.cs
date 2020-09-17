using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ColourInformationBox : Mp4Box
    {
        public ColourInformationBox() : base("colr")
        {
        }
        public string ColourType { get; set; }
        public ushort ColourPrimaries { get; set; }
        public ushort TransferCharacteristics { get; set; }
        public ushort MatrixCoefficients { get; set; }
        public bool FullRangeFlag { get; set; }
        public byte Reserved { get; set; }
        #warning ICC_profile?????
        public byte [] ICCProfile { get; set; }
    }
}
