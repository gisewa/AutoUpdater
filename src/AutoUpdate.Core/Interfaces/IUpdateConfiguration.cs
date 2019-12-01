
namespace AutoUpdate.Core.Interfaces
{
    using AutoUpdate.Core.Update;
    using System;

    public interface IUpdateConfiguration
    {
        T GetOption<T>(UpdateOption<T> option);

        bool SetOption(UpdateOption option, object value);

        bool SetOption<T>(UpdateOption<T> option, T value);
    }
}
