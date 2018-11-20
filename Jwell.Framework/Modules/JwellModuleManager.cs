using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Linq;
using Jwell.Framework.Ioc;

namespace Jwell.Framework.Modules
{
    [Ignore]
    public class JwellModuleManager
    {
        private JwellModuleInfo StartupModule { get; set; }

        private Type StartupModuleType { get; set; }

        private JwellModuleCollection _moduleList;

        private List<JwellModuleInfo> ModuleList { get; set; }

        public JwellModuleManager(Type startupModule)
        {
            StartupModuleType = startupModule;
        }

        public void Initialize(ContainerBuilder builder)
        {
            builder.RegisterInstance(this).AsSelf().SingleInstance();

            _moduleList = new JwellModuleCollection(StartupModuleType);

            LoadAllModules(builder);

            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.Initialize();
            }
        }

        public void Load(ContainerBuilder builder)
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.Load(builder);
            }
        }

        public void Loaded(IContainer container)
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.Loaded(container);
            }
        }

        private void LoadAllModules(ContainerBuilder builder)
        {            
            var moduleTypes = FindAllModuleTypes().Distinct().ToList();
            
            CreateModules(builder, moduleTypes);

            _moduleList.EnsureKernelModuleToBeFirst();
            _moduleList.EnsureStartupModuleToBeLast();

            SetDependencies();
        }

        private List<Type> FindAllModuleTypes()
        {
            var modules = JwellModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_moduleList.StartupModuleType);

            return modules;
        }

        private void CreateModules(ContainerBuilder builder, ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = Activator.CreateInstance(moduleType) as JwellModule;
                if (moduleObject == null)
                {
                    throw new InvalidOperationException("This type is not a module: " + moduleType.AssemblyQualifiedName);
                }

                var moduleInfo = new JwellModuleInfo(moduleType);
                moduleInfo.Instance = moduleObject;

                _moduleList.Add(moduleInfo);

                if (moduleType == _moduleList.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                builder.RegisterInstance(moduleObject).AsSelf().SingleInstance();
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _moduleList)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in JwellModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _moduleList.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new InvalidOperationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
