using JT1078.Protocol.Enums;
using System;
using System.Text;

namespace JT1078.Protocol
{
    /// <summary>
    /// M  - 1 - 标志位，确定是否是完整数据帧的边界
    /// PT - 7 - 负载类型
    /// </summary>
    public class JT1078Label2
    {
        public JT1078Label2(byte value)
        {
            M =  (byte)(value >> 7);
            PT = (JT1078AVType)(value & 0x7f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">0-1</param>
        /// <param name="pt">0-127</param>
        public JT1078Label2(byte m, JT1078AVType pt)
        {
            M = m;
            PT = pt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">0-1</param>
        /// <param name="pt">0-127</param>
        public JT1078Label2(byte m,byte pt)
        {
            M = m;
            PT = (JT1078AVType)pt;
        }

        /// <summary>
        /// M  - 1 - 标志位，确定是否是完整数据帧的边界
        /// </summary>
        public byte M { get; set; }
        /// <summary>
        /// PT - 7 - 负载类型
        /// 用于说明RTP报文中有效载荷的类型，如GSM音频、JPEM图像等
        /// </summary>
        public JT1078AVType PT { get; set; }

        public byte ToByte()
        {
            return (byte)((M << 7) | (byte)PT);
        }

        public string BinaryCode { get { return ToString(); } }
        public override string ToString()
        {
            return Convert.ToString(ToByte(), 2).PadLeft(8, '0');
        }
    }
}
