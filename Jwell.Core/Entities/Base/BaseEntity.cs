using Jwell.Framework.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domain.Entities.Base
{
    public class BaseEntity:Entity<long>
    {
        /// <summary>
        /// 主键自增Id
        /// </summary>
        [Key]
        public override long ID
        {
            get
            {
                return base.ID;
            }

            set
            {
                base.ID = value;
            }
        }
    }
}
