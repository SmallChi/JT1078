using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mvex
    /// </summary>
    public class MovieExtendsBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mvex
        /// </summary>
        public MovieExtendsBox() : base("mvex")
        {
        }
        /// <summary>
        /// mehd
        /// </summary>
        public MovieExtendsHeaderBox MovieExtendsHeaderBox { get; set; }
        /// <summary>
        /// trex
        /// </summary>
        public List<TrackExtendsBox> TrackExtendsBoxs { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if (MovieExtendsHeaderBox != null)
            {
                MovieExtendsHeaderBox.ToBuffer(ref writer);
            }
            if (TrackExtendsBoxs != null)
            {
                foreach(var item in TrackExtendsBoxs)
                {
                    item.ToBuffer(ref writer);
                }
            }
            End(ref writer);
        }
    }
}
