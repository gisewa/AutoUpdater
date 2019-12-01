using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Core.Models
{
    /// <summary>
    /// 更新说明
    /// </summary>
    public class Explain
    {
        public int Index { get; set; }

        public string Content { get; set; }

        public ExplainType Type { get; set; }
    }
}
