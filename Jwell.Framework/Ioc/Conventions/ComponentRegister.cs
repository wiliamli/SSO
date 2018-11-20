using Autofac;
using Autofac.Builder;
using System;
using Jwell.Framework.Extensions;

namespace Jwell.Framework.Ioc.Conventions
{
    public class ComponentRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type, ContainerBuilder builder)
        {
            ComponentAttribute attribute = type.GetFirstAttribute<ComponentAttribute>(true);

            if (attribute == null)
            {
                return;
            }

            if (attribute.AsSelf)
            {
                registration.As(type);
            }
            if (attribute.AsImplementedInterfaces)
            {
                var interfaceTypes = type.GetInterfaces();
                registration.As(interfaceTypes);
            }
            if (attribute.PropertyAutoWired)
            {
                registration.PropertiesAutowired();
            }
            if (attribute.AutoActivate)
            {
                registration.AutoActivate();
            }

            if (attribute.Lifetime == ServiceLifetime.Scoped)
            {
                registration.InstancePerLifetimeScope();
            }
            else if (attribute.Lifetime == ServiceLifetime.Singleton)
            {
                registration.SingleInstance();
            }
            else
            {
                registration.InstancePerDependency();
            }
        }
    }
}
