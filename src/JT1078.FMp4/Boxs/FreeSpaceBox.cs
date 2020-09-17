using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public abstract class FreeSpaceBox: Mp4Box
    {
        public FreeSpaceBox(string boxType) : base(boxType)
        {
        }
        /// <summary>
        /// 填充值
        /// </summary>
        public byte FillValue { get; set; }
        /// <summary>
        /// 填充数量
        /// </summary>
        public int FillCount { get; set; }
    }
}
