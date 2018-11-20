using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace Jwell.Modules.MVC
{
    public class MvcModule:Framework.Modules.JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }

        public override void Loaded(IContainer container)
        {
            base.Loaded(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
