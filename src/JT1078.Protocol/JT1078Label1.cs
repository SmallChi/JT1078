using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    /// <summary>
    /// V  - 2 - 固定为2
    /// P  - 1 - 固定为0
    /// X  - 1 - RTP头是否需要扩展位，固定为0
    /// CC - 4 - 固定为1
    /// </summary>
    public class JT1078Label1
    {
        public JT1078Label1(byte value)
        {
            V = (byte)(value >> 6);
            P = (byte)(value >> 5 & 0x01);
            X = (byte)(value >> 4 & 0x01);
            CC = (byte)(value & 0xF);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">0-3</param>
        /// <param name="p">0-1</param>
        /// <param name="x">0-1</param>
        /// <param name="cc">0-15</param>
        public JT1078Label1(byte v, byte p, byte x, byte cc)
        {
            V = v;
            P = p;
            X = x;
            CC = cc;
        }
        public byte V { get; set; }
        public byte P { get; set; }
        public byte X { get; set; }
        public byte CC { get; set; }
        public byte ToByte()
        {
            return (byte)((V << 6) | (byte)(P << 5) | (byte)(X << 4) | CC);
        }
        public string BinaryCode { get { return ToString(); } }
        public override string ToString()
        {
            return Convert.ToString(ToByte(), 2).PadLeft(8, '0');
        }
    }
}
