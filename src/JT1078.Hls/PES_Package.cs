using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class PES_Package: ITSMessagePackFormatter
    {
        public static byte[] PESSTARCODE = new byte[] { 0x00, 0x00, 0x01 };
        /// <summary>
        /// 开始码，固定为0x000001
        /// </summary>
        public byte[] PESStartCode { get; set; } = PESSTARCODE;
        /// <summary>
        /// 音频取值（0xc0-0xdf），通常为0xc0
        /// 视频取值（0xe0-0xef），通常为0xe0
        /// </summary>
        public byte StreamId { get; set; } = 0xe0;
        /// <summary>
        /// 后面pes数据的长度，0表示长度不限制，只有视频数据长度会超过0xffff
        /// </summary>
        public ushort PESPacketLength { get; set; }
        /// <summary>
        /// 通常取值0x80，表示数据不加密、无优先级、备份的数据
        /// ISOIEC13818-1 120页 Table E-1 -- PES packet header example
        /// </summary>
        internal byte Flag1 { get; set; } = 0x80;
        /// <summary>
        /// 取值0x80表示只含有pts，取值0xc0表示含有pts和dts
        /// ISOIEC13818-1 120页 Table E-1 -- PES packet header example
        /// </summary>
        public PTS_DTS_Flags PTS_DTS_Flag { get; set; }
        /// <summary>
        /// 根据PTS_DTS_Flag来判断后续长度
        /// 后面数据的长度，取值5或10
        /// </summary>
        internal byte PESDataLength { get; set; }
        /// <summary>
        /// 5B 
        /// 33bit值
        /// </summary>
        public long PTS { get; set; }
        /// <summary>
        /// 5B 
        /// 33bit值
        /// </summary>
        public long DTS { get; set; }
        /// <summary>
        /// 音视频数据
        /// </summary>
        public ES_Package Payload { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteArray(PESStartCode);
            writer.WriteByte(StreamId);
            writer.WriteUInt16(PESPacketLength);
            writer.WriteByte(Flag1);
            writer.WriteByte((byte)PTS_DTS_Flag);
            if (PTS_DTS_Flag== PTS_DTS_Flags.all)
            {
                writer.WriteByte(10);
                writer.WriteInt5(ToPTS());
                writer.WriteInt5(ToDTS());
            }
            else if(PTS_DTS_Flag == PTS_DTS_Flags.pts)
            {
                writer.WriteByte(5);
                writer.WriteInt5(ToPTS());
            }
            else if (PTS_DTS_Flag == PTS_DTS_Flags.dts)
            {
                writer.WriteByte(5);
                writer.WriteInt5(ToDTS());
            }
            Payload.ToBuffer(ref writer);
        }

        public long ToPTS()
        {
            var str = Convert.ToString(PTS, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0011");
            str = str.Substring(str.Length - 40, 40);
            return Convert.ToInt64(str, 2);
        }   
        
        public long ToDTS()
        {
            var str = Convert.ToString(DTS, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0001");
            str = str.Substring(str.Length - 40, 40);
            return Convert.ToInt64(str, 2);
        }
    }
}
