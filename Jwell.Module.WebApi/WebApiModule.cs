using Autofac;
using Autofac.Integration.WebApi;
using Jwell.Modules.WebApi.INI;
using System.Web.Http;

namespace Jwell.Modules.WebApi
{
    public class WebApiModule : Framework.Modules.JwellModule
    {

        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            IniConfig.ReadValue("ProjectSign");
        }

        public override void Loaded(IContainer container)
        {
            base.Loaded(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            IniConfig.ReadValue("ProjectSign");
        }
    }
}
