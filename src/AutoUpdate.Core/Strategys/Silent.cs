using AutoUpdate.Core.Abstracts;
using AutoUpdate.Core.Events;
using AutoUpdate.Core.Interfaces;
using AutoUpdate.Core.Models;
using AutoUpdate.Core.Utils;
using Microsoft.Win32;
using System;

namespace AutoUpdate.Core.Strategys
{
    public class Silent : AbstractStrategy, IStrategy
    {
        public IUpdateConfiguration Configuration { get; }
        private UpdatePacket _updatePacket;
        private Action<object, UpdateStatusEventArgs> _eventAction;

        public void Create(IFile file, Action<object, UpdateStatusEventArgs> eventAction)
        {
            _updatePacket = (UpdatePacket)file;
            _eventAction = eventAction;
        }

        public void Excute()
        {
            var isVerify = VerifyFileMd5($"{_updatePacket.TempPath}.{_updatePacket.Format}", _updatePacket.MD5);
            if (!isVerify) return;

            if (UnPacket($"{ _updatePacket.TempPath }.{_updatePacket.Format}", _updatePacket.TempPath))
            {
                UpdateFiles();
            }
        }

        public bool UpdateFiles()
        {
            try
            {
                FileUtil.DirectoryCopy(_updatePacket.TempPath, _updatePacket.Path,
            true, true, o => _updatePacket.Name = o);

                FileUtil.UpdateReg(Registry.LocalMachine, FileUtil.SubKey, "DisplayVersion", 
                    _updatePacket.NewVersion);

                _eventAction(this, new UpdateStatusEventArgs() { Status = $"更新完成" , Code = 200 , ProgressValue = 100 });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
