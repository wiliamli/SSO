using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;

namespace Jwell.Repository.Repositories
{
    public interface IAuthUserRepository: IRepository<AuthUser, long>
    {
    }
}
