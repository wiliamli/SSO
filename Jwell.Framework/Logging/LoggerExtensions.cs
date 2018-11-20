using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Logging
{
    /// <summary>
    /// 日志接口的扩展方法
    /// </summary>
    public static class LoggerExtensions
    {
        #region debug

        public static void Debug(this ILogger logger, string message)
        {
            logger.Debug(message, string.Empty, string.Empty);
        }

        public static void Debug(this ILogger logger, string message, string filter1, string filter2)
        {
            logger.Debug(message, filter1, filter2);
        }

        #endregion

        #region error

        public static void Error(this ILogger logger, string message)
        {
            logger.Error(message, null, string.Empty, string.Empty);
        }

        public static void Error(this ILogger logger, string message, Exception ex)
        {
            logger.Error(message, ex, string.Empty, string.Empty);
        }

        public static void Error(this ILogger logger, string message, Exception ex, string filter1, string filter2)
        {
            logger.Error(message, ex, filter1, filter2);
        }

        #endregion

        #region info

        public static void Info(this ILogger logger, string message)
        {
            logger.Info(message, null, null);
        }

        #endregion

        #region trace

        public static void Trace(this ILogger logger, string message)
        {
            logger.Trace(message, null, null);
        }

        #endregion

        #region warn

        public static void Warn(this ILogger logger, string message)
        {
            logger.Warn(message,null, null);
        }

        #endregion
    }
}
