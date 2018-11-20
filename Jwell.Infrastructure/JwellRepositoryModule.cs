using Autofac;
using Jwell.Domain;
using Jwell.Framework.Modules;
using Jwell.Modules.EntityFramework;

namespace Jwell.Repository
{
    [DependOn(typeof(EntityFrameworkModule))]
    public class JwellRepositoryModule: JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
