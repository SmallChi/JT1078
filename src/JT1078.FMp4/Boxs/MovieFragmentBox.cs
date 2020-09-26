using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// moof
    /// </summary>
    public class MovieFragmentBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// moof
        /// </summary>
        public MovieFragmentBox() : base("moof")
        {
        }
        /// <summary>
        /// mfhd
        /// </summary>
        public MovieFragmentHeaderBox MovieFragmentHeaderBox { get; set; }
        /// <summary>
        /// traf
        /// </summary>
        public TrackFragmentBox TrackFragmentBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if (MovieFragmentHeaderBox != null)
            {
                MovieFragmentHeaderBox.ToBuffer(ref writer);
            }        
            if (TrackFragmentBox != null)
            {
                TrackFragmentBox.ToBuffer(ref writer);
            }
            End(ref writer);
        }
    }
}
