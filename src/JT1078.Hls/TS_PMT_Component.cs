using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_PMT_Component: ITSMessagePackFormatter
    {
        /// <summary>
        /// 流类型，标志是Video还是Audio还是其他数据，h.264编码对应0x1b，aac编码对应0x0f，mp3编码对应0x03
        /// 8bit
        /// </summary>
        public StreamType StreamType { get; set; }
        /// <summary>
        /// 固定为二进制111(7)
        /// 0b_1110_0000_0000_0000
        /// 3bit
        /// </summary>
        internal byte Reserved1 { get; set; } = 0x07;
        /// <summary>
        /// 与StreamType对应的PID
        /// 13bit
        /// </summary>
        public ushort ElementaryPID { get; set; }
        /// <summary>
        /// 固定为二进制1111(15)
        /// 0b_1111_0000_0000_0000
        /// 4bit
        /// </summary>
        internal byte Reserved2 { get; set; } = 0x0F;
        /// <summary>
        /// 描述信息，指定为0x000表示没有
        /// 12bit
        /// </summary>
        internal ushort ESInfoLength { get; set; } = 0x000;
        public DescriptorBase Descriptor { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte((byte)StreamType);
            writer.WriteUInt16((ushort)(0b_1110_0000_0000_0000 | ElementaryPID));
            if (Descriptor == null)
            {
                writer.WriteUInt16((ushort)(0b_1111_0000_0000_0000 | ESInfoLength));
            }
            else
            {
                writer.Skip(2, out int ESInfoLengthPosition);
                Descriptor.ToBuffer(ref writer);
                ESInfoLength = (ushort)(writer.GetCurrentPosition() - ESInfoLengthPosition - 2);
                writer.WriteUInt16Return((ushort)(0b_1111_0000_0000_0000 | ESInfoLength), ESInfoLengthPosition);
            }
        }
    }
}
