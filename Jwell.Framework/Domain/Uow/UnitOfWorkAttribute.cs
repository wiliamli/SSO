using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Domain.Uow
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkAttribute : Attribute
    {
        public bool UseTransaction { get; set; }
        
        public bool IsDisabled { get; set; }
        
        public UnitOfWorkAttribute()
        {

        }

        public UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                UseTransaction = UseTransaction
            };
        }
    }
}
