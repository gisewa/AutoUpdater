# GeneralUpdate #
![](https://img.shields.io/badge/GeneralUpdate.Core-3.2.1-green)
![](https://img.shields.io/badge/GeneralUpdate.Single-1.0.0-green)
![](https://img.shields.io/badge/GeneralUpdate.Zip-1.0.0-green)
![](https://img.shields.io/github/license/WELL-E/AutoUpdater?color=blue)
![](https://img.shields.io/github/stars/WELL-E/AutoUpdater?color=blue)

### GeneralUpdate是基于.net framwork开发的一款（c/s应用）自动升级程序。该组件将更新的核心部分抽离出来方便应用于多种项目当中目前适用于wpf，控制台应用，winfrom。 ###

- 本组件（除Single组件仅支持Framework以外）均支持框架版本 **.NET Framework 4.6.1** | **.NET Core 2.0** | **.NET 5**
- 如果有任何使用问题可以在Github的issues上进行提问，每周会解决并解答bug或者问题。
- 每次迭代新版本**doc文件夹中的帮助文档也会随之更新**，各位开发者请多关注。
- 如果该组件能够帮助到您，希望可以点个**Strat**和关注一下文档末尾的联系方式。您的支持是对开源作者的动力。



## How to use it： ##
Gitee（码云）地址：

- https://gitee.com/Juster-zhu/GeneralUpdate

Nuget地址：

- https://www.nuget.org/packages/GeneralUpdate.Core/
- https://www.nuget.org/packages/GeneralUpdate.Single/
- https://www.nuget.org/packages/GeneralUpdate.Zip/


GitHub地址：
 
- Address:https://github.com/WELL-E/AutoUpdater/tree/autoupdate2
- Issues：https://github.com/WELL-E/AutoUpdater/issues


## Run： ##
![](https://img2020.cnblogs.com/blog/1214710/202005/1214710-20200503171309275-1921529733.png)


## Quick start: ##

    #region Launch1

            args = new string[6] {
                "0.0.0.0",
                "1.1.1.1",
                "https://github.com/WELL-E",
                 "http://192.168.50.225:7000/update.zip",
                 @"E:\PlatformPath",
                "509f0ede227de4a662763a4abe3d8470",
                 };

            GeneralUpdateBootstrap bootstrap = new GeneralUpdateBootstrap();//自动更新引导类
            bootstrap.DownloadStatistics += OnDownloadStatistics;//下载进度通知事件
            bootstrap.ProgressChanged += OnProgressChanged;//更新进度通知事件
            bootstrap.Strategy<DefultStrategy>().//注册策略，可自定义更新流程
                Option(UpdateOption.Format, "zip").//指定更新包的格式，目前只支持zip
                Option(UpdateOption.MainApp, "your application name").//指定更新完成后需要启动的主程序名称不需要加.exe直接写名称即可
                Option(UpdateOption.DownloadTimeOut,60).//下载超时时间（单位：秒）,如果不指定则默认超时时间为30秒。
                RemoteAddress(args).//这里的参数保留了之前的参数数组集合
                Launch();//启动更新

            #endregion

            #region Launch2

            /*
             * Launch2
             * 新增了第二种启动方式
             * 流程：
             * 1.指定更新地址，https://api.com/GeneralUpdate?version=1.0.0.1 在webapi中传入客户端当前版本号
             * 2.如果需要更新api回返回给你所有的更新信息（详情内容参考 /Models/UpdateInfo.cs）
             * 3.拿到更新信息之后则开始http请求更新包
             * 4.下载
             * 5.解压
             * 6.更新本地文件
             * 7.关闭更新程序
             * 8.启动配置好主程序
             * 更新程序必须跟主程序放在同级目录下
             */

            //GeneralUpdateBootstrap bootstrap2 = new GeneralUpdateBootstrap();
            //bootstrap2.DownloadStatistics += OnDownloadStatistics;
            //bootstrap2.ProgressChanged += OnProgressChanged;
            //bootstrap2.Strategy<DefultStrategy>().
            //    Option(UpdateOption.Format, "zip").
            //    Option(UpdateOption.MainApp, "").
			//	  Option(UpdateOption.DownloadTimeOut,60).//下载超时时间（单位：秒）,如果不指定则默认超时时间为30秒。
            //    RemoteAddress(@"https://api.com/GeneralUpdate?version=1.0.0.1").//指定更新地址
            //    Launch();

            #endregion

        private static void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
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

## Author ##
JusterZhu & WELL-E

## Contact us ##

WELL-E

- 1群 130108655 2群 960655709
- Git: https://github.com/WELL-E

juster.chu

- E-Mail：zhuzhen723723@outlook.com
- QQ: 580749909
- Git: https://github.com/JusterZhu
- 微信公众号: DotNet源计划（justerzz）