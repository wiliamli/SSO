using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Ioc
{
    /// <summary>
    /// 服务生命周期
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// 临时服务
        /// </summary>
        Transient = 0,

        /// <summary>
        /// 托管的生命周期
        /// </summary>
        Scoped = 1,

        /// <summary>
        /// 单例服务
        /// </summary>
        Singleton = 2
    }
}
