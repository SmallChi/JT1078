using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FDItemInformationBox : FullBox
    {
        public FDItemInformationBox(byte version, uint flags) : base("fiin", version, flags)
        {
        }

        public ushort EntryCount { get; set; }
        /// <summary>
        /// length:EntryCount
        /// </summary>
        public List<PartitionEntryBox> PartitionEntries { get; set; }
        public FDSessionGroupBox SessionInfo { get; set; }
        public GroupIdToNameBox GroupIdToName { get; set; }
    }
}
