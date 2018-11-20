using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;

namespace Jwell.Repository.Repositories
{
    public class AuthOrganizationRepository : RepositoryBase<AuthOrganization, JwellKPIDbContext, long>, IAuthOrganizationRepository
    {
        public AuthOrganizationRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {

        }
    }
}
