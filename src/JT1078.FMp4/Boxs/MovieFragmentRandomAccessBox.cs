using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieFragmentRandomAccessBox : Mp4Box
    {
        public MovieFragmentRandomAccessBox() : base("mfra")
        {
        }

        public TrackFragmentRandomAccessBox TrackFragmentRandomAccessBox { get; set; }

        public MovieFragmentRandomAccessOffsetBox MovieFragmentRandomAccessOffsetBox { get; set; }
    }
}
