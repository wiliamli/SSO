using Jwell.Modules.Logger.Log;
using Jwell.Modules.Logger.Log.Model;
using System;

namespace Jwell.Modules.Logger
{
    public static class JwellLogger
    {
        private static LogContext logContext = new LogContext();

        #region DEBUG
        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug(string message)
        {
            logContext.Debug(message);
        }

        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        public static void Debug(Marker marker, string traceID, string message)
        {
            logContext.Debug(marker, traceID, message);
        }

        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Debug(string traceID, string message, string filter1, string filter2)
        {
            logContext.Debug(traceID, message, filter1, filter2);
        }

        /// <summary>
        /// 记录Debug消息
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Debug(Marker marker, string traceID, string message, string filter1, string filter2)
        {
            logContext.Debug(marker, traceID, message, filter1, filter2);
        }
        #endregion

        #region ERROR
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message)
        {
            logContext.Error(message);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        public static void Error(string traceID, string message)
        {
            logContext.Error(traceID, message);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string traceID, string message, Exception ex)
        {
            logContext.Error(traceID, message, ex);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        public static void Error(Marker marker, string traceID, string message, string stackTrace)
        {
            logContext.Error(marker, traceID, message, stackTrace);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(Marker marker, string traceID, string message, Exception ex)
        {
            logContext.Error(marker, traceID, message, ex);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Error(Marker marker, string traceID, string message, Exception ex, string filter1, string filter2)
        {
            logContext.Error(marker, traceID, message, ex, filter1, filter2);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Error(Marker marker, string traceID, string message, string stackTrace, string filter1, string filter2)
        {
            logContext.Error(marker, traceID, message, stackTrace, filter1, filter2);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Error(string traceID, string message, Exception ex, string filter1, string filter2)
        {
            logContext.Error(traceID, message, ex, filter1, filter2);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Fatal(string message)
        {
            logContext.Fatal(message);
        }

        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        public static void Fatal(string traceID, string message, string stackTrace)
        {
            logContext.Fatal(traceID, message, stackTrace);
        }

        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        public static void Fatal(Marker marker, string traceID, string message, string stackTrace)
        {
            logContext.Fatal(marker, traceID, message, stackTrace);
        }

        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Fatal(string traceID, string message, string stackTrace, string filter1, string filter2)
        {
            logContext.Fatal(message, stackTrace, traceID, filter1, filter2);
        }

        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Fatal(string traceID, string message, Exception ex, string filter1, string filter2)
        {
            logContext.Fatal(message, ex, traceID, filter1, filter2);
        }

        /// <summary>
        /// 记录致命日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="stackTrace">堆栈跟踪</param>
        /// <param name="filter1"></param>
        /// <param name="filter2"></param>
        public static void Fatal(Marker marker, string traceID, string message, string stackTrace, string filter1, string filter2)
        {
            logContext.Fatal(marker, traceID, message, stackTrace, filter1, filter2);
        }
        #endregion

        #region INFO
        /// <summary>
        /// 保存消息日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message)
        {
            logContext.Info(message);
        }

        /// <summary>
        /// 保存消息日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        public static void Info(Marker marker, string traceID, string message)
        {
            logContext.Info(marker, traceID, message);
        }

        /// <summary>
        /// 保存消息日志
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Info(string traceID, string message, string filter1, string filter2)
        {
            logContext.Info(traceID, message, filter1, filter2);
        }

        /// <summary>
        /// 保存消息日志
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Info(Marker marker, string traceID, string message, string filter1, string filter2)
        {
            logContext.Info(marker, traceID, message, filter1, filter2);
        }
        #endregion

        #region WARN
        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Warn(string message)
        {
            logContext.Warn(message);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        public static void Warn(string traceID, string message)
        {
            logContext.Warn(traceID, message);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        public static void Warn(Marker marker, string traceID, string message)
        {
            logContext.Warn(marker, traceID, message);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Warn(string traceID, string message, string filter1, string filter2)
        {
            logContext.Warn(traceID, message, filter1, filter2);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="marker">自定义类名和方法名(或其他标记信息)</param>
        /// <param name="traceID">跟踪ID</param>
        /// <param name="message">消息</param>
        /// <param name="filter1">过滤条件1</param>
        /// <param name="filter2">过滤条件2</param>
        public static void Warn(Marker marker, string traceID, string message, string filter1, string filter2)
        {
            logContext.Warn(marker, traceID, message, filter1, filter2);
        }
        #endregion
    }
}