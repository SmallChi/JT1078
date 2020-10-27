using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// minf
    /// </summary>
    public class MediaInformationBox : Mp4Box,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// minf
        /// </summary>
        public MediaInformationBox() : base("minf")
        {
        }
        /// <summary>
        /// vmhd
        /// </summary>
        public VideoMediaHeaderBox VideoMediaHeaderBox { get; set; }
        /// <summary>
        /// dinf
        /// </summary>
        public DataInformationBox DataInformationBox { get; set; }
        /// <summary>
        /// stbl
        /// </summary>
        public SampleTableBox SampleTableBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if (VideoMediaHeaderBox != null)
            {
                VideoMediaHeaderBox.ToBuffer(ref writer);
            }
            DataInformationBox.ToBuffer(ref writer);
            SampleTableBox.ToBuffer(ref writer);
            End(ref writer);
        }
    }
}
