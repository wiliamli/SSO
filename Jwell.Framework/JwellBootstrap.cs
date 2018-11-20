using Autofac;
using Jwell.Framework.Modules;

namespace Jwell.Framework
{
    public class JwellBootstrap<TModule>
        where TModule : JwellModule
    {
        private ContainerBuilder ContainerBuilder { get; set; }

        public JwellBootstrap()
        {
            ContainerBuilder = new ContainerBuilder();
        }

        public void Start()
        {
            //将配置初始化放在最前,确保所有模块都能正确访问
            Settings.GlobalSettings.Initialize();

            JwellModuleManager manager = new JwellModuleManager(typeof(TModule));
            manager.Initialize(this.ContainerBuilder);

            manager.Load(this.ContainerBuilder);

            IContainer rootContainer = ContainerBuilder.Build();

            manager.Loaded(rootContainer);
        }
    }
}
