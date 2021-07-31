using JT1078.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Sockets;
using System.Threading;
namespace JT1078.Hls.Test
{
    public class M3U8_Test
    {
        /// <summary>
        /// 模拟发送视频数据
        /// </summary>
        [Fact]
        public void Test1()
        {
            try
            {
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_5.txt"));
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect("127.0.0.1", 10888);
                long lasttime = 0;
                foreach (var line in lines)
                {
                    var temp = line.Split(',');
                    if (lasttime == 0)
                    {
                        lasttime = long.Parse(temp[0]);
                    }
                    else 
                    {
                        var ts = long.Parse(temp[0]) - lasttime;
                        if(ts>0)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(ts));
                        }
                        else if (ts == 0)
                        {
                           
                        }
                        lasttime = long.Parse(temp[0]);
                    }
                    var data= temp[1].ToHexBytes();
                    clientSocket.Send(data);
                }
            }
            catch (Exception ex)
            {
                //Assert.Throws<Exception>(() => { });
            }
        }
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
                var m3u8_filename = Path.Combine(hls_file_directory, "live.m3u8");
                TSEncoder tSEncoder = new TSEncoder();
                var m3u8Manage = new M3U8FileManage(new Options.M3U8Option { HlsFileDirectory = hls_file_directory, M3U8FileName = m3u8_filename }, tSEncoder);
         
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        m3u8Manage.CreateTsData(fullpackage);
                    }
                }
            }
            catch (Exception ex)
            {
                //Assert.Throws<Exception>(() => { });
            }
        }
    }
}
