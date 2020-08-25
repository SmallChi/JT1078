using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace JT1078.Flv.Test.Audio
{
    public class AudioTest
    {
        readonly ITestOutputHelper output;

        public AudioTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// 8000采样，单通道，16位pcm->aac
        /// <para>8k-1ch-16bit</para>
        /// </summary>
        [Fact(DisplayName = "pcm编码aac")]
        public void Test1()
        {
            //todo:音频暂时先放下
            ReadOnlySpan<byte> fileData = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Audio/Files/testpacket.pcm"));
            //注意 这里为了可以判断音频是否可用，因此使用adts，当网络传输的时候不应该使用adts
            //var faac = new FaacEncoder_x64(8000, 1, 16, true);
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\Files\testpacket.aac");
            //if (File.Exists(path)) File.Delete(path);
            //output.WriteLine(path);
            //var offset = 0;
            //var step = faac.frameSize;
            //var totalBytes = 0;
            //var stopwatch = new Stopwatch();
            //while (offset + step < fileData.Length)
            //{
            //    stopwatch.Start();
            //    var aacBuff = faac.Encode(fileData.Slice(offset, step).ToArray());
            //    stopwatch.Stop();
            //    if (aacBuff.Any())
            //        aacBuff.AppendBytesToFile(path);
            //    offset += step;
            //    totalBytes += aacBuff.Length;
            //}
            //faac.Dispose();
            //output.WriteLine($"已编码字节数：{offset}，剩余未编码字节数：{fileData.Length - offset}，编码后字节数：{totalBytes}，耗时：{stopwatch.Elapsed.Milliseconds}毫秒");
        }
    }
    static class Ex
    {
        public static void AppendBytesToFile(this byte[] fileBytes, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Append);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
        }
    }
}
