using Jwell.Modules.Logger.Log.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
    public static class RemoteConfig
    {
        public static string AgentUrl(string evn,bool isMonitor)
        {

            string domain = LogConstant.DOMAIN;

            string environment = evn.ToLower();

            if (environment == "gray" || environment == "product")
            {
                //TODO:域名要换,域名根据环境变化
                domain = "domain";
            }
            if (isMonitor)
            {
                return $"http://{domain}{LogConstant.PREFIXROUTE}/SaveMonitor";
            }
            else
            {
                return $"http://{domain}{LogConstant.PREFIXROUTE}/SaveLog";
            }
        }
    }
}
