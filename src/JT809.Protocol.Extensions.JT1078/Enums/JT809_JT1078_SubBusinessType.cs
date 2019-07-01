using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Enums
{
    public enum JT809_JT1078_SubBusinessType : ushort
    {
        ///<summary>
        ///时效口令上报消息
        ///UP_AUTHRIZE_MSG_STARTUP
        ///</summary>
        [Description("时效口令上报消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1700_0x1701))]
        [JT809BusinessTypeDescription("UP_AUTHRIZE_MSG_STARTUP", "时效口令上报消息")]
        时效口令上报消息 = 0x1701,
        ///<summary>
        ///时效口令请求消息
        ///UP_AUTHRIZE_MSG_STARTUP_REQ
        ///</summary>
        [Description("时效口令请求消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1700_0x1702))]
        [JT809BusinessTypeDescription("UP_AUTHRIZE_MSG_STARTUP_REQ", "时效口令请求消息")]
        时效口令请求消息 = 0x1702,
        ///<summary>
        ///时效口令请求应答消息
        ///DOWN_AUTHRIZE_MSG_STARTUP_REQ_ACK
        ///</summary>
        [Description("时效口令请求应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9700_0x9702))]
        [JT809BusinessTypeDescription("DOWN_AUTHRIZE_MSG_STARTUP_REQ_ACK", "时效口令请求应答消息")]
        时效口令请求应答消息 = 0x9702,
        ///<summary>
        ///实时音视频请求应答消息
        ///UP_REALVIDEO_MSG_STARTUP_ACK
        ///</summary>
        [Description("实时音视频请求应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1800_0x1801))]
        [JT809BusinessTypeDescription("UP_REALVIDEO_MSG_STARTUP_ACK", "实时音视频请求应答消息")]
        实时音视频请求应答消息 = 0x1801,
        ///<summary>
        ///主动请求停止实时音视频传输应答消息
        ///UP_REALVIDEO_MSG_END_ACK
        ///</summary>
        [Description("主动请求停止实时音视频传输应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1800_0x1802))]
        [JT809BusinessTypeDescription("UP_REALVIDEO_MSG_END_ACK", "主动请求停止实时音视频传输应答消息")]
        主动请求停止实时音视频传输应答消息 = 0x1802,
        ///<summary>
        ///实时音视频请求消息
        ///DOWN_REALVIDEO_MSG_STARTUP
        ///</summary>
        [Description("实时音视频请求消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9800_0x9801))]
        [JT809BusinessTypeDescription("DOWN_REALVIDEO_MSG_STARTUP", "实时音视频请求消息")]
        实时音视频请求消息 = 0x9801,
        ///<summary>
        ///主动请求停止实时音视频传输消息
        ///DOWN_REALVIDEO_MSG_END
        ///</summary>
        [Description("主动请求停止实时音视频传输消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9800_0x9802))]
        [JT809BusinessTypeDescription("DOWN_REALVIDEO_MSG_END", "主动请求停止实时音视频传输消息")]
        主动请求停止实时音视频传输消息 = 0x9802,
        ///<summary>
        ///主动上传音视频资源目录信息消息
        ///UP_FILELIST_MSG
        ///</summary>
        [Description("主动上传音视频资源目录信息消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1900_0x1901))]
        [JT809BusinessTypeDescription("UP_FILELIST_MSG", "主动上传音视频资源目录信息消息")]
        主动上传音视频资源目录信息消息 = 0x1901,
        ///<summary>
        ///查询音视频资源目录应答消息
        ///UP_REALVIDEO_FILELIST_REQ_ACK
        ///</summary>
        [Description("查询音视频资源目录应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1900_0x1902))]
        [JT809BusinessTypeDescription("UP_REALVIDEO_FILELIST_REQ_ACK", "查询音视频资源目录应答消息")]
        查询音视频资源目录应答消息 = 0x1902,
        ///<summary>
        ///主动上传音视频资源目录信息应答消息
        ///DOWN_FILELIST_MSG_ACK
        ///</summary>
        [Description("主动上传音视频资源目录信息应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9900_0x9901))]
        [JT809BusinessTypeDescription("DOWN_FILELIST_MSG_ACK", "主动上传音视频资源目录信息应答消息")]
        主动上传音视频资源目录信息应答消息 = 0x9901,
        ///<summary>
        /// 查询音视频资源目录请求消息
        ///DOWN_REALVIDEO_FILELIST_REQ
        ///</summary>
        [Description("查询音视频资源目录请求消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9900_0x9902))]
        [JT809BusinessTypeDescription("DOWN_REALVIDEO_FILELIST_REQ", "查询音视频资源目录请求消息")]
        查询音视频资源目录请求消息 = 0x9902,
        ///<summary>
        ///远程录像回放请求应答消息
        ///UP_PLAYBACK_MSG_STARTUP_ACK
        ///</summary>
        [Description("远程录像回放请求应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1A00_0x1A01))]
        [JT809BusinessTypeDescription("UP_PLAYBACK_MSG_STARTUP_ACK", "远程录像回放请求应答消息")]
        远程录像回放请求应答消息 = 0x1A01,
        ///<summary>
        ///远程录像回放控制应答消息
        ///UP_PLAYBACK_MSG_CONTROL_ACK
        ///</summary>
        [Description("远程录像回放控制应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1A00_0x1A02))]
        [JT809BusinessTypeDescription("UP_PLAYBACK_MSG_CONTROL_ACK", "远程录像回放控制应答消息")]
        远程录像回放控制应答消息 = 0x1A02,
        ///<summary>
        ///远程录像回放请求消息
        ///DOWN_PLAYBACK_MSG_STARTUP
        ///</summary>
        [Description("远程录像回放请求消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9A00_0x9A01))]
        [JT809BusinessTypeDescription("DOWN_PLAYBACK_MSG_STARTUP", "远程录像回放请求消息")]
        远程录像回放请求消息 = 0x9A01,
        ///<summary>
        ///远程录像回放控制消息
        ///DOWN_PLAYBACK_MSG_CONTROL
        ///</summary>
        [Description("远程录像回放控制消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9A00_0x9A02))]
        [JT809BusinessTypeDescription("DOWN_PLAYBACK_MSG_CONTROL", "远程录像回放控制消息")]
        远程录像回放控制消息 = 0x9A02,
        ///<summary>
        ///远程录像下载请求应答消息
        ///UP_DOWNLOAD_MSG_STARTUP_ACK
        ///</summary>
        [Description("远程录像下载请求应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1B00_0x1B01))]
        [JT809BusinessTypeDescription("UP_DOWNLOAD_MSG_STARTUP_ACK", "远程录像下载请求应答消息")]
        远程录像下载请求应答消息 = 0x1B01,
        ///<summary>
        ///远程录像下载通知消息
        ///UP_DOWNLOAD_MSG_END_INFORM
        ///</summary>
        [Description("远程录像下载通知消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1B00_0x1B02))]
        [JT809BusinessTypeDescription("UP_DOWNLOAD_MSG_END_INFORM", "远程录像下载通知消息")]
        远程录像下载通知消息 = 0x1B02,
        ///<summary>
        ///远程录像下载控制应答消息
        ///UP_DOWNLOAD_MSG_CONTROL_ACK
        ///</summary>
        [Description("远程录像下载控制应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x1B00_0x1B03))]
        [JT809BusinessTypeDescription("UP_DOWNLOAD_MSG_CONTROL_ACK", "远程录像下载控制应答消息")]
        远程录像下载控制应答消息 = 0x1B03,
        ///<summary>
        ///远程录像下载请求消息
        ///DOWN_DOWNLOAD_MSG_STARTUP
        ///</summary>
        [Description("远程录像下载请求消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9B00_0x9B01))]
        [JT809BusinessTypeDescription("DOWN_DOWNLOAD_MSG_STARTUP", "远程录像下载请求消息")]
        远程录像下载请求消息 = 0x9B01,
        ///<summary>
        ///远程录像下载完成通知应答消息
        ///UP_DWONLOAD_MSG_END_INFORM_ACK
        ///</summary>
        [Description("远程录像下载完成通知应答消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9B00_0x9B02))]
        [JT809BusinessTypeDescription("UP_DWONLOAD_MSG_END_INFORM_ACK", "远程录像下载完成通知应答消息")]
        远程录像下载完成通知应答消息 = 0x9B02,
        ///<summary>
        ///远程录像下载控制消息
        ///DWON_DOWNLOAD_MSG_CONTROL
        ///</summary>
        [Description("远程录像下载控制消息")]
        [JT809BodiesType(typeof(JT809_JT1078_0x9B00_0x9B03))]
        [JT809BusinessTypeDescription("DWON_DOWNLOAD_MSG_CONTROL", "远程录像下载控制消息")]
        远程录像下载控制消息 = 0x9B03,
    }
}
