// This software is part of the Autofac IoC container
// Copyright © 2013 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Internal;
using Jwell.Framework.Aspect;
using Autofac;

#if NET45
using System.Runtime.Remoting;
#endif

namespace Jwell.Framework.Extensions
{
    /// <summary>
    /// Adds registration syntax to the <see cref="ContainerBuilder"/> type.
    /// </summary>
    internal static class RegistrationExtensions
    {
        private const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";

        private const string AttributeInterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName";

        private static readonly IEnumerable<Service> EmptyServices = Enumerable.Empty<Service>();

        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration)
        {
            return EnableClassInterceptors(registration, ProxyGenerationOptions.Default);
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfaces">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options,
            params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            if (!(registration.ActivatorData is ConcreteReflectionActivatorData))
            {
                return registration;
            }

            var activatorData = registration.ActivatorData as ConcreteReflectionActivatorData;

            activatorData.ImplementationType =
                ProxyGenerator.ProxyBuilder.CreateClassProxyType(
                    activatorData.ImplementationType,
                    additionalInterfaces ?? new Type[0],
                    options);

            var interceptorServices = GetInterceptorServicesFromAttributes(activatorData.ImplementationType);
            AddInterceptorServicesToMetadata(registration, interceptorServices, AttributeInterceptorsPropertyName);

            registration.OnPreparing(e =>
            {
                var proxyParameters = new List<Parameter>();
                int index = 0;

                if (options.HasMixins)
                {
                    foreach (var mixin in options.MixinData.Mixins)
                    {
                        proxyParameters.Add(new PositionalParameter(index++, mixin));
                    }
                }

                proxyParameters.Add(new PositionalParameter(index++, GetInterceptorServices(e.Component, activatorData.ImplementationType)
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray()));

                if (options.Selector != null)
                {
                    proxyParameters.Add(new PositionalParameter(index, options.Selector));
                }

                e.Parameters = proxyParameters.Concat(e.Parameters).ToArray();
            });

            return registration;
        }

        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> EnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration)
        {
            return EnableInterfaceInterceptors(registration, null);
        }

        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> EnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, ProxyGenerationOptions options)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.RegistrationData.ActivatingHandlers.Add((sender, e) =>
            {
                EnsureInterfaceInterceptionApplies(e.Component);

                var proxiedInterfaces = e.Instance
                    .GetType()
                    .GetInterfaces()
                    .Where(i =>
                    {
                        var ti = i.GetTypeInfo();
#pragma warning disable CS0612 // 类型或成员已过时
                        return ti.IsVisible || ti.Assembly.IsInternalToDynamicProxy();
#pragma warning restore CS0612 // 类型或成员已过时
                    })
                    .ToArray();

                if (!proxiedInterfaces.Any())
                {
                    return;
                }

                var theInterface = proxiedInterfaces.First();
                var interfaces = proxiedInterfaces.Skip(1).ToArray();

                var interceptors = GetInterceptorServices(e.Component, e.Instance.GetType())
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray();

                e.Instance = options == null
                    ? ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, interceptors)
                    : ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, options, interceptors);
            });

            return registration;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServices">The interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Service[] interceptorServices)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (interceptorServices == null || interceptorServices.Any(s => s == null))
            {
                throw new ArgumentNullException(nameof(interceptorServices));
            }

            AddInterceptorServicesToMetadata(builder, interceptorServices, InterceptorsPropertyName);

            return builder;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceNames">The names of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params string[] interceptorServiceNames)
        {
            if (interceptorServiceNames == null || interceptorServiceNames.Any(n => n == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceNames));
            }

            return InterceptedBy(builder, interceptorServiceNames.Select(n => new KeyedService(n, typeof(IInterceptor))).ToArray());
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceTypes">The types of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Type[] interceptorServiceTypes)
        {
            if (interceptorServiceTypes == null || interceptorServiceTypes.Any(t => t == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceTypes));
            }

            return InterceptedBy(builder, interceptorServiceTypes.Select(t => new TypedService(t)).ToArray());
        }

#if NET45
        /// <summary>
        /// Intercepts the interface of a transparent proxy (such as WCF channel factory based clients).
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="additionalInterfacesToProxy">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> InterceptTransparentProxy<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, params Type[] additionalInterfacesToProxy)
        {
            return InterceptTransparentProxy(registration, null, additionalInterfacesToProxy);
        }

        /// <summary>
        /// Intercepts the interface of a transparent proxy (such as WCF channel factory based clients).
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfacesToProxy">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> InterceptTransparentProxy<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, ProxyGenerationOptions options, params Type[] additionalInterfacesToProxy)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.RegistrationData.ActivatingHandlers.Add((sender, e) =>
            {
                EnsureInterfaceInterceptionApplies(e.Component);

                if (!RemotingServices.IsTransparentProxy(e.Instance))
                {
                    throw new DependencyResolutionException(string.Format(
                    CultureInfo.CurrentCulture, RegistrationExtensionsResources.TypeIsNotTransparentProxy, e.Instance.GetType().FullName));
                }
                var instanceType = e.Instance.GetType();
                var instanceTypeInfo = instanceType.GetTypeInfo();
                if (!instanceTypeInfo.IsInterface)
                {
                    throw new DependencyResolutionException(string.Format(
                    CultureInfo.CurrentCulture, RegistrationExtensionsResources.TransparentProxyIsNotInterface, e.Instance.GetType().FullName));
                }

                if (additionalInterfacesToProxy.Any())
                {
                    var remotingTypeInfo = (IRemotingTypeInfo)RemotingServices.GetRealProxy(e.Instance);

                    var invalidInterfaces = additionalInterfacesToProxy
                    .Where(i => !remotingTypeInfo.CanCastTo(i, e.Instance))
                    .ToArray();

                    if (invalidInterfaces.Any())
                    {
                        string message = string.Format(
                            CultureInfo.CurrentCulture,
                            RegistrationExtensionsResources.InterfaceNotSupportedByTransparentProxy,
                            string.Join(", ", invalidInterfaces.Select(i => i.FullName)));
                        throw new DependencyResolutionException(message);
                    }
                }

                var interceptors = GetInterceptorServices(e.Component, instanceType)
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray();

                e.Instance = options == null
                ? ProxyGenerator.CreateInterfaceProxyWithTargetInterface(instanceType, additionalInterfacesToProxy, e.Instance, interceptors)
                : ProxyGenerator.CreateInterfaceProxyWithTargetInterface(instanceType, additionalInterfacesToProxy, e.Instance, options, interceptors);
            });

            return registration;
        }
#endif

        private static void EnsureInterfaceInterceptionApplies(IComponentRegistration componentRegistration)
        {
            if (componentRegistration.Services
                .OfType<IServiceWithType>()
                .Select(s => s.ServiceType.GetTypeInfo())
#pragma warning disable CS0612 // 类型或成员已过时
                .Any(s => !s.IsInterface || (!s.Assembly.IsInternalToDynamicProxy() && !s.IsVisible)))
#pragma warning restore CS0612 // 类型或成员已过时
            {
                //throw new InvalidOperationException(
                //    string.Format(
                //        CultureInfo.CurrentCulture,
                //        "The component {0} cannot use interface interception as it provides services that are not publicly visible interfaces. Check your registration of the component to ensure you're not enabling interception and registering it as an internal/private interface type.",
                //        componentRegistration));
            }
        }

        private static void AddInterceptorServicesToMetadata<TLimit, TActivatorData, TStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            IEnumerable<Service> interceptorServices,
            string metadataKey)
        {
            object existing;
            if (builder.RegistrationData.Metadata.TryGetValue(metadataKey, out existing))
            {
                builder.RegistrationData.Metadata[metadataKey] =
                    ((IEnumerable<Service>)existing).Concat(interceptorServices).Distinct();
            }
            else
            {
                builder.RegistrationData.Metadata.Add(metadataKey, interceptorServices);
            }
        }

        private static IEnumerable<Service> GetInterceptorServices(IComponentRegistration registration, Type implType)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            if (implType == null)
            {
                throw new ArgumentNullException(nameof(implType));
            }

            var result = EmptyServices;

            object services;
            if (registration.Metadata.TryGetValue(InterceptorsPropertyName, out services))
            {
                result = result.Concat((IEnumerable<Service>)services);
            }

            return result;
        }

        private static IEnumerable<Service> GetInterceptorServicesFromAttributes(Type implType)
        {
            var implTypeInfo = implType.GetTypeInfo();
            if (!implTypeInfo.IsClass) return Enumerable.Empty<Service>();

            var classAttributeServices = implTypeInfo
                .GetCustomAttributes(typeof(InterceptAttribute), true)
                .Cast<InterceptAttribute>()
                .SelectMany(att => att.InterceptBy.Select(t => new TypedService(t)));

            var interfaceAttributeServices = implType
                .GetInterfaces()
                .SelectMany(i => i.GetTypeInfo().GetCustomAttributes(typeof(InterceptAttribute), true))
                .Cast<InterceptAttribute>()
                .SelectMany(att => att.InterceptBy.Select(t => new TypedService(t)));

            return classAttributeServices.Concat(interfaceAttributeServices);
        }
    }
}