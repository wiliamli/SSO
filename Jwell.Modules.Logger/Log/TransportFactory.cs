using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log
{
    public static class TransportFactory
    {
        public static Source Source;

        public static Channel Channel;

        static TransportFactory()
        {
            Channel = new Channel();
            Source = new Source(Channel);
        }
    }
}
