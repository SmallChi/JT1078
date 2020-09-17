using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FDSessionGroupBox : Mp4Box
    {
        public FDSessionGroupBox() : base("segr")
        {
        }

        public ushort NumSessionGroups { get; set; }

        public class FDSessionGroupInfo
        {
            public byte EntryCount { get; set; }
            /// <summary>
            /// length:EntryCount
            /// </summary>
            public List<uint> GroupIDs { get; set; }
            public ushort NumChannelsInSessionGroup { get; set; }
            /// <summary>
            /// length:NumChannelsInSessionGroup
            /// </summary>
            public List<uint> HintTrackId{ get; set; }
    }
    }
}
