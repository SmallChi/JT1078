using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public class FlvBody
    {
        /// <summary>
        /// 前一个tag的长度
        /// </summary>
        public uint PreviousTagSize { get; set; }
        public FlvTags Tag { get; set; }
    }
}
