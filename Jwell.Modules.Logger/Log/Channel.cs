using Jwell.Modules.Logger.Log.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
     public class Channel
    {
        private ConcurrentQueue<LogBase> queue = new ConcurrentQueue<LogBase>();

        private volatile int maxSize = 100000;

        private PerformanceMonitor queueSizeCounter = new PerformanceMonitor("queueSizeCounter");

        private PerformanceMonitor enqueueCounter = new PerformanceMonitor("enqueueCounter");

        private PerformanceMonitor dequeueCounter = new PerformanceMonitor("dequeueCounter");

        private PerformanceMonitor overflowCounter = new PerformanceMonitor("overflowCounter");

        internal long QueueSizeCounter => queueSizeCounter.Counter;

        internal long EnqueueCounter => enqueueCounter.Counter;

        internal long DequeueCounter => dequeueCounter.Counter;

        internal long OverflowCounter => overflowCounter.Counter;

        internal int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                if (value > 0 && value <= 200000)
                {
                    maxSize = value;
                }
            }
        }

        internal void Add(LogBase log)
        {
            if (queue.Count < maxSize)
            {
                queue.Enqueue(log);
                queueSizeCounter.Increment();
                enqueueCounter.Increment();
            }
            else
            {
                overflowCounter.Increment();
            }
        }

        internal LogBase Take()
        {
            LogBase result = null;
            if (queue.TryDequeue(out result))
            {
                queueSizeCounter.Decrement();
                dequeueCounter.Increment();
                return result;
            }
            return null;
        }
    }
}
