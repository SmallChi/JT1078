using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// moov
    /// </summary>
    public class MovieBox : Mp4Box,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// moov
        /// </summary>
        public MovieBox() : base("moov")
        {
        }
        /// <summary>
        /// mvhd
        /// </summary>
        public MovieHeaderBox MovieHeaderBox { get; set; }
        /// <summary>
        /// trak
        /// </summary>
        public TrackBox TrackBox { get; set; }
        /// <summary>
        /// mvex
        /// </summary>
        public MovieExtendsBox MovieExtendsBox { get; set; }
        /// <summary>
        /// udta
        /// </summary>
        public UserDataBox UserDataBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            MovieHeaderBox.ToBuffer(ref writer);
            TrackBox.ToBuffer(ref writer);
            if (MovieExtendsBox != null)
            {
                MovieExtendsBox.ToBuffer(ref writer);
            }
            if (UserDataBox != null)
            {
                UserDataBox.ToBuffer(ref writer);
            }
            End(ref writer);
        }
    }
}
