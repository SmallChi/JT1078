using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// 
    /// </summary>
    public class FragmentBox:IFMp4MessagePackFormatter
    {
        /// <summary>
        /// moof
        /// </summary>
        public MovieFragmentBox MovieFragmentBox { get; set; }
        /// <summary>
        /// mdat
        /// </summary>
        public MediaDataBox MediaDataBox { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            if (MovieFragmentBox != null)
            {
                MovieFragmentBox.ToBuffer(ref writer);
            }
            if (MediaDataBox != null)
            {
                MediaDataBox.ToBuffer(ref writer);
            }
        }
    }
}
