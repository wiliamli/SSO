using System.Linq;
using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Constant;
using Jwell.Repository.Context;
using System.Collections.Generic;

namespace Jwell.Repository.Repositories
{
    public class AuthUserRepository : RepositoryBase<AuthUser, JwellKPIDbContext, long>, IAuthUserRepository
    {
        public AuthUserRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
          
        }
    }
}
