using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        ArrayPool<byte> arrayPool = ArrayPool<byte>.Create();
        byte[] fileData;
        int fileIndex = 0;

        ConcurrentDictionary<string, ulong> TsFirst1078PackageDic = new ConcurrentDictionary<string, ulong>();

        public M3U8FileManage(M3U8Option m3U8Option, TSEncoder tSEncoder)
        {
            this.tSEncoder = tSEncoder;
            this.m3U8Option = m3U8Option;
            fileData = arrayPool.Rent(2500000);
            //AppendM3U8Start(m3U8Option.TsFileMaxSecond, m3U8Option.TsFileCount);
        }
        /// <summary>
        /// 创建m3u8文件 和 ts文件
        /// </summary>
        /// <param name="jt1078Package"></param>
        public void CreateM3U8File(JT1078Package jt1078Package)
        {
            //CombinedTSData(jt1078Package);
            //if (m3U8FileManage.m3U8Option.AccumulateSeconds >= m3U8FileManage.m3U8Option.TsFileMaxSecond)
            //{
            //    m3U8FileManage.CreateM3U8File(jt1078Package, fileData.AsSpan().Slice(0, fileIndex).ToArray());
            //    arrayPool.Return(fileData);
            //    fileData = arrayPool.Rent(2500000);
            //    fileIndex = 0;
            //}
        }


        private byte[] CreateTsData(JT1078Package jt1078Package, bool isNeedHeader,Span<byte> span) {
            var buff = TSArrayPool.Rent(jt1078Package.Bodies.Length + 2048);
            TSMessagePackWriter tSMessagePackWriter = new TSMessagePackWriter(buff);
            if (TsFirst1078PackageDic.TryGetValue(jt1078Package.SIM, out var firstTimespan))
            {
                if ((jt1078Package.Timestamp - firstTimespan) > 10 * 1000)
                {
                    //按设定的时间（默认为10秒）切分ts文件
                }
                var pes = tSEncoder.CreatePES(jt1078Package, 188);
                tSMessagePackWriter.WriteArray(pes);
            }
            else {
                var sdt = tSEncoder.CreateSDT(jt1078Package);
                tSMessagePackWriter.WriteArray(sdt);
                var pat = tSEncoder.CreatePAT(jt1078Package);
                tSMessagePackWriter.WriteArray(pat);
                var pmt = tSEncoder.CreatePMT(jt1078Package);
                tSMessagePackWriter.WriteArray(pmt);
                var pes = tSEncoder.CreatePES(jt1078Package, 188);
                tSMessagePackWriter.WriteArray(pes);
            }
            return buff;
        }

        public void CreateM3U8File(JT1078Package fullpackage,byte[] data)
        {
            //ecode_slice_header error  以非关键帧开始的报错信息
            //生成一个ts文件
            var ts_name = $"{m3U8Option.TsFileCount}.ts";
            var ts_filepath = Path.Combine(m3U8Option.HlsFileDirectory, ts_name);
            CreateTsFile(ts_filepath, data);

            var media_sequence_no = m3U8Option.TsFileCount - m3U8Option.TsFileCapacity;
            var del_ts_name = $"{media_sequence_no}.ts";
            //更新m3u8文件
            UpdateM3U8File(m3U8Option.AccumulateSeconds, media_sequence_no + 1, del_ts_name, ts_name);

            m3U8Option.IsNeedFirstHeadler = true;
            m3U8Option.AccumulateSeconds = 0;
            m3U8Option.TsFileCount = m3U8Option.TsFileCount + 1;
        }

        public void AppendM3U8Start(int fileMaxSecond, int firstTSSerialno)
        {
            if (File.Exists(m3U8Option.M3U8Filepath)) File.Delete(m3U8Option.M3U8Filepath);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXTM3U");//开始
            sb.AppendLine("#EXT-X-VERSION:3");//版本号
            sb.AppendLine("#EXT-X-ALLOW-CACHE:NO");//是否允许cache    
            sb.AppendLine($"#EXT-X-TARGETDURATION:{fileMaxSecond}");//每个分片TS的最大的时长  
            sb.AppendLine($"#EXT-X-MEDIA-SEQUENCE:{firstTSSerialno}");//第一个TS分片的序列号  
            using (StreamWriter sw = new StreamWriter(m3U8Option.M3U8Filepath, true))
            {
                sw.WriteLine(sb);
            }
        }

        /// <summary>
        /// 添加结束标识
        /// </summary>
        /// <param name="filepath"></param>
        public void AppendM3U8End()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXT-X-ENDLIST"); //m3u8文件结束符 表示视频已经结束 有这个标志同时也说明当前流是一个非直播流
                                             //#EXT-X-PLAYLIST-TYPE:VOD/Live   //VOD表示当前视频流不是一个直播流，而是点播流(也就是视频的全部ts文件已经生成)
            using (StreamWriter sw = new StreamWriter(m3U8Option.M3U8Filepath, true))
            {
                sw.WriteLine(sb);
            }
        }

        /// <summary>
        /// m3u8追加ts文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="tsRealSecond"></param>
        /// <param name="tsName"></param>
        /// <param name="sb"></param>
        public void AppendTsToM3u8(double tsRealSecond, string tsName, StringBuilder sb, bool isAppend = true)
        {
            sb.AppendLine($"#EXTINF:{tsRealSecond},");//extra info，分片TS的信息，如时长，带宽等
            sb.AppendLine($"{tsName}");//文件名
            using (StreamWriter sw = new StreamWriter(m3U8Option.M3U8Filepath, isAppend))
            {
                sw.WriteLine(sb);
            }
        }

        /// <summary>
        /// 更新m3u8文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="tsRealSecond"></param>
        /// <param name="count"></param>
        /// <param name="tsName"></param>
        public void UpdateM3U8File(double tsRealSecond, int media_sequence_no, string del_ts_name, string ts_name)
        {
            StringBuilder sb = new StringBuilder();
            var del_ts_filepath = Path.Combine(m3U8Option.HlsFileDirectory, del_ts_name);
            if (File.Exists(del_ts_filepath))
            {
                //删除最早一个ts文件
                File.Delete(del_ts_filepath);
                bool startAppendFileContent = true;
                bool isFirstEXTINF = true;
                using (StreamReader sr = new StreamReader(m3U8Option.M3U8Filepath))
                {
                    while (!sr.EndOfStream)
                    {
                        var text = sr.ReadLine();
                        if (text.Length == 0) continue;
                        if (text.StartsWith("#EXT-X-MEDIA-SEQUENCE"))
                        {
                            string media_sequence = $"#EXT-X-MEDIA-SEQUENCE:{media_sequence_no}";
                            sb.AppendLine(media_sequence);
                            continue;
                        }
                        if (text.StartsWith("#EXTINF") && isFirstEXTINF)
                        {
                            startAppendFileContent = false;
                            continue;
                        }
                        if (text.StartsWith(del_ts_name) && isFirstEXTINF)
                        {
                            isFirstEXTINF = false;
                            startAppendFileContent = true;
                            continue;
                        }
                        if (startAppendFileContent)
                        {
                            sb.AppendLine(text);
                        }
                    }
                }
                AppendTsToM3u8( tsRealSecond, ts_name, sb, false);
            }
            else
            {
                AppendTsToM3u8(tsRealSecond, ts_name, sb);
            }
        }

        /// <summary>
        /// 创建ts文件
        /// </summary>
        /// <param name="ts_filepath">ts文件路径</param>
        /// <param name="data">文件内容</param>
        public void CreateTsFile(string ts_filepath, byte[] data)
        {
            File.Delete(ts_filepath);
            using (var fileStream = new FileStream(ts_filepath, FileMode.CreateNew, FileAccess.Write))
            {
                fileStream.Write(data,0,data.Length);
            }
        }
    }
}
