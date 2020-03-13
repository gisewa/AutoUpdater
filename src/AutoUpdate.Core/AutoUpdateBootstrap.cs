using AutoUpdate.Core.Abstracts;
using AutoUpdate.Core.Interfaces;
using AutoUpdate.Core.Models;
using AutoUpdate.Core.Utils;
using System;

namespace AutoUpdate.Core
{
    public sealed class AutoUpdateBootstrap : AbstractBootstrap<AutoUpdateBootstrap, IStrategy>
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string PacketName { get; set; }
        public string DownloadPath { get; set; }
        public string InstallPath { get; set; }
        public string MD5 { get; set; }
        public string Version { get; set; }
        public string LocalVersion { get; set; }

        public AutoUpdateBootstrap() : base() {
            
        }

        public AutoUpdateBootstrap(AbstractBootstrap<AutoUpdateBootstrap, IStrategy> bootstrap) : base(bootstrap)
        {
            this.Packet = bootstrap.Packet;
        }

        public AutoUpdateBootstrap RemoteAddress(string configjsonPath)
        {
            var json = FileUtil.ConfigurationBulider(configjsonPath);
            var host = json["Host"].ToString();
            var post = Convert.ToInt32(json["Port"]);
            var packetName = json["PacketName"].ToString();
            var downloadPath = json["DownloadPath"].ToString();
            var installPath = json["InstallPath"].ToString();
            var md5 = json["MD5"].ToString();
            var version = json["Version"].ToString();

            this.Host = host;
            this.Port = post;
            this.PacketName = packetName;
            this.DownloadPath = downloadPath;
            this.InstallPath = installPath;
            this.MD5 = md5;
            this.Version = version;
            this.ValidateRemoteAddress();
            InitPacket();
            return this;
        }

        public AutoUpdateBootstrap RemoteAddress(
            string host, int port, string version,
            string packetName, string downloadPath,
            string installPath,string md5)
        {
            this.Host = host;
            this.Port = port;
            this.PacketName = $"{packetName}";
            this.DownloadPath = downloadPath;
            this.InstallPath = installPath;
            this.MD5 = md5;
            this.Version = version;
            this.ValidateRemoteAddress();
            InitPacket();
            return this;
        }

        private void InitPacket() {
            Packet = new UpdatePacket();
            Packet.Url = $"http://{Host}:{Port}/{PacketName}.zip";
            Packet.TempPath = $"{FileUtil.GetTempDirectory()}\\{PacketName}";
            Packet.Path = InstallPath;
            Packet.Name = PacketName;
            Packet.MD5 = MD5;
            base.Packet = this.Packet;
        }

        public AutoUpdateBootstrap ValidateRemoteAddress()
        {
            if (string.IsNullOrWhiteSpace(Host))
            {
                throw new NullReferenceException("host not set");
            }

            if (Port == 0)
            {
                throw new NullReferenceException("port not set");
            }

            if (string.IsNullOrWhiteSpace(PacketName))
            {
                throw new NullReferenceException("packet name not set");
            }

            if (string.IsNullOrWhiteSpace(DownloadPath))
            {
                throw new NullReferenceException("download path not set");
            }

            if (string.IsNullOrWhiteSpace(InstallPath))
            {
                throw new NullReferenceException("install path not set");
            }

            if (string.IsNullOrWhiteSpace(MD5))
            {
                throw new NullReferenceException("install path not set");
            }

            return this;
        }
    }
}
