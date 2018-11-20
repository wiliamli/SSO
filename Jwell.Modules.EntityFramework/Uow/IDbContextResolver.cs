using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.EntityFramework.Uow
{
    public interface IDbContextResolver
    {
        T ResolveDbContext<T>() where T : DbContext;
    }
}
