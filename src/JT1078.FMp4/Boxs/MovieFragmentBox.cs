using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieFragmentBox : Mp4Box
    {
        public MovieFragmentBox() : base("moof")
        {
        }

        public MovieFragmentHeaderBox MovieFragmentHeaderBox { get; set; }
        public TrackFragmentBox TrackFragmentBox { get; set; }
    }
}
