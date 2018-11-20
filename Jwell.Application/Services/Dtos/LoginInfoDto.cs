using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfoDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        ///  唯一码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 员工工号(HR系统统一工号)
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 密码（OA统一密码）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string IdentifyingCode{get;set;}

        /// <summary>
        /// 访问Token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
