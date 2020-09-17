using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class GroupIdToNameBox : FullBox
    {
        public GroupIdToNameBox(byte version=0, uint flags=0) : base("gitn", version, flags)
        {
        }

        public ushort EntryCount { get; set; }

        public List<GroupIdToNameInfo> GroupIdToNameInfos { get; set; }

        public class GroupIdToNameInfo
        {
            public uint GroupID { get; set; }

            public string GroupName { get; set; }
        }
    }
}
