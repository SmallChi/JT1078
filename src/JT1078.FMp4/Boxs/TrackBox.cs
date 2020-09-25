using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// trak
    /// </summary>
    public class TrackBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// trak
        /// </summary>
        public TrackBox() : base("trak")
        {
        }
        /// <summary>
        /// tkhd
        /// </summary>
        public TrackHeaderBox TrackHeaderBox { get; set; }
        //不是必须的    public TrackReferenceBox TrackReferenceBox { get; set; } 
        //不是必须的    public EditBox EditBox { get; set; }
        /// <summary>
        /// mdia
        /// </summary>
        public MediaBox MediaBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            TrackHeaderBox.ToBuffer(ref writer);
            MediaBox.ToBuffer(ref writer);
            End(ref writer);
        }
    }
}
