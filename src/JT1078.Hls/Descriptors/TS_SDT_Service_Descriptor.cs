using JT1078.Hls.Enums;
using JT1078.Hls.Interfaces;
using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Hls.Descriptors
{
    /// <summary>
    /// 业务描述服务描述
    /// </summary>
    public class TS_SDT_Service_Descriptor : DescriptorBase
    {
        /// <summary>
        /// 业务描述符
        /// 8bit
        /// </summary>
        public override byte Tag { get; set; } = 0x48;
        /// <summary>
        /// 8bit
        /// </summary>
        public override byte Length { get; set; }
        /// <summary>
        /// 
        /// 8bit
        /// </summary>
        internal TS_SDT_Service_Descriptor_ServiceType ServiceType { get; set; } =  TS_SDT_Service_Descriptor_ServiceType.数字电视业务;
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
        public override void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte(Tag);
            writer.Skip(1,out var position);
            writer.WriteByte((byte)ServiceType);
            writer.Skip(1, out var serviceProviderLengthPosition);
            writer.WriteString(ServiceProvider);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - serviceProviderLengthPosition-1), serviceProviderLengthPosition);
            writer.Skip(1, out int SeviceNameLengthPosition);
            writer.WriteString(ServiceName);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - SeviceNameLengthPosition-1), SeviceNameLengthPosition);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position-1), position);
        }
    }
}
