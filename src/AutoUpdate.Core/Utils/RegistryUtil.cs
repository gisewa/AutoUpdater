/*
 * Copyright (c) well-e.  All rights reserved.
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Utils
{
    public class RegistryUtil : IDisposable
    {
        #region Private Members
        private readonly RegistryKey _baseRegistryKey;
        private readonly string _subKey; //SOFTWARE\...
        #endregion

        #region Constructors
        #endregion

        #region Public Properties

        public RegistryUtil(RegistryKey baseKey, string subKey)
        {
            _baseRegistryKey = baseKey;
            _subKey = subKey;
        }
        #endregion

        #region Public Methods

        public string Read(string keyName)
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.OpenSubKey(_subKey))
            {
                return sk == null ? null : sk.GetValue(keyName.ToUpper()).ToString();
            }
        }


        public void Write(string keyName, object value)
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.CreateSubKey(_subKey))
            {
                if (sk != null)
                {
                    sk.SetValue(keyName.ToUpper(), value);
                }
            }
        }


        public void DeleteKey(string keyName)
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.CreateSubKey(_subKey))
            {
                if (sk != null)
                {
                    sk.DeleteValue(keyName);
                }
            }
        }

        public void DeleteSubKeyTree()
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.OpenSubKey(_subKey))
            {
                if (sk != null)
                {
                    sk.DeleteSubKeyTree(_subKey);
                }
            }
        }


        public int SubKeyCount()
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.OpenSubKey(_subKey))
            {
                return sk != null ? sk.SubKeyCount : 0;
            }
        }

        public int ValueCount()
        {
            var rk = _baseRegistryKey;
            using (var sk = rk.OpenSubKey(_subKey))
            {
                return sk != null ? sk.ValueCount : 0;
            }
        }

        public void Dispose()
        {
            if (_baseRegistryKey != null)
            {
                _baseRegistryKey.Close();
                //_baseRegistryKey.Dispose();
            }
        }
        #endregion

    }
}
