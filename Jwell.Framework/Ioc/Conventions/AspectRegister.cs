using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using System.Reflection;
using Jwell.Framework.Aspect;
using Jwell.Framework.Extensions;
using Autofac.Core;
using Castle.DynamicProxy;

namespace Jwell.Framework.Ioc.Conventions
{
    public class AspectRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type, ContainerBuilder builder)
        {
            var classAttributes = type.GetCustomAttributes<InterceptAttribute>();
            var interfaceAttributes = type.GetInterfaceAttributes<InterceptAttribute>();

            var allAttributes = classAttributes.Union(interfaceAttributes);

            if (classAttributes.Any())
            {
                registration.EnableClassInterceptors();
            }
            else if (interfaceAttributes.Any())
            {
                registration.EnableInterfaceInterceptors();
            }

            if (!allAttributes.Any())
            {
                return;
            }

            IEnumerable<Type> interceptBy = allAttributes.SelectMany(t => t.InterceptBy);
            if (interceptBy.Any())
            {
                registration.InterceptedBy(interceptBy.ToArray());
            }
        }
    }        
}
