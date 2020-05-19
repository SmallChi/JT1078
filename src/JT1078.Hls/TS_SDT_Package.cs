using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace JT1078.Hls
{
    /// <summary>
    /// 业务描述表
    /// </summary>
    public class TS_SDT_Package : ITSMessagePackFormatter
    {
        public TS_Header Header { get; set; }
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        public byte TableId { get; set; } = 0x42;
        /// <summary>
        /// 
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
        internal ushort TransportStreamId { get; set; } 
        /// <summary>
        /// 
        /// 2bit
        /// </summary>
        internal byte Reserved2 { get; set; }
        /// <summary>
        /// 
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
        public List<TS_SDT_Service> Services { get; set; }

        /// <summary>
        /// 前面数据的CRC32校验码
        /// </summary>
        public uint CRC32 { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte(TableId);
            writer.Skip(2, out int SectionLengthPosition);
            writer.WriteUInt16(TransportStreamId);
            writer.WriteByte((byte)(Reserved2 << 6 | VersionNumber << 1 | CurrentNextIndicator));
            writer.WriteByte(SectionNumber);
            writer.WriteByte(LastSectionNumber);
            writer.WriteUInt16(OriginalNetworkId);
            writer.WriteByte(ReservedFutureUse2);
            foreach (var service in Services)
            {
                service.ToBuffer(ref writer);
            }
            ushort servicesLength =(ushort)( writer.GetCurrentPosition() - SectionLengthPosition);
            const int crcLength = 4;
            writer.WriteUInt16Return((ushort)(SectionSyntaxIndicator<<15 | ReservedFutureUse1<<14 | servicesLength+ crcLength), SectionLengthPosition);
            writer.WriteCRC32(5);
            var size = TSConstants.FiexdPackageLength - writer.GetCurrentPosition();
            writer.WriteArray(Enumerable.Range(0, size).Select(s => (byte)0xFF).ToArray());
        }
    }
}
