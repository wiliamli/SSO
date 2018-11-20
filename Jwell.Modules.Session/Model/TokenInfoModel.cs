using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Session.Model
{
    public class TokenInfoModel
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
