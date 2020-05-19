using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Hls
{
    /// <summary>
    /// 业务描述服务描述
    /// </summary>
    public class TS_SDT_Service_Descriptor : ITSMessagePackFormatter
    {
        public TS_Header Header { get; set; }
        /// <summary>
        /// PAT表固定为0x00
        /// 8bit
        /// </summary>
        public byte TableId { get; set; } = 0x42;
        /// <summary>
        /// 固定为二进制1
        /// 1bit
        /// </summary>
        internal byte SectionSyntaxIndicator { get; set; }
        /// <summary>
        /// 
        /// 1bit
        /// </summary>
        internal byte ReservedFutureUse1 { get; set; }
        /// <summary>
        /// 
        /// 2bit
        /// </summary>
        internal byte Reserved1 { get; set; }
        /// <summary>
        /// 后面数据的长度
        /// 12bit
        /// </summary>
        public ushort SectionLength { get; set; }
        /// <summary>
        /// 传输流ID
        /// 16bit
        /// </summary>
        internal ushort TransportStreamId { get; set; } = 0x0001;
        /// <summary>
        /// 
        /// 2bit
        /// </summary>
        internal byte Reserved2 { get; set; }
        /// <summary>
        /// 版本号，
        /// 5bit
        /// </summary>
        public byte VersionNumber { get; set; }
        /// <summary>
        /// 
        /// 1bit
        /// </summary>
        public byte CurrentNextIndicator { get; set; } 
        /// <summary>
        /// 
        /// bit8
        /// </summary>
        internal byte SectionNumber { get; set; } 
        /// <summary>
        /// 
        /// bit8
        /// </summary>
        internal byte LastSectionNumber { get; set; }
        /// <summary>
        /// 
        /// bit8
        /// </summary>
        internal ushort OriginalNetworkId { get; set; }
        /// <summary>
        /// 
        /// 1Byte
        /// </summary>
        internal byte ReservedFutureUse2 { get; set; }
        public List<TS_PAT_Program> Services { get; set; }

        /// <summary>
        /// 前面数据的CRC32校验码
        /// </summary>
        public uint CRC32 { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            Header.PackageType = PackageType.PAT;
            Header.ToBuffer(ref writer);
            writer.WriteByte(TableId);
            //SectionSyntaxIndicator   Zero  Reserved1   SectionLength
            //1 0 11 0000 0000 0000
            //(ushort)(0b_1011_0000_0000_0000 | SectionLength)
            writer.Skip(2, out int SectionLengthPosition);
            writer.WriteUInt16(TransportStreamId);
            //Reserved2 VersionNumber CurrentNextIndicator
            //11 00000 1
            var a = 0xC0 & (Reserved2 << 6);
            var b = 0x3E & (VersionNumber << 3);
            var c = (byte)(a | b | CurrentNextIndicator);
            writer.WriteByte(c);
            writer.WriteByte(SectionNumber);
            writer.WriteByte(LastSectionNumber);
            //if (Programs != null)
            //{
            //    foreach (var program in Programs)
            //    {
            //        program.ToBuffer(ref writer);
            //    }
            //}
            const int crcLength= 4;
            writer.WriteUInt16Return((ushort)(0b_1011_0000_0000_0000 | (ushort)(writer.GetCurrentPosition() - SectionLengthPosition - 2)+ crcLength), SectionLengthPosition);
            //打包ts流时PAT和PMT表是没有adaptation field的，不够的长度直接补0xff即可。
            //ts header(4B) + adaptation field length(1)
            writer.WriteCRC32(5);
            var size = TSConstants.FiexdPackageLength - writer.GetCurrentPosition();
            writer.WriteArray(Enumerable.Range(0, size).Select(s => (byte)0xFF).ToArray());
        }
    }
}
