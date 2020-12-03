using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// traf
    /// </summary>
    public class TrackFragmentBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// traf
        /// </summary>
        public TrackFragmentBox() : base("traf")
        {
        }
        /// <summary>
        /// tfhd
        /// </summary>
        public TrackFragmentHeaderBox TrackFragmentHeaderBox { get; set; }
        /// <summary>
        /// sdtp
        /// </summary>
        public SampleDependencyTypeBox SampleDependencyTypeBox { get; set; }
        /// <summary>
        /// trun
        /// </summary>
        public TrackRunBox TrackRunBox { get; set; }
        /// <summary>
        /// tfdt
        /// </summary>
        public TrackFragmentBaseMediaDecodeTimeBox TrackFragmentBaseMediaDecodeTimeBox { get; set; }
        /// <summary>
        /// sbgp
        /// </summary>
        public SampleToGroupBox SampleToGroupBox { get; set; }
        /// <summary>
        /// subs
        /// </summary>
        public SubSampleInformationBox SubSampleInformationBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if (TrackFragmentHeaderBox != null)
            {
                TrackFragmentHeaderBox.ToBuffer(ref writer);
            }
            if (SampleDependencyTypeBox != null)
            {
                SampleDependencyTypeBox.ToBuffer(ref writer);
            }      
            if (TrackFragmentBaseMediaDecodeTimeBox != null)
            {
                TrackFragmentBaseMediaDecodeTimeBox.ToBuffer(ref writer);
            }
            if (TrackRunBox != null)
            {
                TrackRunBox.ToBuffer(ref writer);
            }
            if (SampleToGroupBox != null)
            {
                SampleToGroupBox.ToBuffer(ref writer);
            }        
            if (SubSampleInformationBox != null)
            {
                SubSampleInformationBox.ToBuffer(ref writer);
            }
            End(ref writer);
        }
    }
}
