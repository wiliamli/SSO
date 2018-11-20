using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jwell.Framework.Ioc;
using Jwell.Framework.Ioc.Conventions;
using Jwell.Framework.Settings;

namespace Jwell.Framework.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAssemblyByConvention(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                RegisterTypeByConvention(builder, type);
            }
        }

        public static void RegisterTypeByConvention(ContainerBuilder builder, Type type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
            {
                return;
            }

            var ignoreAttribute = type.GetCustomAttribute<IgnoreAttribute>();
            if (ignoreAttribute != null)
            {
                return;
            }

            var registration = builder.RegisterType(type);

            List<IConventionRegister> registers = GlobalSettings.Instance.IocSettings().ConventionRegisters;

            foreach (var register in registers)
            {
                register.Register(registration, type, builder);
            }
        }
    }
}
