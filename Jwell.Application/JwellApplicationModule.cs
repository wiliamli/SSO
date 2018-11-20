using Autofac;
using Jwell.Framework.Modules;
using Jwell.Repository;

namespace Jwell.Application
{
    [DependOn(typeof(JwellRepositoryModule))]
    public class JwellApplicationModule: JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }

        //public override void Loaded(IContainer container)
        //{
        //    base.Loaded(container);

        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}
    }
}
