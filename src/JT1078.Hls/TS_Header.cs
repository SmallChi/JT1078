using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_Header : ITSMessagePackFormatter
    {
        /// <summary>
        /// 同步字节，固定为0x47
        /// </summary>
        internal byte SyncByte { get; set; } = 0x47;
        /// <summary>
        /// 传输错误指示符，表明在ts头的adapt域后由一个无用字节，通常都为0，这个字节算在adapt域长度内
        /// 1bit
        /// </summary>
        internal byte TransportErrorIndicator { get; set; } = 0;
        /// <summary>
        /// 负载单元起始标示符，一个完整的数据包开始时标记为1
        /// 1bit
        /// </summary>
        public byte PayloadUnitStartIndicator { get; set; } = 1;
        /// <summary>
        /// 传输优先级，0为低优先级，1为高优先级，通常取0
        /// 1bit
        /// </summary>
        internal byte TransportPriority { get; set; } = 0;
        /// <summary>
        /// pid值
        /// 13bit
        /// </summary>
        public ushort PID { get; set; } = 0x0011;
        /// <summary>
        /// 传输加扰控制
        /// 2bit
        /// </summary>
        internal byte TransportScramblingControl { get; set; } = 0;
        /// <summary>
        /// 是否包含自适应区，‘00’保留；‘01’为无自适应域，仅含有效负载；‘10’为仅含自适应域，无有效负载；‘11’为同时带有自适应域和有效负载。
        /// 2bit
        /// </summary>
        public AdaptationFieldControl AdaptationFieldControl { get; set; }
        /// <summary>
        /// 递增计数器，从0-f，起始值不一定取0，但必须是连续的
        /// 4bit
        /// </summary>
        public byte ContinuityCounter { get; set; } = 0;
        /// <summary>
        /// 自适应域长度，后面的字节数
        /// 调整字段长度标示，标示此字节后面调整字段的长度，占位8bit；
        /// 值为0时，表示在TS分组中插入一个调整字节，后面没有调整字段，紧跟着的是有效负载；
        /// adaptation_field_control == ‘11’时，此值在0 ~182之间，
        /// adaptation_field_control == ‘10’时，此值为183，若字段没这么长则填充0xFF字段；
        /// </summary>
        public byte AdaptationLength { get; set; }
        /// <summary>
        /// 附加字段
        /// </summary>
        public TS_AdaptationInfo Adaptation { get; set; }

        public PackageType PackageType { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte(SyncByte);
            //TransportErrorIndicator   PayloadUnitStartIndicator   TransportPriority   PID
            //0 1   0   0000 0000 0000 0
            //writer.WriteUInt16((ushort)((0b_0100_0000_0000_0000 & (PayloadUnitStartIndicator<<14)) | PID));
            writer.WriteUInt16((ushort)((TransportErrorIndicator<<15) |(PayloadUnitStartIndicator<<14) | PID));
            writer.WriteByte((byte)((byte)AdaptationFieldControl | ContinuityCounter));
            if(PackageType== PackageType.PAT ||
               PackageType == PackageType.PMT ||
               PackageType == PackageType.Data_Start ||
               PackageType == PackageType.Data_End ||
                PackageType == PackageType.SDT)
            {
                if (Adaptation != null)
                {
                    writer.Skip(1, out int AdaptationLengthPosition);
                    Adaptation.ToBuffer(ref writer);
                    writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - AdaptationLengthPosition - 1), AdaptationLengthPosition);
                }
                else
                {
                    writer.WriteByte(0);
                }
            }
        }
    }
}
