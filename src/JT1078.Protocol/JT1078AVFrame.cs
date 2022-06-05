using JT1078.Protocol.H264;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JT1078.Protocol
{
    public class JT1078AVFrame : IJT1078AVKey
    {
        /// <summary>
        /// 终端设备SIM卡号
        /// BCD[6]
        /// </summary>
        public string SIM { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNumber { get; set; }
        public List<H264NALU> Nalus { get; set; }
        public H264NALU SPS { get; set; }
        public H264NALU PPS { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        string VideoType { get; set; } = "avc1";
        public byte ProfileIdc { get; set; }
        public byte ProfileCompat { get; set; }
        public byte LevelIdc { get; set; }
        public string ToCodecs()
        {
            return $"{VideoType}.{ProfileIdc:x2}{ProfileCompat:x2}{LevelIdc:x2}";
        }
        public string GetAVKey()
        {
            return $"{SIM}_{LogicChannelNumber.ToString()}";
        }
    }
}
