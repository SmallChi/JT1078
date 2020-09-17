using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class LevelAssignmentBox : FullBox
    {
        public LevelAssignmentBox(byte version=0, uint flags=0) : base("leva", version, flags)
        {
        }

        public byte LevelCount { get; set; }

        public class LevelAssignmentInfo
        {
            public uint TrackId { get; set; }
            /// <summary>
            /// 1byte 8-1
            /// </summary>
            public bool PaddingFlag { get; set; }
            /// <summary>
            /// 1byte 8-7
            /// </summary>
            public byte AssignmentType { get; set; }
            /// <summary>
            /// AssignmentType == 0 || assignment_type == 1
            /// </summary>
            public uint GroupingType { get; set; }
            /// <summary>
            /// assignment_type == 1
            /// </summary>
            public uint GroupingTypeParameter { get; set; }
            /// <summary>
            /// assignment_type == 4
            /// </summary>
            public uint SubTrackId { get; set; }
        }
    }
}
