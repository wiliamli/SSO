using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
    }
}