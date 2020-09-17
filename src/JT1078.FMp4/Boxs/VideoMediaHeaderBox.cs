using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class VideoMediaHeaderBox : FullBox
    {
        public VideoMediaHeaderBox(byte version = 0, uint flags = 1) : base("vmhd", version, flags)
        {
        }

        public ushort GraphicsMode { get; set; }
        public ushort Red { get; set; }
        public ushort Green { get; set; }
        public ushort Blue { get; set; }
        public ushort[] OpColor
        {
            get
            {
                return new ushort[] {
                    Red,Green,Blue
                };
            }
        }
    }
}
