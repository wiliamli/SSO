using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log.Model
{
    /// <summary>
    /// 级别由低到高
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// 跟踪信息
        /// </summary>
        TRACE = 1,

        /// <summary>
        /// 调试信息
        /// </summary>
        DEBUG = 2,

        /// <summary>
        /// 一般信息
        /// </summary>
        INFO = 3,

        /// <summary>
        /// 警告
        /// </summary>
        WARN = 4,

        /// <summary>
        /// 错误
        /// </summary>
        ERROR = 5,

        /// <summary>
        /// 调试
        /// </summary>
        FATAL = 6
    }
}
