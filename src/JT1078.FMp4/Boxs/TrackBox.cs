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
         public TrackReferenceBox TrackReferenceBox { get; set; }
         public EditBox EditBox { get; set; }
         public MediaBox MediaBox { get; set; }
    }
}
