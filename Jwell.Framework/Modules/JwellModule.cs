using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwell.Framework.Extensions;
using Jwell.Framework.Settings;

namespace Jwell.Framework.Modules
{
    public abstract class JwellModule
    {
        protected Assembly ThisAssembly => this.GetType().Assembly;

        protected GlobalSettings Settings => GlobalSettings.Instance;

        /// <summary>
        /// 此方法用于注册服务前修改配置
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 此方法默认调用了RegisterAssemblyByConvention方法, 还可以注册自定义的类型
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyByConvention(ThisAssembly);
        }

        /// <summary>
        /// 此方法可以安全的访问IContainer
        /// </summary>
        /// <param name="container"></param>
        public virtual void Loaded(IContainer container)
        {
            
        }

        #region Static methods

        public static bool IsJwellModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(JwellModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds direct depended modules of a module (excluding given module).
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsJwellModule(moduleType))
            {
                throw new InvalidOperationException("This type is not a module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependOnAttribute), true).Cast<DependOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependOnModules)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(JwellKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsJwellModule(module))
            {
                throw new InvalidOperationException("This type is not a module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }

        #endregion
    }
}
