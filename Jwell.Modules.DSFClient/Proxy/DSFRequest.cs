using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.DSFClient.Proxy
{
    public abstract class DSFParam<T> where T : new()
    {
        /// <summary>
        /// 参数
        /// </summary>
        public abstract T Parameter { get; set; }
    }
}
