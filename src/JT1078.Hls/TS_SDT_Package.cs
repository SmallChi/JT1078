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
        /// 表标识符
        /// 可以是0x42,表示描述的是当前流的信息,也可以是0x46,表示是其他流的信息
        /// 8bit
        /// </summary>
        public byte TableId { get; set; } = 0x42;
        /// <summary>
        /// 段语法指示符
        /// 1bit
        /// </summary>
        internal byte SectionSyntaxIndicator { get; set; } = 0x01;
        /// <summary>
        /// 保留未来使用
        /// 1bit
        /// </summary>
        internal byte ReservedFutureUse1 { get; set; } = 0x01;
        /// <summary>
        /// 保留位,防止控制字冲突,一般是''0'',也有可能是''1''
        /// 2bit
        /// </summary>
        internal byte Reserved1 { get; set; } = 0x03;
        /// <summary>
        /// 段长度 从transport_stream_id开始,到CRC_32结束(包含)
        /// 12bit
        /// </summary>
        public ushort SectionLength { get; set; }
        /// <summary>
        /// 传输流标识符
        /// 同 PAT表中的 TransportStreamId 和PMT表中 ProgramNumber
        /// 16bit
        /// </summary>
        internal ushort TransportStreamId { get; set; }
        /// <summary>
        /// 保留位
        /// 2bit
        /// </summary>
        internal byte Reserved2 { get; set; }= 0x03;
        /// <summary>
        /// 版本号
        /// 标识子表的版本号。当子表包含的信息发生变化时， version_number 加 1。当值增至 31 时，复位为 0。
        /// 当 current_next_indicator 置“1”时，则 version_number 为当前使用的子表的版本号。
        /// 当 current_next_indicator 置“0”时，则 version_number 为下一个使用的子表的版本号。
        /// 5bit
        /// </summary>
        public byte VersionNumber { get; set; } = 0x00;
        /// <summary>
        /// 当前后续指示符
        /// 当被置“1”时，表示当前子表  正被使用。
        /// 当其置“0”时，表示所传子表尚未被使用，它是下一个将被使用的子表。
        /// 1bit
        /// </summary>
        public byte CurrentNextIndicator { get; set; } = 0x00;
        /// <summary>
        /// 段号 
        /// 子表中的第一个段的 section_number 标 为 “ 0x00 ”。
        /// 每增加一个具有相同的 table_id 、 transport_stream_id 和original_network_id 的段，section_number 就加 1。
        /// bit8
        /// </summary>
        internal byte SectionNumber { get; set; }=0x00;
        /// <summary>
        /// 最后段号
        /// 表示所属的子表的最后一个段（即段号最大的段）的段号。
        /// bit8
        /// </summary>
        internal byte LastSectionNumber { get; set; } = 0x00;
        /// <summary>
        /// 原始网络标识符
        /// 原始传输系统的 network_id
        /// bit16
        /// </summary>
        internal ushort OriginalNetworkId { get; set; } 
        /// <summary>
        /// 保留未来使用位
        /// bit8
        /// </summary>
        internal byte ReservedFutureUse2 { get; set; } = 0xFF;
        public List<TS_SDT_Service> Services { get; set; }

        /// <summary>
        /// 前面数据的CRC32校验码
        /// </summary>
        public uint CRC32 { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            Header.PackageType = PackageType.SDT;
            Header.ToBuffer(ref writer);
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
            ushort servicesLength =(ushort)( writer.GetCurrentPosition() - SectionLengthPosition-2);
            const int crcLength = 4;
            writer.WriteUInt16Return((ushort)(SectionSyntaxIndicator<<15 | ReservedFutureUse1<<14| Reserved1<<12 | servicesLength+ crcLength), SectionLengthPosition);
            writer.WriteCRC32(5);
            var size = TSConstants.FiexdPackageLength - writer.GetCurrentPosition();
            writer.WriteArray(Enumerable.Range(0, size).Select(s => (byte)0xFF).ToArray());
        }
    }
}
