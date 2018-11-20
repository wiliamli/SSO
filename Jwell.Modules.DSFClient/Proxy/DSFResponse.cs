using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.DSFClient.Proxy
{
    public class ErrorInfo
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 堆栈跟踪信息
        /// </summary>
        public string StackTrace { get; set; }
    }

    /// <summary>
    /// 响应消息
    /// </summary>
    public class DSFResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ErrorInfo Error { get; set; }
    }

    public class DSFResponse<T> : DSFResponse
    {
        public T Data { get; set; }
    }
}
