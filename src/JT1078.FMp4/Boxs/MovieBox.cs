using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JT1078.FMp4
{
    public class MovieBox : Mp4Box
    {
        public MovieBox() : base("moov")
        {
        }
        public MovieHeaderBox MovieHeaderBox { get; set; }
        public TrackBox TrackBox { get; set; }
        public MovieExtendsBox MovieExtendsBox { get; set; }   
    }
}
