using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class UserDataBox : Mp4Box, IFMp4MessagePackFormatter
    {
        public UserDataBox() : base("udta")
        {
        }

        public byte[] Data { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            if(Data!=null && Data.Length > 0)
            {
                writer.WriteArray(Data);
            }
            End(ref writer);
        }
    }
}
