using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Constant
{
    public static class DbContextContant
    {
        public static string KPISCHEMA => System.Configuration.ConfigurationManager.AppSettings["kpi"];
    }
}
