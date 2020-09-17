using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieExtendsHeaderBox : FullBox
    {
        public MovieExtendsHeaderBox( byte version, uint flags=0) : base("mehd", version, flags)
        {
        }
        public uint FragmentDuration { get; set; }
    }
}
