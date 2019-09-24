using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public interface IAmf3Metadata
    {
        ushort FieldNameLength { get; set; }
        string FieldName { get; set; }
        byte DataType { get; set; }
        object Value { get; set; }
        ReadOnlySpan<byte> ToBuffer();
    }
}
