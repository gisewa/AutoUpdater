using AutoUpdate.Core;
using AutoUpdate.Core.Strategys;
using AutoUpdate.Core.Update;
using System;

namespace AutoApdate.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// quick start
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region Launch1

            args = new string[6] {
                "0.0.0.0",
                "1.1.1.1",
                "https://github.com/WELL-E",
                 "http://localhost：9090/UpdateFile.zip",
                 @"E:\PlatformPath",
                "da9266fffb21617b1b055cb9ec92f74a",
                 };

            AutoUpdateBootstrap bootstrap = new AutoUpdateBootstrap();
            bootstrap.DownloadStatistics += OnDownloadStatistics;
            bootstrap.ProgressChanged += OnProgressChanged;
            bootstrap.Strategy<DefultStrategy>().
                Option(UpdateOption.Format, "zip").
                Option(UpdateOption.MainApp, "KGS.CPP").
                RemoteAddress(args).
                Launch();

            #endregion

            #region Launch2

            AutoUpdateBootstrap bootstrap2 = new AutoUpdateBootstrap();
            bootstrap2.DownloadStatistics += OnDownloadStatistics;
            bootstrap2.ProgressChanged += OnProgressChanged;
            bootstrap2.Strategy<DefultStrategy>().
                Option(UpdateOption.Format, "zip").
                Option(UpdateOption.MainApp, "KGS.CPP").
                RemoteAddress(@"https://api.com/AutoUpdate?version=1.0.0.1").
                Launch();

            #endregion

            Console.Read();
        }

        private static void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.Type == ProgressType.Donwload)
            {
                var str = $"Donwload进度：{e.ProgressValue}";
                Console.WriteLine(str);
            }

            if (e.Type == ProgressType.Updatefile)
            {
                var str = $"当前更新第：{e.ProgressValue}个,更新文件总数：{e.TotalSize}";
                Console.WriteLine(str);
            }

            if (e.Type == ProgressType.Done)
            {
                Console.WriteLine("更新完成");
            }
        }

        private static void OnDownloadStatistics(object sender, DownloadStatisticsEventArgs e)
        {
            Console.WriteLine($"下载速度：{e.Speed}，剩余时间：{e.Remaining.Minute}:{e.Remaining.Second}");
        }
    }
}
