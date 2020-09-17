using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieExtendsBox : Mp4Box
    {
        public MovieExtendsBox() : base("mvex")
        {
        }
        public MovieExtendsHeaderBox MovieExtendsHeaderBox { get; set; }
        public List<TrackExtendsBox> TrackExtendsBoxs { get; set; }
    }
}
