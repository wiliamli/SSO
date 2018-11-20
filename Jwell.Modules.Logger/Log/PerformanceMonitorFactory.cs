using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
    public class PerformanceMonitorFactory
    {
        private static IDictionary<string, PerformanceMonitor> monitors = new Dictionary<string, PerformanceMonitor>();

        private static object dicLock = new object();

        public static void Add(string title, PerformanceMonitor monitor)
        {
            lock (dicLock)
            {
                if (monitors.ContainsKey(title))
                {
                    monitors[title] = monitor;
                }
                else
                {
                    monitors.Add(title, monitor);
                }
            }
        }

        public static IDictionary<string, PerformanceMonitor> Get()
        {
            lock (dicLock)
            {
                return new Dictionary<string, PerformanceMonitor>(monitors);
            }
        }
    }
}
