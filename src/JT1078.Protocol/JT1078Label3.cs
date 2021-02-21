using JT1078.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    /// <summary>
    /// 数据类型
    /// 分包处理标记
    /// </summary>
    public class JT1078Label3
    {
        public JT1078Label3(byte value)
        {
            DataType = (JT1078DataType)(value >> 4);
            SubpackageType = (JT1078SubPackageType)(value & 0x0f);
        }
        public JT1078Label3(JT1078DataType dataType, JT1078SubPackageType subpackageType)
        {
            DataType = dataType;
            SubpackageType = subpackageType;
        }
        public JT1078Label3(JT1078DataType dataType)
        {
            DataType = dataType;
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        public JT1078DataType DataType { get; set; }
        /// <summary>
        /// 分包处理标记
        /// </summary>
        public JT1078SubPackageType SubpackageType { get; set; }
        public string BinaryCode { get { return ToString(); } }
        public byte ToByte()
        {
            return (byte)(((byte)DataType << 4) | (byte)SubpackageType);
        }
        public override string ToString()
        {
            return Convert.ToString(ToByte(), 2).PadLeft(8, '0');
        }
    }
}
