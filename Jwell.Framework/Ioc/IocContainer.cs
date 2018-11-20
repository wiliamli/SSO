using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Ioc
{
    public sealed class IocContainer
    {
        /// <summary>
        /// IoC容器实例
        /// </summary>
        public static IContainer Container { get; internal set; }
    }
}
