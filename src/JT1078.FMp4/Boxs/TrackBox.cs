using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackBox : Mp4Box
    {
        public TrackBox() : base("trak")
        {
        }
         public TrackHeaderBox TrackHeaderBox { get; set; }
        //不是必须的    public TrackReferenceBox TrackReferenceBox { get; set; } 
        //不是必须的    public EditBox EditBox { get; set; }
        public MediaBox MediaBox { get; set; }
    }
}
