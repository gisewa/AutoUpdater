using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Models
{

    /// <summary>
    /// 同步类型
    /// </summary>
    public enum SyncType 
    {
        All = 0,
        Some = 1
    }

    /// <summary>
    /// 更新类型
    /// </summary>
    public enum UpdateType 
    {
        Update = 0,
        Nothing = 1,
        Compel = 3
    }

    public enum Strategy {
        Online = 1,
        Silent = 2
    }

    /// <summary>
    /// 更新操作
    /// </summary>
    //public enum UpdateOption 
    //{
    //    Try = 0,
    //    Net = 1,
    //    Exit = 2
    //}

    /// <summary>
    /// 更新说明类型
    /// </summary>
    public enum ExplainType 
    {
        Fix = 0,
        New = 1,
        Delete = -1
    }
}
