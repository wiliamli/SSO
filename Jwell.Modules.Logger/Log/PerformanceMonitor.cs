using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
    /// <summary>
    /// 执行监控(队列)
    /// </summary>
    public class PerformanceMonitor
    { 
        private long counter;

        private readonly string title = string.Empty;

        public long Counter => counter;

        public PerformanceMonitor(string title)
        {
            PerformanceMonitorFactory.Add(title, this);
            this.title = title;
        }

        public void Increment()
        {
            Interlocked.Increment(ref counter);
        }

        public void Increment(long n)
        {
            Interlocked.Add(ref counter, n);
        }

        public void Decrement()
        {
            Interlocked.Decrement(ref counter);
        }

        public void Decrement(long n)
        {
            Interlocked.Add(ref counter, -1 * n);
        }
    }
}
