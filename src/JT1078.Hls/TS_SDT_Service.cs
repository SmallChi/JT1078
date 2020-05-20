using JT1078.Hls.Descriptors;
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
    /// 业务描述服务
    /// </summary>
    public class TS_SDT_Service : ITSMessagePackFormatter
    {
        /// <summary>
        /// 业务标识符
        /// 用于在 TS 流中识别不同的业务。service_id 与program_map_section 中的 program_number 取同一值
        /// 16bit
        /// </summary>
        internal ushort ServiceId { get; set; }
        /// <summary>
        /// 保留将来使用
        /// 6bit
        /// </summary>
        internal byte ReservedFutureUse { get; set; } = 0x3F;
        /// <summary>
        /// EIT 时间表标志
        /// 置“1”时，表示业务的 EIT 时间表信息存在于当前 TS 中（一个 EIT 时间表子表两次出现的最大时间间隔信息见 ETR 211）。
        /// 置“0”时，表示业务的 EIT 时间表信息不在当前 TS 中
        /// 1bit
        /// </summary>
        internal byte EITScheduleFlag { get; set; } = 0x00;
        /// <summary>
        /// EIT 当前后续标志
        /// 置“1”时，表示业务的 EIT  当前后续信息存在于当前 TS 中（一个 EIT 当前后续子表两次出现的最大时间间隔信息见ETR 211）
        /// 置“0”时，表示业务的 EIT 当前后续信息不在当前 TS 中。
        /// 1bit
        /// </summary>
        public byte EITPresentFollowingFlag { get; set; } = 0x00;
        /// <summary>
        /// 运行状态
        /// 对于一个 NVOD 业务，running_status 的值都置“0”
        /// 3bit
        /// </summary>
        internal TS_SDT_Service_RunningStatus RunningStatus { get; set; }
        /// <summary>
        /// 自由条件接收模式
        /// 置“0”时，表示业务的所有组件都未被加扰
        /// 置“1”时，表示一路或多路码流的接收由 CA 系统控制。
        /// 1bit
        /// </summary>
        internal byte FreeCAMode { get; set; } = 0x00;
        /// <summary>
        /// 描述符循环长度
        /// 指出从本字段的下一个字节开始的描述符的总字节长度。
        /// 12bit
        /// </summary>
        public ushort DescriptorsLoopLength { get; set; }
       
        public List<TS_SDT_Service_Descriptor> Descriptors { get; set; }

        public void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteUInt16(ServiceId);
            writer.WriteByte((byte)(ReservedFutureUse << 2 | EITScheduleFlag << 1 | EITPresentFollowingFlag));
            writer.Skip(2, out var position);
            foreach (var descriptor in Descriptors)
            {
                descriptor.ToBuffer(ref writer);
            }
            DescriptorsLoopLength = (ushort)(writer.GetCurrentPosition() - position-2);
            writer.WriteUInt16Return((ushort)((ushort)RunningStatus << 13 | (ushort)FreeCAMode << 12 | DescriptorsLoopLength), position);
        }
    }
}
