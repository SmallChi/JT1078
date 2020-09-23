using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// trak
    /// </summary>
    public class TrackBox : Mp4Box
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
    }
}
