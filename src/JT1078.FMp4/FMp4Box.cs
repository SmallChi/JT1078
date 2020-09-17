using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FMp4Box
    {
        public FileTypeBox FileTypeBox { get; set; }
        public MovieBox MovieBox { get; set; }
        public List<FragmentBox> FragmentBoxs { get; set; }
        public MovieFragmentRandomAccessBox MovieFragmentRandomAccessBox { get; set; }
    }
}
