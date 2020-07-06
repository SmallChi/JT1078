using JT1078.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.Hls.Test
{
    public class M3U8_Test
    {
        /// <summary>
        /// 生成m3u8索引文件
        /// </summary>
        [Fact]
        public void Test4()
        {
            try
            {
                var hls_file_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "terminalno");
                if (!File.Exists(hls_file_directory)) Directory.CreateDirectory(hls_file_directory);
                var m3u8_filepath = Path.Combine(hls_file_directory, "live.m3u8");

                //TSEncoder tSEncoder = new TSEncoder(new M3U8FileManage (new Options.M3U8Option { HlsFileDirectory = hls_file_directory, M3U8Filepath = m3u8_filepath }) );
                //var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                //foreach (var line in lines)
                //{
                //    var data = line.Split(',');
                //    var bytes = data[6].ToHexBytes();
                //    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                //    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                //    if (fullpackage != null)
                //    {
                //        tSEncoder.CreateM3U8File(fullpackage);
                //    }
                //}
                //tSEncoder.AppendM3U8End();
            }
            catch (Exception ex)
            {
                //Assert.Throws<Exception>(() => { });
            }
        }
    }
}
