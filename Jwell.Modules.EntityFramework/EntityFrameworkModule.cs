using Autofac;
using Jwell.Framework.Ioc;
using Jwell.Framework.Settings;
using Jwell.Modules.EntityFramework.Ioc;

namespace Jwell.Modules.EntityFramework
{
    [Ignore]
    public class EntityFrameworkModule:Framework.Modules.JwellModule
    {
        public override void Initialize()
        {
            base.Initialize();

            GlobalSettings.Instance.IocSettings().ConventionRegisters.Add(new EFConventionRegister());
        }
    }
}
