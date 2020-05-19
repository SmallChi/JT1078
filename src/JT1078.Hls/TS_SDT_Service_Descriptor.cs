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
    /// 业务描述服务描述
    /// </summary>
    public class TS_SDT_Service_Descriptor : ITSMessagePackFormatter
    {
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        public byte DescriptorTag { get; set; } = 0x48;
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        internal byte DescriptorLength { get; set; } = 0x12;
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        internal byte ServiceType { get; set; } = 0x01;
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        internal byte ServiceProviderLength { get; set; }
        /// <summary>
        /// 
        /// ServiceProviderLength
        /// </summary>
        public string ServiceProvider { get; set; }
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        internal byte ServiceNameLenth { get; set; }
        /// <summary>
        /// 
        /// ServiceNameLenth
        /// </summary>
        internal string ServiceName { get; set; }
        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte(DescriptorTag);
            writer.Skip(1,out var position);
            writer.WriteByte(ServiceType);
            writer.Skip(1, out var serviceProviderLengthPosition);
            writer.WriteString(ServiceProvider);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - serviceProviderLengthPosition), serviceProviderLengthPosition);
            writer.Skip(1, out int SeviceNameLengthPosition);
            writer.WriteString(ServiceName);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - SeviceNameLengthPosition), SeviceNameLengthPosition);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position), position);
        }
    }
}
