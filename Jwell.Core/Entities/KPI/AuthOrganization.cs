using Jwell.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domain.Entities
{
    [Table("AUTH_ORGANIZATION")]
    public class AuthOrganization : BaseEntity
    {
        [Column("NAME")]
        public string Name { get; set;}

        [Column("CODE")]
        public string Code { get; set; }

        [Column("REMARK")]
        public string Remark { get; set; }

        public long? PID { get; set; }

        [Column("SEQUENCE")]
        public int? Sequence { get; set; }

        [Column("LAYER")]
        public int? Layer { get; set; }

        [Column("ACCOUNT_ID")]
        public string AccountID { get; set; }

        [Column("CREATE_ID")]
        public long? CreateID { get; set; }

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
