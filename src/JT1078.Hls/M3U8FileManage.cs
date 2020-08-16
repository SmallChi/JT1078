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
        public readonly M3U8Option m3U8Option;
        
        public ConcurrentQueue<TsFileInfo> tsFileInfoQueue = new ConcurrentQueue<TsFileInfo>();
        public ConcurrentQueue<string> tsDelFileNameQueue = new ConcurrentQueue<string>();

        ConcurrentDictionary<string, ulong> TsFirst1078PackageDic = new ConcurrentDictionary<string, ulong>();

        public M3U8FileManage(M3U8Option m3U8Option, TSEncoder tSEncoder)
        {
            this.tSEncoder = tSEncoder;
            this.m3U8Option = m3U8Option;
        }

        public void CreateTsData(JT1078Package jt1078Package) {
            string tsFileDirectory = m3U8Option.HlsFileDirectory;
            string tsNewFileName = $"{m3U8Option.TsFileCount}.ts";
            string fileName= Path.Combine(tsFileDirectory, tsNewFileName);
            var buff = TSArrayPool.Rent(jt1078Package.Bodies.Length + 2048);
            TSMessagePackWriter tSMessagePackWriter = new TSMessagePackWriter(buff);
            if (TsFirst1078PackageDic.TryGetValue(jt1078Package.SIM, out var firstTimespan))
            {
                var pes = tSEncoder.CreatePES(jt1078Package, 188);
                tSMessagePackWriter.WriteArray(pes);
                CreateTsFile(fileName, tSMessagePackWriter.FlushAndGetArray());
                if ((jt1078Package.Timestamp - firstTimespan) > 10 * 1000)
                {
                    //按设定的时间（默认为10秒）切分ts文件
                    TsFirst1078PackageDic.TryRemove(jt1078Package.SIM, out var _);
                    tsFileInfoQueue.Enqueue(new TsFileInfo { FileName= tsNewFileName, Duration= (jt1078Package.Timestamp - firstTimespan)/1000.0 });
                    m3U8Option.TsFileCount++;
                }
            }
            else {
                var sdt = tSEncoder.CreateSDT(jt1078Package);
                tSMessagePackWriter.WriteArray(sdt);
                var pat = tSEncoder.CreatePAT(jt1078Package);
                tSMessagePackWriter.WriteArray(pat);
                var pmt = tSEncoder.CreatePMT(jt1078Package);
                tSMessagePackWriter.WriteArray(pmt);
                var pes = tSEncoder.CreatePES(jt1078Package);
                tSMessagePackWriter.WriteArray(pes);
                CreateTsFile(fileName, tSMessagePackWriter.FlushAndGetArray());
                TsFirst1078PackageDic.TryAdd(jt1078Package.SIM, jt1078Package.Timestamp);
            }
        }

        public void CreateM3U8File()
        {
            //ecode_slice_header error  以非关键帧开始的报错信息
            if (tsFileInfoQueue.TryDequeue(out var tsFileInfo))
            {
                string firstTsIndex = string.Empty;
                StringBuilder sb = new StringBuilder();
                List<string> fileHeader = new List<string>() {
                        "#EXTM3U",//开始
                        "#EXT-X-VERSION:3",//版本号
                        "#EXT-X-ALLOW-CACHE:NO",//是否允许cache    
                        $"#EXT-X-TARGETDURATION:{m3U8Option.TsFileMaxSecond}",//第一个TS分片的序列号  
                        $"#EXT-X-MEDIA-SEQUENCE:firstTsIndex"
                    };
                Queue<string> fileBody = new Queue<string>();
                if (!File.Exists(m3U8Option.M3U8Filepath)) File.Create(m3U8Option.M3U8Filepath);
                using(FileStream fs=new FileStream(m3U8Option.M3U8Filepath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var text = sr.ReadLine();
                        if (text.Length == 0) continue;
                        if (fileHeader.Contains(text)) continue;
                        if (text.StartsWith("#EXT-X-MEDIA-SEQUENCE")) continue;
                        fileBody.Enqueue(text);
                    }
                    if (fileBody.Count >= m3U8Option.TsFileCapacity * 2)
                    {
                        var deleteTsFileName_extraInfo = fileBody.Dequeue();
                        var deleteTsFileName = fileBody.Dequeue();
                        tsDelFileNameQueue.Enqueue(deleteTsFileName);

                        fileBody.Enqueue($"#EXTINF:{tsFileInfo.Duration},");
                        fileBody.Enqueue(tsFileInfo.FileName);

                        firstTsIndex = fileBody.ElementAt(1).Replace(".ts", "");
                    }
                    else {
                        fileBody.Enqueue($"#EXTINF:{tsFileInfo.Duration},");
                        fileBody.Enqueue(tsFileInfo.FileName);

                        firstTsIndex = fileBody.ElementAt(1).Replace(".ts", "");
                    }
                    fileHeader.ForEach((m) => {
                        if (m.Contains("firstTsIndex")) m = m.Replace("firstTsIndex", firstTsIndex);
                        sb.AppendLine(m);
                    });
                    fileBody.ToList().ForEach(m => {
                        sb.AppendLine(m);
                    });
                }
                using (FileStream fs = new FileStream(m3U8Option.M3U8Filepath, FileMode.Create,FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sb.ToString());
                }
            }
        }

        /// <summary>
        /// 创建ts文件
        /// </summary>
        /// <param name="fileName">ts文件路径</param>
        /// <param name="data">文件内容</param>
        private void CreateTsFile(string fileName, byte[] data)
        {
            var fileDirectory = fileName.Substring(0, fileName.LastIndexOf("\\"));
            if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);
            using (var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write,FileShare.ReadWrite, data.Length))
            {
                fileStream.Write(data,0,data.Length);
            }
        }

        /// <summary>
        /// 添加结束标识
        /// 直播流用不到
        /// </summary>
        /// <param name="filepath"></param>
        //public void AppendM3U8End()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("#EXT-X-ENDLIST"); //m3u8文件结束符 表示视频已经结束 有这个标志同时也说明当前流是一个非直播流
        //     //#EXT-X-PLAYLIST-TYPE:VOD/Live   //VOD表示当前视频流不是一个直播流，而是点播流(也就是视频的全部ts文件已经生成)
        //}
    }
    /// <summary>
    /// ts文件信息
    /// </summary>
    public class TsFileInfo { 
        public string FileName { get; set; } 
        public double Duration { get; set; }
    }
}
