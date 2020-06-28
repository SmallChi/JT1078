using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// ts文件管理
    /// </summary>
    public class Ts_File_Manage
    {
        /// <summary>
        /// 创建ts文件
        /// </summary>
        /// <param name="ts_filepath">ts文件路径</param>
        /// <param name="data">文件内容</param>
        public void CreateTsFile(string ts_filepath, byte[] data)
        {
            DeleteTsFile(ts_filepath);
            using (var fileStream = new FileStream(ts_filepath, FileMode.CreateNew, FileAccess.Write))
            {
                fileStream.Write(data);
            }
        }

        /// <summary>
        /// 删除ts文件
        /// </summary>
        /// <param name="ts_filepath">ts文件路径</param>
        public void DeleteTsFile(string ts_filepath)
        {
            if (File.Exists(ts_filepath)) File.Delete(ts_filepath);
        }
        /// <summary>
        /// ts文件是否存在
        /// </summary>
        /// <param name="ts_filepath"></param>
        /// <returns></returns>
        public bool ExistTsFile(string ts_filepath) {
            return File.Exists(ts_filepath);
        }
    }
}
