using Jwell.Application;
using Jwell.Framework.Modules;
using Jwell.Modules.WebApi;
using Jwell.Modules.MVC;
using Jwell.Repository;
using Jwell.Modules.Session;
using Jwell.Modules.Cache;

namespace Jwell.SSO
{
    /// <summary>
    /// 模块加载
    /// </summary>
    [DependOn(typeof(MvcModule),
        typeof(WebApiModule),
        typeof(JwellApplicationModule),
        typeof(JwellRepositoryModule),
        typeof(JwellSessionModule),
        typeof(JwellCacheModule))]
    public class JwellWebModule : JwellModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}