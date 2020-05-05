using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class ES_Package: ITSMessagePackFormatter
    {
        public static byte[] NALU0X09 = new byte[] { 0x00, 0x00, 0x00, 0x01, 0x09, 0xFF };
        public byte[] NALU0x09 { get; set; } = NALU0X09;
        public List<byte[]> NALUs { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteArray(NALU0x09);
            if(NALUs!=null)
            {
                foreach(var nalu in NALUs)
                {
                    writer.WriteArray(nalu);
                }
            }
        }
    }
}
