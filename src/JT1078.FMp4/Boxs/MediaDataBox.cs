using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mdat
    /// </summary>
    public class MediaDataBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mdat
        /// </summary>
        public MediaDataBox() : base("mdat")
        {
        }
        /// <summary>
        /// 过滤掉AUD/SPS/PPS NAL <br/>
        /// 然后将其他NAL写入Data中 <br/>
        /// Filter out AUD/SPS/PPS NAL units from your stream<br/>
        /// Write you converted NAL units into the MDAT box<br/>
        /// </summary>
        public List<byte[]> Data { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            
            if (Data != null && Data.Count > 0)
            {
                foreach(var nalu in Data)
                {
                    if(nalu!=null && nalu.Length > 0)
                    {
                        writer.WriteInt32(nalu.Length);
                        writer.WriteArray(nalu);
                    }
                }
            }
            End(ref writer);
        }
    }
}
