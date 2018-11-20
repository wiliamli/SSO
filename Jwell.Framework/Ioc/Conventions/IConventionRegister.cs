using Autofac;
using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jwell.Framework.Ioc.Conventions
{
    public interface IConventionRegister
    {
        void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type, ContainerBuilder builder);
    }
}
