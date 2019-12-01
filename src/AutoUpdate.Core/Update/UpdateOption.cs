using DotNetty.Common.Utilities;
using AutoUpdate.Core.Abstracts;
using AutoUpdate.Core.Interfaces;
using System.Diagnostics.Contracts;

namespace AutoUpdate.Core.Update
{
    public abstract class UpdateOption : AbstractConstant<UpdateOption>
    {
        class UpdateOptionPool : ConstantPool
        {
            protected override IConstant NewConstant<T>(int id, string name) => new UpdateOption<T>(id, name);
        }

        static readonly UpdateOptionPool Pool = new UpdateOptionPool();

        public static UpdateOption<T> ValueOf<T>(string name) => (UpdateOption<T>)Pool.ValueOf<T>(name);


        public static readonly UpdateOption<int> Try = ValueOf<int>("TRY");

        public static readonly UpdateOption<int> Net = ValueOf<int>("NET");

        public static readonly UpdateOption<int> Exit = ValueOf<int>("EXIT");

        public static readonly UpdateOption<string> Format = ValueOf<string>("FORMAT");

        internal UpdateOption(int id, string name)
          : base(id, name)
        {
        }

        public abstract bool Set(IUpdateConfiguration configuration, object value);

    }


    public sealed class UpdateOption<T> : UpdateOption
    {
        internal UpdateOption(int id, string name)
            : base(id, name)
        {
        }

        public void Validate(T value) => Contract.Requires(value != null);

        public override bool Set(IUpdateConfiguration configuration, object value) => configuration.SetOption(this, (T)value);
    }
}
