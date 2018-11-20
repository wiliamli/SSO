using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Logging
{
    public interface ILogger
    {
        void Debug(string message, string filter1, string filter2);

        void Error(string message, Exception ex, string filter1, string filter2);

        void Fatal(string message, string filter1, string filter2);

        void Info(string message, string filter1, string filter2);

        void Trace(string message, string filter1, string filter2);

        void Warn(string message, string filter1, string filter2);
    }
}
