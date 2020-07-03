using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Hls
{
    public class TS_AdaptationInfo : ITSMessagePackFormatter
    {
        /// <summary>
        /// 取0x50表示包含PCR或0x40表示不包含PCR
        /// 1B
        /// </summary>
        public PCRInclude PCRIncluded { get; set; }
        /// <summary>
        /// JT1078时间戳
        /// 第一包的数据、关键帧
        /// Program Clock Reference,节目时钟参考,用于恢复出与编码端一致的系统时序时钟STC（System Time Clock）
        /// 5B
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 填充字节大小
        /// </summary>
        public byte FillSize { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte((byte)PCRIncluded);
            if (PCRIncluded == PCRInclude.包含)
            {
                writer.WritePCR(Timestamp);
            }
            if (FillSize > 0)
            {
                writer.WriteArray(Enumerable.Range(0, FillSize).Select(s => (byte)0xFF).ToArray());
            }
        }

        /// <summary>
        ///  if(PCR_flag == '1'){
        ///    program_clock_reference_base             33              uimsbf
        ///    Reserved                                 6               bslbf
        ///    program_clock_reference_extension        9               uimsbf
        ///  }
        /// </summary>
        /// <returns></returns>
        //private long ToPCR()
        //{
        //    return (Timestamp / 300 << 15 | 0x7E00);
        //} 
    }
}
