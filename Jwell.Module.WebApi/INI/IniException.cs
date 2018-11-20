using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.WebApi.INI
{
    public class IniException : Exception
    {
        private readonly string message;// 错误消息
        public IniException(string message)
        {
            this.message = message;
        }

        public override string Message => this.message;

        public override string ToString()
        {
            return this.message;
        }
    }
}
