using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class EditBox : Mp4Box
    {
        public EditBox() : base("edts")
        {
        }

        public List<EditListBox> EditListBox { get; set; }
    }
}
