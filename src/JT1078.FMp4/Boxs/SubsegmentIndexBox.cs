using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SubsegmentIndexBox : FullBox
    {
        public SubsegmentIndexBox(byte version = 0, uint flags = 0) : base("ssix", version, flags)
        {
        }

        public uint SubSegmentCount { get; set; }
        /// <summary>
        /// length:SubSegmentCount
        /// </summary>
        public List<SubsegmentIndexInfo> SubsegmentIndexInfos { get; set; }

        public class SubsegmentIndexInfo
        {
            public uint RangesCount { get; set; }
            /// <summary>
            /// length:RangesCount
            /// </summary>
            public List<SubsegmentRangeInfo> SubsegmentRangeInfos { get; set; }

            public class SubsegmentRangeInfo
            {
                /// <summary>
                /// 32 - 8
                /// </summary>
                public byte Level { get; set; }
                /// <summary>
                ///  32 - 4
                /// </summary>
                public uint RangeSize { get; set; }
            }
        }
    }
}
