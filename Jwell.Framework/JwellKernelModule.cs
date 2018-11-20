using Autofac;
using Castle.Core.Logging;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Modules;

namespace Jwell.Framework
{
    public class JwellKernelModule: JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NullLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<CurrentUnitOfWork>().As<ICurrentUnitOfWork>().SingleInstance();
            builder.RegisterType<UnitOfWorkInterceptor>().AsSelf();
        }
    }
}
