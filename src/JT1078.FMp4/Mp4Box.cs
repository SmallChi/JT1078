using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public abstract class Mp4Box
    {
        public const string UUID = "uuid";
        public Mp4Box(string boxType)
        {
            BoxType = boxType;
        }
        public Mp4Box(string boxType,string extendedType)
        {
            BoxType = boxType;
            if(boxType == UUID)
            {
                UserType = extendedType;
            }
        }
        public string BoxType { get; set; }
        public string UserType { get; set; }
        public uint Size { get; set; }
        public ulong SizeLarge{get;set;}
    }
}
