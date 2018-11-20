using Jwell.Modules.Logger.Log.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jwell.Modules.Logger.Log
{
    public class Source
    {
        private Channel channel;

       

        public Source(Channel channel)
        {
            this.channel = channel;
            MonitorLog();
        }

        internal void Log(Priority priority, Marker marker, string message, string stackTrace
            , string filter1, string filter2, string traceId)
        {
          
            Marker curMarker = marker ?? Marker.Empty;

            channel.Add(new LogModel
            {
                Environment = SetupConfig.SetupConfig.Enviroment,//组件获取
                Category = curMarker.Category,
                SubCategory = curMarker.SubCategory,
                LogType = (byte)priority,
                IP = SetupConfig.SetupConfig.IP,
                Message = (message ?? ""),
                Filter1 = (filter1 ?? ""),
                Filter2 = (filter2 ?? ""),
                DomainName = SetupConfig.SetupConfig.Domain,
                ServiceNumber = SetupConfig.SetupConfig.ServiceNumber,
                ServiceSign = SetupConfig.SetupConfig.ServiceSign,
                StackTrace = stackTrace,
                TraceID = traceId
            });
        }


        private void MonitorLog()
        {
            System.Threading.Tasks.Task.Run(()=> {
                while (true)
                {
                    Marker marker = new Marker("client", "perf");
                    channel.Add(new MonitorLog());
                    //2分钟
                    Thread.Sleep(120000);
                }
            });
        }
    }
}
