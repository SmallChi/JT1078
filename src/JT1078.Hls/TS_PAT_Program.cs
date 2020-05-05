using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public class TS_PAT_Program : ITSMessagePackFormatter
    {
        /// <summary>
        /// 节目号为0x0000时表示这是NIT，节目号为0x0001时,表示这是PMT
        /// 16bit
        /// </summary>
        public ushort ProgramNumber { get; set; }
        /// <summary>
        /// 固定为二进制111(7)
        /// 0b_1110_0000_0000_0000
        /// 3bit
        /// </summary>
        internal byte Reserved1 { get; set; } = 0x07;
        /// <summary>
        /// 节目号对应内容的PID值
        /// 13bit
        /// </summary>
        public ushort PID { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteUInt16(ProgramNumber);
            writer.WriteUInt16((ushort)(0b_1110_0000_0000_0000 | PID));
        }
    }
}
