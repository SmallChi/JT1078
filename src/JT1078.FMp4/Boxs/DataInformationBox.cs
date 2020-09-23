using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// dinf
    /// </summary>
    public class DataInformationBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// dinf
        /// </summary>
        public DataInformationBox() : base("dinf")
        {
        }
        /// <summary>
        /// dref
        /// </summary>
        public DataReferenceBox DataReferenceBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            DataReferenceBox.ToBuffer(ref writer);
            End(ref writer);
        }
    }
}
