using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Jwell.Modules.Dapper.Core
{
    public class DapperFactory
    {
        private static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;


#pragma warning disable CS0618 // 类型或成员已过时
        public static OracleConnection CreateOracleConnection
#pragma warning restore CS0618 // 类型或成员已过时
        {
            get
            {
#pragma warning disable CS0618 // 类型或成员已过时
                var connection = new OracleConnection(ConnectionString);
#pragma warning restore CS0618 // 类型或成员已过时
                return connection;
            }
        }
    }
}
