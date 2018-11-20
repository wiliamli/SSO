using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Ioc;

namespace Jwell.Modules.EntityFramework.Uow
{
    [Singleton]
    public class EFDbContextResolver : IDbContextResolver
    {
        private ICurrentUnitOfWork _currentUnitOfWork;

        public EFDbContextResolver(ICurrentUnitOfWork currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }

        public T ResolveDbContext<T>() where T : DbContext
        {
            IUnitOfWork uow = _currentUnitOfWork.Current;

            if (uow == null)
            {
                throw new InvalidOperationException("Please call the method in a UnitOfWork scope");
            }
            if (!(uow is EFUnitOfWork))
            {
                throw new InvalidOperationException("The current UnitOfWork is not an instance of EFUnitOfWork");
            }

            return ((EFUnitOfWork)uow).GetOrCreateContext<T>();
        }
    }
}
