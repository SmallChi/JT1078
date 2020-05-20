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
        /// 第一包的数据、关键帧
        /// Program Clock Reference,节目时钟参考,用于恢复出与编码端一致的系统时序时钟STC（System Time Clock）
        /// 5B
        /// </summary>
        public long PCR { get; set; }
        /// <summary>
        /// 填充字节大小
        /// </summary>
        public byte FillSize { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte((byte)PCRIncluded);
            if (PCRIncluded== PCRInclude.包含)
            {
                writer.WriteInt5(PCR);
#warning PCR 0????
                writer.WriteByte(0);
            }
            if (FillSize > 0)
            {
                writer.WriteArray(Enumerable.Range(0, FillSize).Select(s => (byte)0xFF).ToArray());
            }
        }
    }
}
