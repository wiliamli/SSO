using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Dapper.Core
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 此Id为编号类主键，非自增long类型主键
        /// 可通过Attribute说明，反射实现
        /// </summary>
        public string Id { get; set; }
    }
}
