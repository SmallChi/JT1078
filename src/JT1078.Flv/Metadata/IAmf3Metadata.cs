using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public interface IAmf3Metadata
    {
        /// <summary>
        /// 字段长度
        /// </summary>
        ushort FieldNameLength { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        string FieldName { get; set; }
        /// <summary>
        /// Amf3数据类型
        /// ref:video_file_format_spec_v10.pdf scriptdatavalue  type
        /// </summary>
        byte DataType { get; set; }
        /// <summary>
        /// 对应的值
        /// </summary>
        object Value { get; set; }
        ReadOnlySpan<byte> ToBuffer();
    }
}
