using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using JT1078.Hls.MessagePack;
using JT1078.Hls.Options;
using JT1078.Protocol;
using JT1078.Protocol.Extensions;

namespace JT1078.Hls
{
    /// <summary>
    /// m3u8文件管理
    /// </summary>
    public class M3U8FileManage
    {
        private TSEncoder tSEncoder;
        private M3U8Option m3U8Option;
        ConcurrentDictionary<string, TsFileInfo> curTsFileInfoDic = new ConcurrentDictionary<string, TsFileInfo>();//当前文件信息
        ConcurrentDictionary<string, Queue<TsFileInfo>> tsFileInfoQueueDic = new ConcurrentDictionary<string, Queue<TsFileInfo>>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m3U8Option"></param>
        public M3U8FileManage(M3U8Option m3U8Option):this(m3U8Option, new TSEncoder())
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m3U8Option"></param>
        /// <param name="tSEncoder"></param>
        public M3U8FileManage(M3U8Option m3U8Option, TSEncoder tSEncoder)
        {
            this.tSEncoder = tSEncoder;
            this.m3U8Option = m3U8Option;
        }

        /// <summary>
        /// 生成ts和m3u8文件
        /// </summary>
        /// <param name="jt1078Package"></param>
        public void CreateTsData(JT1078Package jt1078Package) 
        {         
            string key = jt1078Package.GetKey();
            //string hlsFileDirectory = m3U8Option.HlsFileDirectory;
            //string m3u8FileName = Path.Combine(hlsFileDirectory, key, m3U8Option.M3U8FileName);
            var buff = TSArrayPool.Rent(jt1078Package.Bodies.Length + 1024);
            TSMessagePackWriter tSMessagePackWriter = new TSMessagePackWriter(buff);
            try
            {
                var curTsFileInfo = CreateTsFileInfo(key);
                if (!curTsFileInfo.IsCreateTsFile)
                {
                    var pes = tSEncoder.CreatePES(jt1078Package);
                    tSMessagePackWriter.WriteArray(pes);
                    CreateTsFile(curTsFileInfo.FileName,key, tSMessagePackWriter.FlushAndGetArray());
                    curTsFileInfo.Duration = (jt1078Package.Timestamp - curTsFileInfo.TsFirst1078PackageTimeStamp) / 1000.0;
                    //按设定的时间（默认为10秒）切分ts文件
                    if (curTsFileInfo.Duration > (m3U8Option.TsFileMaxSecond-1))
                    {
                        var tsFileInfoQueue = ManageTsFileInfo(key, curTsFileInfo);
                        CreateM3U8File(curTsFileInfo, tsFileInfoQueue);
                        var newTsFileInfo = new TsFileInfo { IsCreateTsFile = true, Duration = 0, TsFileSerialNo = ++curTsFileInfo.TsFileSerialNo };
                        curTsFileInfoDic.TryUpdate(key, newTsFileInfo, curTsFileInfo);
                    }
                }
                else
                {
                    curTsFileInfo.IsCreateTsFile = false;
                    curTsFileInfo.TsFirst1078PackageTimeStamp = jt1078Package.Timestamp;
                    curTsFileInfo.FileName = $"{curTsFileInfo.TsFileSerialNo}.ts";
                    var sdt = tSEncoder.CreateSDT();
                    tSMessagePackWriter.WriteArray(sdt);
                    var pat = tSEncoder.CreatePAT();
                    tSMessagePackWriter.WriteArray(pat);
                    var pmt = tSEncoder.CreatePMT();
                    tSMessagePackWriter.WriteArray(pmt);
                    var pes = tSEncoder.CreatePES(jt1078Package);
                    tSMessagePackWriter.WriteArray(pes);
                    CreateTsFile(curTsFileInfo.FileName,key, tSMessagePackWriter.FlushAndGetArray());
                }
            }
            finally
            {
                TSArrayPool.Return(buff);
            }
        }
        /// <summary>
        /// 维护TS文件信息队列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="curTsFileInfo"></param>
        /// <returns></returns>
        private Queue<TsFileInfo> ManageTsFileInfo(string key, TsFileInfo curTsFileInfo) 
        {
            if (tsFileInfoQueueDic.TryGetValue(key, out var tsFileInfoQueue))
            {
                if (tsFileInfoQueue.Count >= m3U8Option.TsFileCapacity)
                {
                    var deleteTsFileInfo = tsFileInfoQueue.Dequeue();
                    var deleteTsFileName = Path.Combine(m3U8Option.HlsFileDirectory, key, deleteTsFileInfo.FileName);
                    if (File.Exists(deleteTsFileName)) File.Delete(deleteTsFileName);
                }
                tsFileInfoQueue.Enqueue(curTsFileInfo);
            }
            else
            {
                tsFileInfoQueue = new Queue<TsFileInfo>(new List<TsFileInfo> { curTsFileInfo });
                tsFileInfoQueueDic.TryAdd(key, tsFileInfoQueue);
            }
            return tsFileInfoQueue;
        }

        /// <summary>
        /// 创建M3U8文件
        /// </summary>
        /// <param name="curTsFileInfo">当前ts文件信息</param>
        /// <param name="tsFileInfoQueue">ts文件信息队列</param>
        private void CreateM3U8File(TsFileInfo curTsFileInfo, Queue<TsFileInfo> tsFileInfoQueue)
        {
            //ecode_slice_header error  以非关键帧开始生成的ts，通过ffplay播放会出现报错信息
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXTM3U");//开始
            sb.AppendLine("#EXT-X-VERSION:3");//版本号
            sb.AppendLine("#EXT-X-ALLOW-CACHE:NO");//是否允许cache
            sb.AppendLine($"#EXT-X-TARGETDURATION:{m3U8Option.TsFileMaxSecond}");//第一个TS分片的序列号
            sb.AppendLine($"#EXT-X-MEDIA-SEQUENCE:{(curTsFileInfo.TsFileSerialNo - m3U8Option.TsFileCapacity > 0 ? (curTsFileInfo.TsFileSerialNo - m3U8Option.TsFileCapacity+1) : 0)}"); //默认第一个文件为0
            sb.AppendLine();
            for (int i = 0; i < tsFileInfoQueue.Count; i++)
            {
                var tsFileInfo = tsFileInfoQueue.ElementAt(i);
                sb.AppendLine($"#EXTINF:{tsFileInfo.Duration},");
                sb.AppendLine($"{tsFileInfo.FileName}?{m3U8Option.TsPathSimParamName}={tsFileInfo.Sim}&{m3U8Option.TsPathChannelParamName}={tsFileInfo.ChannelNo}");
            }
            string m3u8FileName = Path.Combine(m3U8Option.HlsFileDirectory,$"{curTsFileInfo.Sim}_{curTsFileInfo.ChannelNo}", m3U8Option.M3U8FileName);
            using (FileStream fs = new FileStream(m3u8FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                var buffer = Encoding.UTF8.GetBytes(sb.ToString());
                fs.Write(buffer,0, buffer.Length);
            }
        }

        /// <summary>
        /// 创建TS文件信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private TsFileInfo CreateTsFileInfo(string key)
        {
            if (!curTsFileInfoDic.TryGetValue(key, out var curTsFileInfo))
            {
                curTsFileInfo = new TsFileInfo()
                {
                    Sim = key.Split('_')[0],
                    ChannelNo = key.Split('_')[1]
                };
                curTsFileInfoDic.TryAdd(key, curTsFileInfo);
            }
            else {
                curTsFileInfo.Sim = key.Split('_')[0];
                curTsFileInfo.ChannelNo = key.Split('_')[1];
            }
            return curTsFileInfo;
        }
        /// <summary>
        /// 创建TS文件
        /// </summary>
        /// <param name="fileName">ts文件路径</param>
        /// <param name="key">终端号_通道号（用作目录）</param>
        /// <param name="data">文件内容</param>
        private void CreateTsFile(string fileName,string key, byte[] data)
        {
            string tsFileName = Path.Combine(m3U8Option.HlsFileDirectory, key, fileName);
            using (var fileStream = new FileStream(tsFileName, FileMode.Append, FileAccess.Write))
            {
                fileStream.Write(data,0,data.Length);
            }
        }

        /// <summary>
        /// 添加结束标识
        /// 直播流用不到
        /// </summary>
        public void AppendM3U8End()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXT-X-ENDLIST"); 
            //m3u8文件结束符 表示视频已经结束 有这个标志同时也说明当前流是一个非直播流
            //#EXT-X-PLAYLIST-TYPE:VOD/Live   //VOD表示当前视频流不是一个直播流，而是点播流(也就是视频的全部ts文件已经生成)
        }

        /// <summary>
        /// 停止观看直播时清零数据
        /// </summary>
        /// <param name="sim"></param>
        /// <param name="channelNo"></param>
        public void Clear(string sim,int channelNo)
        {
            var key = $"{sim}_{channelNo}";
            curTsFileInfoDic.TryRemove(key, out _);
            tsFileInfoQueueDic.TryRemove(key, out _);
            var directory = Path.Combine(m3U8Option.HlsFileDirectory, key);
            if (Directory.Exists(directory)) Directory.Delete(directory);
        }

        /// <summary>
        /// TS文件信息
        /// </summary>
        internal class TsFileInfo
        {
            /// <summary>
            /// 设备手机号
            /// </summary>
            public string Sim { get; set; }
            /// <summary>
            /// 设备逻辑通道号
            /// </summary>
            public string ChannelNo { get; set; }
            /// <summary>
            /// ts文件名
            /// </summary>
            public string FileName { get; set; } = "0.ts";
            /// <summary>
            /// 持续时间
            /// </summary>
            public double Duration { get; set; } = 0;
            /// <summary>
            /// 当前ts文件序号
            /// </summary>
            public int TsFileSerialNo { get; set; } = 0;
            /// <summary>
            /// 是否创建ts文件
            /// </summary>
            public bool IsCreateTsFile { get; set; } = true;
            /// <summary>
            /// ts文件第一个jt1078包的时间戳
            /// </summary>
            public ulong TsFirst1078PackageTimeStamp { get; set; } = 0;
        }
    }

}
