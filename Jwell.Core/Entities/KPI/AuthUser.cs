using Jwell.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Domain.Entities
{
    [Table("AUTH_USER")]
    public class AuthUser: BaseEntity
    {
        [Column("USER_NAME")]
        public string UserName { get; set; }

        [Column("USER_CODE")]
        public string EmployeeID { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("AGE")]
        public string Age { get; set; }

        [Column("SEX")]
        public string Sex { get; set; }

        [Column("PROFILE")]
        public string Profile { get; set; }

        [Column("PHONE_NUM")]
        public string PhoneNum { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("ORG_ID")]
        public long? OrgID { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        [Column("ACCOUNT_ID")]
        public int? AccountID { get; set; }

        [Column("THEME_ID")]
        public int? ThemeID { get; set; }

        [Column("CREATE_ID")]
        public int? CreateID { get; set; }

        [Column("CREATE_NAME")]
        public string CreateName { get; set; }

        [Column("CREATE_TIME")]
        public string CreateTime { get; set; }

        [Column("UPDATE_ID")]
        public long? UpdateID { get; set; }

        [Column("UPDATE_NAME")]
        public string UpdateName { get; set; }

        [Column("UPDATE_TIME")]
        public string UpdateTime { get; set; }
    }
}
