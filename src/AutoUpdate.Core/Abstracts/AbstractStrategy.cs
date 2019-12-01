using AutoUpdate.Core.Update;
using AutoUpdate.Core.Utils;
using System;
using System.IO;
using System.IO.Compression;

namespace AutoUpdate.Core.Abstracts
{
    public class AbstractStrategy
    {
        public virtual T GetOption<T>(UpdateOption<T> option)
        {
            return default;
        }

        public bool VerifyFileMd5(string fileName, string md5)
        {
            var packetMD5 = FileUtil.GetFileMD5(fileName);

            if (md5.ToUpper().Equals(packetMD5.ToUpper()))
            {
                return true;
            }
            return false;
        }

        public bool UnPacket(string filePath,string tempPath) {
            try
            {
                //Directory.Delete(filePath, true);
                ZipFile.ExtractToDirectory(filePath,tempPath);
                File.Delete(filePath);
                Update32Or64Libs(tempPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Update32Or64Libs(string currentDir)
        {
            var is64XSystem = Environment.Is64BitOperatingSystem;
            var sourceDir = Path.Combine(currentDir, is64XSystem ? "x64" : "x32");
            var destDir = Path.Combine(currentDir, "dlls");

            if (!Directory.Exists(sourceDir)) return;
            FileUtil.DirectoryCopy(sourceDir, destDir, true, true, null);
            Directory.Delete(sourceDir);
        }
    }
}
