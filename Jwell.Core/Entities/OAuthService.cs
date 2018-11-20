using Jwell.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Jwell.Domain.Entities
{
    [Table("OAuthServiceManagement")]
    public class OAuthService : BaseEntity
    {

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 客户端的OAuth授权密码
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 访问Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 授权后的回调地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 小组Leader工号
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 范围
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

        public static string Sql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT \"ID\",");
            sql.AppendFormat("\"ServerNumber\",");
            sql.AppendFormat("\"ServerName\",");
            sql.AppendFormat("\"ClientSecret\",");
            sql.AppendFormat("\"AccessToken\",");
            sql.AppendFormat("\"RedirectUri\",");
            sql.AppendFormat("\"TeamLeader\",");
            sql.AppendFormat("\"DemainName\",");
            sql.AppendFormat("\"Scope\",");
            sql.AppendFormat("\"CreatedTime\",");
            sql.AppendFormat("\"CreatedBy\",");
            sql.AppendFormat("\"ModifiedBy\",");
            sql.AppendFormat("\"ModifiedTime\" ");
            sql.AppendFormat(" FROM \"JWELL\".\"OAuthServiceManagement\" ");
            sql.AppendFormat(" WHERE 1=1 ");
            return sql.ToString();
        }
    }
}
