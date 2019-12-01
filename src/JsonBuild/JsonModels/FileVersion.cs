using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBuild.JsonModels
{
    public class RequestCommand {

    }

    public class ResponseCommand {
        //Port string 服务端端口号
        //IP string 服务器地址
        //Supplement string 服务补充地址
        //PacketName String  更新包文件名
        //DonwLoadPath    string 本地下载路径
        //InstallPath string 本地更新（安装）路径
        //Version string 版本号
        //VersionGuid string 版本唯一id
    }

    public class Packet {

        /// <summary>
        /// 文件版本
        /// </summary>
        public string PacketVersion { get; set; }

        /// <summary>
        /// 更新包文件名
        /// </summary>
        public string PacketName { get; set; }

        /// <summary>
        /// 文件加密校验码
        /// </summary>
        public string PacketMD5 { get; set; }


        /// <summary>
        /// 下载目录
        /// </summary>
        public string DonwloadPath { get; set; }

        /// <summary>
        /// 安装目录
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 备份地址
        /// </summary>
        public string BackupPath { get; set; }

        public List<FileVersion> Files { get; set; }
    }

    public class FileVersion
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatingTime { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件加密校验码
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// 0无更新（none）、1新增文件（new）、2版本更新(update)、3移除文件(remove)
        /// </summary>
        public int Status { get; set; }
    }
}
