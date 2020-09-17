using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT1078.FMp4.Test")]

namespace JT1078.FMp4
{
    public static class FMp4Constants
    {
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        public const int DateLimitYear = 2000;
        public static readonly DateTime UTCBaseTime = new DateTime(1904, 1, 1);
    }
}
