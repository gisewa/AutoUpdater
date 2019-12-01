using AutoUpdate.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Models
{
    public sealed class UpdatePacket : FileBase
    {
        /// <summary>
        /// 下载接收的大小
        /// </summary>
        public long ReceivedBytes { get; set; }

        /// <summary>
        /// 下载的文件大小
        /// </summary>
        public long? TotalBytes { get; set; }

        public int ProgressValue { get; set; }

        /// <summary>
        /// 更新包请求地址
        /// </summary>
        public string Url { get; set; }

        public string Format { get; set; }
    }
}
