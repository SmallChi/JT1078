using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Enums
{
    public enum JT809_JT1078_BusinessType : ushort
    {
        ///<summary>
        ///主链路时效口令业务类
        ///UP_AUTHORIZE_MSG
        ///</summary>
        [Description("主链路时效口令业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1700))]
        [JT809BusinessTypeDescription("UP_AUTHORIZE_MSG", "主链路时效口令业务类")]
        主链路时效口令业务类 = 0x1700,
        ///<summary>
        ///从链路时效口令业务类
        ///DOWN_AUTHORIZE_MSG
        ///</summary>
        [Description("从链路时效口令业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9700))]
        [JT809BusinessTypeDescription("DOWN_AUTHORIZE_MSG", "从链路时效口令业务类")]
        从链路时效口令业务类 = 0x9700,
        ///<summary>
        ///主链路实时音视频业务类
        ///UP_REALVIDEO_MSG
        ///</summary>
        [Description("主链路实时音视频业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1800))]
        [JT809BusinessTypeDescription("UP_REALVIDEO_MSG", "主链路实时音视频业务类")]
        主链路实时音视频业务类 = 0x1800,
        ///<summary>
        ///从链路实时音视频业务类
        ///DOWN_REALVIDEO_MSG
        ///</summary>
        [Description("从链路实时音视频业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9800))]
        [JT809BusinessTypeDescription("DOWN_REALVIDEO_MSG", "从链路实时音视频业务类")]
        从链路实时音视频业务类 = 0x9800,
        ///<summary>
        ///主链路远程录像检索业务类
        ///UP_SEARCH_MSG
        ///</summary>
        [Description("主链路远程录像检索业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1900))]
        [JT809BusinessTypeDescription("UP_SEARCH_MSG", "主链路远程录像检索业务类")]
        主链路远程录像检索业务类 = 0x1900,
        ///<summary>
        ///从链路远程录像检索业务类
        ///DOWN_SEARCH_MSG
        ///</summary>
        [Description("从链路远程录像检索业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9900))]
        [JT809BusinessTypeDescription("DOWN_SEARCH_MSG", "从链路远程录像检索业务类")]
        从链路远程录像检索业务类 = 0x9900,
        ///<summary>
        ///主链路远程录像回放业务类
        ///UP_PLAYBACK_MSG
        ///</summary>
        [Description("主链路远程录像回放业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1A00))]
        [JT809BusinessTypeDescription("UP_PLAYBACK_MSG", "主链路远程录像回放业务类")]
        主链路远程录像回放业务类 = 0x1A00,
        ///<summary>
        ///从链路远程录像回放业务类
        ///DOWN_PLAYBACK_MSG
        ///</summary>
        [Description("从链路远程录像回放业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9A00))]
        [JT809BusinessTypeDescription("DOWN_PLAYBACK_MSG", "从链路远程录像回放业务类")]
        从链路远程录像回放业务类 = 0x9A00,
        ///<summary>
        ///主链路远程录像下载业务类
        ///UP_DOWNLOAD_MSG
        ///</summary>
        [Description("主链路远程录像下载业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1B00))]
        [JT809BusinessTypeDescription("UP_DOWNLOAD_MSG", "主链路远程录像下载业务类")]
        主链路远程录像下载业务类 = 0x1B00,
        ///<summary>
        ///从链路远程录像下载业务类
        ///DOWN_DOWNLOAD_MSG
        ///</summary>
        [Description("从链路远程录像下载业务类")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9B00))]
        [JT809BusinessTypeDescription("DOWN_DOWNLOAD_MSG", "从链路远程录像下载业务类")]
        从链路远程录像下载业务类 = 0x9B00,
    }
}
