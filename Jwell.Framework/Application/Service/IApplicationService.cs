using Jwell.Framework.Aspect;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Ioc;

namespace Jwell.Framework.Application.Service
{
    [Transient]
    [Intercept(typeof(UnitOfWorkInterceptor))]
    [UnitOfWork]
    public interface IApplicationService
    {
    }
}
