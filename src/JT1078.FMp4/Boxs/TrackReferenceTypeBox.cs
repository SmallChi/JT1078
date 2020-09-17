using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackReferenceTypeBox : Mp4Box
    {
        public TrackReferenceTypeBox(string referenceType) : base(referenceType)
        {
        }

        public List<uint> TrackIDs { get; set; }
    }
}
