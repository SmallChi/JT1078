using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// fmp4
    /// </summary>
    public class FMp4Box:IFMp4MessagePackFormatter
    {
        /// <summary>
        /// ftyp
        /// </summary>
        public FileTypeBox FileTypeBox { get; set; }
        /// <summary>
        /// moov
        /// </summary>
        public MovieBox MovieBox { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FragmentBox> FragmentBoxs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MovieFragmentRandomAccessBox MovieFragmentRandomAccessBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            FileTypeBox.ToBuffer(ref writer);
        }
    }
}
