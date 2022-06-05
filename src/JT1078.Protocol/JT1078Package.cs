using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    public class JT1078Package: IJT1078AVKey
    {
        /// <summary>
        /// 帧头标识
        /// </summary>
        public static byte[] FH_Bytes = new byte[] { 0x30, 0x31, 0x63, 0x64 };
        /// <summary>
        /// 帧头标识
        /// </summary>
        public const uint FH = 0x30316364;

        public static JT1078Label1 DefaultLabel1 = new JT1078Label1(10, 0, 0, 1);

        /// <summary>
        /// 帧头标识
        /// 固定为0x30 0x31 0x63 0x64
        /// </summary>
        public uint FH_Flag { get; set; } = FH;
        /// <summary>
        /// V  - 2 - 固定为2
        /// P  - 1 - 固定为0
        /// X  - 1 - RTP头是否需要扩展位，固定为0
        /// CC - 4 - 固定为1
        /// 01000001
        /// </summary>
        public JT1078Label1 Label1 { get; set; } = DefaultLabel1;
        /// <summary>
        /// M  - 1 - 标志位，确定是否是完整数据帧的边界
        /// PT - 7 - 负载类型
        /// </summary>
        public JT1078Label2 Label2 { get; set; }
        /// <summary>
        /// 初始化为0，每发送一个RTP数据包，序列号加1
        /// </summary>
        public ushort SN { get; set; }
        /// <summary>
        /// 终端设备SIM卡号
        /// BCD[6]
        /// </summary>
        public string SIM { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNumber { get; set; }
        /// <summary>
        /// 数据类型
        /// 0000：视频I帧
        /// 0001：视频P帧
        /// 0010：视频B帧
        /// 0011：音频帧
        /// 0100：透传数据
        /// 
        /// 0000：原子包，不可被拆分
        /// 0001：分包处理时的第一个包
        /// 0010：分包处理是的最后一个包
        /// 0011：分包处理时间的中间包
        /// </summary>
        public JT1078Label3 Label3 { get; set; }
        /// <summary>
        /// 时间戳
        /// 标识此RTP数据包当前帧的相对时间，单位毫秒（ms）。
        /// 当数据类型为01000时，则没有该字段
        /// </summary>
        public ulong Timestamp { get; set; }
        /// <summary>
        /// 该帧与上一个关键帧之间的时间间隔，单位毫秒(ms),
        /// 当数据类型为非视频帧时，则没有该字段
        /// </summary>
        public ushort LastIFrameInterval { get; set; }
        /// <summary>
        /// 该帧与上一帧之间的时间间隔，单位毫秒(ms),
        /// 当数据类型为非视频帧时，则没有该字段
        /// </summary>
        public ushort LastFrameInterval { get; set; }
        /// <summary>
        /// 后续数据体长度，不含此字段
        /// </summary>
        public ushort DataBodyLength { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies{ get; set; }

        [Obsolete("use GetAVKey()")]
        public string GetKey()
        {
            return $"{SIM}_{LogicChannelNumber.ToString()}";
        }

        public string GetAVKey()
        {
            return $"{SIM}_{LogicChannelNumber.ToString()}";
        }
    }
}
