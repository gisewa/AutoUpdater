using AutoUpdate.Core.Interfaces;
using AutoUpdate.Core.Update;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Configs
{
    public class OnilneConfig : IUpdateConfiguration
    {
        public string Format { get; set; }

        public T GetOption<T>(UpdateOption<T> option)
        {
            Contract.Requires(option != null);

            if (UpdateOption.Format.Equals(option))
            {
                return (T)(object)this.Format;
            }
            return default;
        }

        public bool SetOption(UpdateOption option, object value)
        {
            throw new NotImplementedException();
        }

        public bool SetOption<T>(UpdateOption<T> option, T value)
        {
            if (UpdateOption.Format.Equals(option))
            {
                this.Format = (string)(object)value;
                return true;
            }
            return false;
        }
    }
}
