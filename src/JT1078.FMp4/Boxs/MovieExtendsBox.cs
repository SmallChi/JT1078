using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mvex
    /// </summary>
    public class MovieExtendsBox : Mp4Box
    {
        /// <summary>
        /// mvex
        /// </summary>
        public MovieExtendsBox() : base("mvex")
        {
        }
        public MovieExtendsHeaderBox MovieExtendsHeaderBox { get; set; }
        public List<TrackExtendsBox> TrackExtendsBoxs { get; set; }
    }
}
