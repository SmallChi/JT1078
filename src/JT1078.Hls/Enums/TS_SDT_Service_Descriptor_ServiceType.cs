using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Enums
{
    /// <summary>
    /// 服务描述类型
    /// </summary>
    public enum TS_SDT_Service_Descriptor_ServiceType:byte
    {
        保留以后使用=0x00,
        数字电视业务 = 0x01,
        数字无线声音业务 = 0x02,
        图文电视业务 = 0x03,
        NVOD参考业务 = 0x04,
        NVOD时间平移业务 = 0x05,
        镶嵌业务 = 0x06,
        PAL编码信号 = 0x07,
        SECAM编码信号 = 0x08,
        D_D2_MAC = 0x09,
        FM无线 = 0x0A,
        NTSC编码信号 = 0x0B,
        数据广播业务 = 0x0C,
    }
}
