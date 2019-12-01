using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Interfaces
{
    /// <summary>
    ///     A singleton which is safe to compare via the <c>==</c> operator. Created and managed by
    ///     <see cref="ConstantPool" />.
    /// </summary>
    public interface IConstant
    {
        /// <summary>Returns the unique number assigned to this <see cref="IConstant" />.</summary>
        int Id { get; }

        /// <summary>Returns the name of this <see cref="IConstant" />.</summary>
        string Name { get; }
    }
}
