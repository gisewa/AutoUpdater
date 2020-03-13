using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Events
{
    public class UpdateStatusEventArgs : EventArgs
    {
        public int Code { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 进度
        /// </summary>
        public double ProgressValue { get; set; }
    }

    public class DownloadProgressChangedExEventArgs : EventArgs
    {
        /// <summary>
        /// 进度
        /// </summary>
        public double ProgressValue { get; set; }

        /// <summary>
        /// 已下载文件大小
        /// </summary>
        public double ReceivedSize { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double? TotalSize { get; set; }
    }


    /// <summary>
    /// 下载信息统计
    /// </summary>
    public class DownloadStatisticsEventArgs : EventArgs {

        public DateTime Remaining { get; set; }

        public string Speed { get; set; }
    }
}
