using Jwell.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domain.Entities
{
    [Table("OAuthValidate")]
    public class OAuthValidate : BaseEntity
    {
        /// <summary>
        /// 生成码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Code的过期时间
        /// </summary>
        public int CodeExpire { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用Token可以拿用户数据
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// State值
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 访问范围
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
    }
}
