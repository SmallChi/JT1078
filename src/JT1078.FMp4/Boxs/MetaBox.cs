using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MetaBox : FullBox
    {
        public MetaBox(string handlerType,byte version=0, uint flags=0) : base("meta", version, flags)
        {
            HandlerType = handlerType;
        }

        public string HandlerType { get; set; }

        public HandlerBox TheHandler { get; set; }

        public DataInformationBox FileLocations { get; set; }

        public ItemLocationBox ItemLLocations { get; set; }
    }
}
