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
    /// 格式节目映射表
    /// </summary>
    public class TS_PMT_Package : ITSMessagePackFormatter
    {
        public TS_Header Header { get; set; }
        /// <summary>
        /// PMT表取值随意
        /// 8bit
        /// </summary>
        public byte TableId { get; set; } = 0xFF;
        /// <summary>
        /// 固定为二进制1
        /// 1bit
        /// </summary>
        internal byte SectionSyntaxIndicator { get; set; } = 0x01;
        /// <summary>
        /// 固定为二进制0
        /// 1bit
        /// </summary>
        internal byte Zero { get; set; } = 0x00;
        /// <summary>
        /// 固定为二进制3
        /// 2bit
        /// </summary>
        internal byte Reserved1 { get; set; } = 0x03;
        /// <summary>
        /// 后面数据的长度
        /// 12bit
        /// </summary>
        public ushort SectionLength { get; set; }
        /// <summary>
        /// 频道号码,表示当前的PMT关联到的频道,取值0x0001
        /// 16bit
        /// </summary>
        public ushort ProgramNumber { get; set; } = 0x0001;
        /// <summary>
        /// 固定为二进制3
        /// 2bit
        /// </summary>
        internal byte Reserved2 { get; set; } = 0x03;
        /// <summary>
        /// 版本号，固定为二进制00000，如果PAT有变化则版本号加1
        /// 5bit
        /// </summary>
        public byte VersionNumber { get; set; } = 0x00;
        /// <summary>
        /// 固定为二进制1，表示这个PAT表可以用，如果为0则要等待下一个PAT表
        /// 1bit
        /// </summary>
        public byte CurrentNextIndicator { get; set; } = 1;
        /// <summary>
        /// 固定为0x00
        /// bit8
        /// </summary>
        internal byte SectionNumber { get; set; } = 0x00;
        /// <summary>
        /// 固定为0x00
        /// bit8
        /// </summary>
        internal byte LastSectionNumber { get; set; } = 0x00;
        /// <summary>
        /// 固定为二进制111(7)
        /// 2bit
        /// </summary>
        internal byte Reserved3 { get; set; } = 0x07;
        /// <summary>
        /// PCR(节目参考时钟)所在TS分组的PID，指定为视频PID
        /// 13bit
        /// </summary>
        public ushort PCR_PID { get; set; }
        /// <summary>
        /// 固定为二进制1111(F)
        /// 4bit
        /// </summary>
        internal byte Reserved4 { get; set; } = 0x0F;
        /// <summary>
        /// 节目描述信息,指定为0x000表示没有
        /// 12bit
        /// </summary>
        public ushort ProgramInfoLength { get; set; }      
        public List<TS_PMT_Component> Components { get; set; }
        /// <summary>
        /// 前面数据的CRC32校验码
        /// </summary>
        public uint CRC32 { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            Header.PackageType = PackageType.PMT;
            Header.ToBuffer(ref writer);
            writer.WriteByte(TableId);
            //SectionSyntaxIndicator   Zero  Reserved1   SectionLength
            //1 0 11 0000 0000 0000
            //(ushort)(0b_1011_0000_0000_0000 | SectionLength)
            writer.Skip(2, out int SectionLengthPosition);
            writer.WriteUInt16(ProgramNumber);
            //Reserved2 VersionNumber CurrentNextIndicator
            //11 00000 1
            var a = 0xC0 & (Reserved2 << 6);
            var b = 0x3E & (VersionNumber << 3);
            var c = (byte)(a | b | CurrentNextIndicator);
            writer.WriteByte(c);
            writer.WriteByte(SectionNumber);
            writer.WriteByte(LastSectionNumber);
            //Reserved3 PCR_PID
            //111   0000 0000 0000 0
            writer.WriteUInt16((ushort)(0b_1110_0000_0000_0000 | PCR_PID));
            //Reserved4 ProgramInfoLength
            //1111 0000 0000 0000
            writer.WriteUInt16((ushort)(0b_1111_0000_0000_0000 | ProgramInfoLength));
            if (Components != null)
            {
                foreach(var component in Components)
                {
                    component.ToBuffer(ref writer);
                }
            }
            const int crcLength = 4;
            writer.WriteUInt16Return((ushort)(0b_1011_0000_0000_0000 | (ushort)(writer.GetCurrentPosition() - SectionLengthPosition - 2) + crcLength), SectionLengthPosition);
            //打包ts流时PAT和PMT表是没有adaptation field的，不够的长度直接补0xff即可。
            //ts header(4B) + adaptation field length(1)
            writer.WriteCRC32(5);
            var size = TSConstants.FiexdPackageLength - writer.GetCurrentPosition();
            writer.WriteArray(Enumerable.Range(0, size).Select(s => (byte)0xFF).ToArray());
        }
    }
}
