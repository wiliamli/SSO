using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;
using Autofac;
using System.Reflection;

namespace Jwell.Framework.Ioc.Conventions
{
    public class LifetimeEventsRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type, ContainerBuilder builder)
        {
            if (typeof(ILifetimeEvents).IsAssignableFrom(type))
            {
                registration.OnActivating(e => { ((ILifetimeEvents)e.Instance).OnActivating(); });
                registration.OnActivated(e => { ((ILifetimeEvents)e.Instance).OnActivated(); });
                registration.OnRelease(e =>
                {
                    ((ILifetimeEvents)e).OnRelease();
                    if (e is IDisposable)
                    {
                        ((IDisposable)e).Dispose();
                    }
                });
            }
        }
    }
}
