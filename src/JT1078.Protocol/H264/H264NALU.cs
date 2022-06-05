using JT1078.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.H264
{
    public class H264NALU: IJT1078AVKey
    {
        public readonly static byte[] Start1 = new byte[3] { 0, 0, 1 };
        public readonly static byte[] Start2 = new byte[4] { 0, 0, 0, 1 };
        public byte[] StartCodePrefix { get; set; }
        public NALUHeader NALUHeader { get; set; }
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
        /// </summary>
        public JT1078DataType DataType { get; set; }
        /// <summary>
        /// 该帧与上一个关键帧之间的时间间隔，单位毫秒(ms),
        /// 当数据类型为非视频帧时，则没有该字段
        /// </summary>
        public ushort LastIFrameInterval { get; set; }
        /// <summary>
        /// 该帧与上一个帧之间的时间间隔，单位毫秒(ms),
        /// 当数据类型为非视频帧时，则没有该字段
        /// </summary>
        public ushort LastFrameInterval { get; set; }
        /// <summary>
        /// 时间戳
        /// 标识此RTP数据包当前帧的相对时间，单位毫秒（ms）。
        /// 当数据类型为01000时，则没有该字段
        /// </summary>
        public ulong Timestamp { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] RawData { get; set; }

        public string GetAVKey()
        {
            return $"{SIM}_{LogicChannelNumber.ToString()}";
        }

        [Obsolete("use GetAVKey")]
        public string GetKey()
        {
            return $"{SIM}_{LogicChannelNumber.ToString()}";
        }
    }
}
