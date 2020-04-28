using AutoUpdate.Core.Update;
using AutoUpdate.Core.Utils;
using System;
using System.IO;

namespace AutoUpdate.Core.Strategys
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
    }
}
