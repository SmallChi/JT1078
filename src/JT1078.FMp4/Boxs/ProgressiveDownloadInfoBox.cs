using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ProgressiveDownloadInfoBox : FullBox
    {
        public ProgressiveDownloadInfoBox(byte version=0, uint flags=0) : base("pdin", version, flags)
        {
        }

        public List<ProgressiveDownloadInfo> ProgressiveDownloadInfos { get; set; }

        public class ProgressiveDownloadInfo
        {
            public ushort Rate { get; set; }
            public ushort InitialDelay { get; set; }
        }
    }
}
