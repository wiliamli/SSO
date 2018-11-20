using System;
using System.Collections.Generic;
using System.Text;
using Jwell.Framework.Ioc.Conventions;

namespace Jwell.Framework.Ioc
{
    public class IocSettings
    {
        public List<IConventionRegister> ConventionRegisters { get; private set; }

        public IocSettings()
        {
            ConventionRegisters = new List<IConventionRegister>();            
        }

        internal static IocSettings Default
        {
            get
            {
                var settings = new IocSettings();

                settings.ConventionRegisters.Add(new ComponentRegister());
                settings.ConventionRegisters.Add(new LifetimeEventsRegister());
                settings.ConventionRegisters.Add(new AspectRegister());

                return settings;
            }
        }
    }
}
