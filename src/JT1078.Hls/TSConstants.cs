using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls
{
    public static class TSConstants
    {
        static TSConstants()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding = Encoding.GetEncoding("GBK");
        }
        public static Encoding Encoding { get; }
        /// <summary>
        /// 固定包长度
        /// </summary>
        public const int FiexdPackageLength = 188;
        /// <summary>
        /// 固定ES包头的长度
        /// </summary>
        public const int FiexdESPackageHeaderLength = 6;
        
    }
}
