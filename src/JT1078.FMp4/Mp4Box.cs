using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Mp4Box
    {
        //public const string UUID = "uuid";

        const int FixedSizeLength = 4;

        public Mp4Box(string boxType)
        {
            BoxType = boxType;
        }
        ///// <summary>
        ///// 不考虑自定义
        ///// </summary>
        //public Mp4Box(string boxType, string extendedType)
        //{
        //    BoxType = boxType;
        //    if (boxType == UUID)
        //    {
        //       UserType = extendedType;
        //    }
        //}
        /// <summary>
        /// 盒子大小
        /// </summary>
        public uint Size { get; set; }

        ///// <summary>
        ///// 不考虑size==1
        ///// size==1
        ///// </summary>
        //public ulong SizeLarge { get; set; }

        /// <summary>
        /// 盒子类型
        /// </summary>
        public string BoxType { get; set; }

        ///// <summary>
        ///// 不考虑boxtype==‘uuid'
        ///// boxtype==‘uuid’
        ///// int(8)[16]
        ///// </summary>
        //public string UserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SizePosition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void Start(ref FMp4MessagePackWriter writer)
        {
            writer.Skip(FixedSizeLength, out SizePosition);
            writer.WriteASCII(BoxType);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void End(ref FMp4MessagePackWriter writer)
        {
            writer.WriteUInt32Return((uint)(writer.GetCurrentPosition() - SizePosition), SizePosition);
        }
    }
}
