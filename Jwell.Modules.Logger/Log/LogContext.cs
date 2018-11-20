using Jwell.Framework.Utilities;
using Jwell.Modules.Logger.Log.Constant;
using Jwell.Modules.Logger.Log.Model;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
    internal class LogContext
    {
        static LogContext()
        {
            Consume();
        }

        #region
        internal void Debug(string message)
        {
            Debug(LogConstant.DEFAULTVALUE, message);
        }

        internal void Debug(string traceID,string message)
        {
            Debug(Marker.Empty,traceID, message);
        }

        internal void Debug(Marker marker, string traceID, string message)
        {
            Debug(marker, traceID, message, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Debug(string traceID, string message, string filter1, string filter2)
        {
            Debug(Marker.Empty,traceID ,message, filter1, filter2);
        }

        internal void Debug(Marker marker,string traceID ,string message, string filter1, string filter2)
        {
            Log(Priority.DEBUG, marker,traceID,message,LogConstant.DEFAULTVALUE ,filter1, filter2);
        }

        internal void Error(string message)
        {
            Error(Marker.Empty, LogConstant.DEFAULTVALUE, message, LogConstant.DEFAULTVALUE);
        }

        internal void Error(string traceID,string message)
        {
            Error(Marker.Empty,traceID, message,LogConstant.DEFAULTVALUE);
        }

        internal void Error(string traceID, string message, Exception ex)
        {
            Error(Marker.Empty, traceID, message, ex);
        }

        internal void Error(Marker marker, string traceID, string message,string stackTrace)
        {
            Error(marker, traceID, message,stackTrace, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Error(Marker marker, string traceID,string message, Exception ex)
        {
            Error(marker,traceID ,message, ex, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Error(string message,string stackTrace, string filter1, string filter2)
        {
            Error(Marker.Empty,LogConstant.DEFAULTVALUE ,message, stackTrace, filter1, filter2);
        }

        internal void Error(Marker marker,string traceID ,string message, Exception ex, string filter1, string filter2)
        {
            Error(marker,traceID ,message, ex, filter1, filter2);
        }

        internal void Error(string traceID,string message,Exception ex,string filter1, string filter2)
        {
            Log(Priority.ERROR, Marker.Empty, traceID ,message,ErrorMessage(message, ex), filter1, filter2);
        }

        internal void Error(Marker marker, string traceID, string message, string stackTrace, string filter1, string filter2)
        {
            Log(Priority.ERROR, marker, traceID, message, stackTrace, filter1, filter2);
        }

        private string ErrorMessage(string message, Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder(message);
            while (ex != null)
            {
                stringBuilder.AppendFormat("\r\n{0}:{1}\r\n{2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
                ex = ex.InnerException;
            }
            return stringBuilder.ToString();
        }

        internal void Fatal(string message)
        {
            Fatal(message, LogConstant.DEFAULTVALUE);
        }
        internal void Fatal(string message, Exception ex)
        {
            Fatal(message, ErrorMessage(message, ex));
        }

        internal void Fatal(string message,string stackTrace)
        {
            Fatal(Marker.Empty,LogConstant.DEFAULTVALUE, message, stackTrace);
        }

        internal void Fatal(string traceID, string message, Exception ex)
        {
            Fatal(Marker.Empty, traceID, message, ErrorMessage(message,ex));
        }

        internal void Fatal(string traceID, string message,string stackTrace)
        {
            Fatal(Marker.Empty, traceID, message, stackTrace);
        }

        internal void Fatal(Marker marker,string traceID ,string message,string stackTrace)
        {
            Fatal(marker,message, stackTrace, traceID ,LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        public void Fatal(string message, string stackTrace ,string traceID ,string filter1, string filter2)
        {
            Fatal(Marker.Empty, traceID, message, stackTrace ,filter1, filter2);
        }

        public void Fatal(string message, Exception ex, string traceID, string filter1, string filter2)
        {
            Fatal(Marker.Empty, traceID, message, ErrorMessage(message,ex), filter1, filter2);
        }

        internal void Fatal(Marker marker, string traceID, string message,string stackTrace ,string filter1, string filter2)
        {
            Log(Priority.FATAL, marker, traceID, message, stackTrace,filter1, filter2);
        }

        internal void Info(string message)
        {
            Info(LogConstant.DEFAULTVALUE, message);
        }

        internal void Info(string traceID,string message)
        {
            Info(Marker.Empty, traceID, message);
        }

        internal void Info(Marker marker, string traceID,string message)
        {
            Info(marker,traceID ,message, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Info(string traceID, string message, string filter1, string filter2)
        {
            Info(Marker.Empty, traceID, message, filter1, filter2);
        }

        internal void Info(Marker marker, string traceID, string message, string filter1, string filter2)
        {
            Log(Priority.INFO, marker, traceID, message, LogConstant.DEFAULTVALUE, filter1, filter2);
        }

        internal void Log(Priority priority, Marker marker,string traceID ,string message, string stackTrace, string filter1, string filter2)
        {
            Log(priority, marker, traceID, message, stackTrace, filter1, filter2,string.Empty);
        }

        internal static void Log(Priority priority, Marker marker, string traceID, string message, string stackTrace, string filter1, string filter2, string contextID = null)
        {
            TransportFactory.Source.Log(priority, marker, message, stackTrace, filter1, filter2, traceID);
        }

        internal void Trace(string message)
        {
            Trace(LogConstant.DEFAULTVALUE, message);
        }

        internal void Trace(string traceID,string message)
        {
            Trace(Marker.Empty, traceID, message);
        }

        internal void Trace(Marker marker,string traceID ,string message)
        {
            Trace(marker, traceID, message, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Trace(string traceID, string message, string filter1, string filter2)
        {
            Trace(Marker.Empty, traceID, message, filter1, filter2);
        }

        internal void Trace(Marker marker,string traceID ,string message, string filter1, string filter2)
        {
            Log(Priority.TRACE, marker,traceID, message, LogConstant.DEFAULTVALUE, filter1, filter2);
        }

        internal void Warn(string message)
        {
            Warn(LogConstant.DEFAULTVALUE, message);
        }

        internal void Warn(string traceID,string message)
        {
            Warn(Marker.Empty, traceID,message);
        }

        internal void Warn(Marker marker,string traceID ,string message)
        {
            Warn(marker,traceID ,message, LogConstant.DEFAULTVALUE, LogConstant.DEFAULTVALUE);
        }

        internal void Warn(string traceID,string message, string filter1, string filter2)
        {
            Warn(Marker.Empty,traceID, message, filter1, filter2);
        }

        internal void Warn(Marker marker, string traceID, string message, string filter1, string filter2)
        {
            Log(Priority.WARN, marker, traceID, message,LogConstant.DEFAULTVALUE ,filter1, filter2);
        }
        #endregion

        internal static void Consume()
        {
            Task.Run(() => Handle());
        }

        private static void Handle()
        {
            //TODO:后期用kafka
            while (true)
            {
                try
                {
                    if (TransportFactory.Channel.QueueSizeCounter > 0)
                    {
                        var task = TransportFactory.Channel.Take();
                        if (task is LogModel)
                        {
                            LogModel log = task as LogModel;
                            HttpHelper.HttpPost(RemoteConfig.AgentUrl(SetupConfig.SetupConfig.Enviroment, false),
                                Serializer.ToJson(log), LogConstant.TOKENHEADER);
                            //入库
                        }
                        if (task is MonitorLog)
                        {
                            MonitorLog log = task as MonitorLog;
                            HttpHelper.HttpPost(RemoteConfig.AgentUrl(SetupConfig.SetupConfig.Enviroment, true),
                                Serializer.ToJson(log), LogConstant.TOKENHEADER);
                        }
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    //TODO:日志
                    throw ex;
                }
            }
        }
    }
}
