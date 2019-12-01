using AutoUpdate.Core;
using AutoUpdate.Core.Strategys;
using AutoUpdate.Core.Update;
using System;

namespace AutoApdate.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string installPath = @"E:\testdownload\";
            string downloadPath = @"E:\testdownload\";

            AutoUpdateBootstrap bootstrap = new AutoUpdateBootstrap();
            bootstrap.DownloadProgressChangedEx += OnDownloadProgressChangedEx;
            bootstrap.DownloadStatistics += OnDownloadStatistics;
            bootstrap.UpdateStatusChanged += OnUpdateStatusChanged;
            bootstrap.Strategy<Silent>()
                .Option(UpdateOption.Format, "zip")
                .RemoteAddress("192.168.50.225", 7000, "version1.0", "update", downloadPath, installPath, "f5797866b13def762f88c52dec15c762")
                .Launch();
            Console.Read();
        }

        private static void OnUpdateStatusChanged(object sender, AutoUpdate.Core.Events.UpdateStatusEventArgs e)
        {
            Console.WriteLine($"状态码：{e.Code},下载状态：{e.Status}, 整体更新进度：{e.ProgressValue}");
        }

        private static void OnDownloadStatistics(object sender, AutoUpdate.Core.Events.DownloadStatisticsEventArgs e)
        {
            Console.WriteLine($"剩余下载时间：{e.Remaining.Second},下载速度：{e.Speed}");
        }

        private static void OnDownloadProgressChangedEx(object sender, AutoUpdate.Core.Events.DownloadProgressChangedExEventArgs e)
        {
            Console.WriteLine($"文件下载进度：{e.ProgressValue}，已下载文件大小：{e.ReceivedSize}，文件大小：{e.TotalSize}");
        }
    }
}
