using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Enums
{
    public enum Amf3Type : byte
    {
        Undefined,
        Null,
        False,
        True,
        Integer,
        Double,
        String,
        XmlDocument,
        Date,
        Array,
        Object,
        Xml,
        ByteArray,
        VectorInt,
        VectorUInt,
        VectorDouble,
        VectorObject,
        Dictionary
    }
}
