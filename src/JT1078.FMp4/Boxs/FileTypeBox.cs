using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// ftyp 盒子相当于就是该 mp4 的纲领性说明。即，告诉解码器它的基本解码版本，兼容格式。简而言之，就是用来告诉客户端，该 MP4 的使用的解码标准。通常，ftyp 都是放在 MP4 的开头。
    /// </summary>
    public class FileTypeBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// ftyp
        /// </summary>
        public FileTypeBox() : base("ftyp")
        {
        }
        /// <summary>
        ///因为兼容性一般可以分为推荐兼容性和默认兼容性。这里 major_brand 就相当于是推荐兼容性。通常，在 Web 中解码，一般而言都是使用 isom 这个万金油即可。如果是需要特定的格式，可以自行定义。
        /// 4位
        /// </summary>
        public string MajorBrand { get; set; }
        /// <summary>
        /// 最低兼容版本
        /// 4位
        /// </summary>
        public string MinorVersion { get; set; } = "\0\0\0\0";
        /// <summary>
        /// 和MajorBrand类似，通常是针对 MP4 中包含的额外格式，比如，AVC，AAC 等相当于的音视频解码格式。
        /// 4位*n
        /// </summary>
        public List<string> CompatibleBrands { get; set; } = new List<string>();

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            writer.WriteASCII(MajorBrand);
            writer.WriteASCII(MinorVersion);
            if(CompatibleBrands!=null && CompatibleBrands.Count > 0)
            {
                foreach(var item in CompatibleBrands)
                {
                    writer.WriteASCII(item);
                }
            }
            End(ref writer);
        }
    }
}
