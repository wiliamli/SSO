using Autofac;
using Autofac.Builder;
using Entities =Jwell.Framework.Domain.Entities;
using Jwell.Framework.Ioc;
using Jwell.Framework.Ioc.Conventions;
using Jwell.Modules.EntityFramework.Uow;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Jwell.Framework.Domain.Repositories;
using Jwell.Modules.EntityFramework.Repositories;

namespace Jwell.Modules.EntityFramework.Ioc
{
    [Ignore]
    public class EFConventionRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type, ContainerBuilder builder)
        {
            //如果是DbContext, 则注册entity
            if (typeof(EFUnitOfWork).IsAssignableFrom(type))
            {
                var entityProperties = type.GetProperties();

                foreach (var entityProperty in entityProperties)
                {
                    Type entityType;
                    if (IsEntityProperty(entityProperty, out entityType))
                    {
                        Type repositoryInterfaceType = typeof(IRepository<,>).MakeGenericType(entityType);
                        Type repositoryImplType = typeof(RepositoryBase<,,>).MakeGenericType(entityType, type); //第一个是实体,第二个是DbContext

                        builder.RegisterType(repositoryImplType).AsSelf().As(repositoryInterfaceType).AsImplementedInterfaces().PreserveExistingDefaults(); //一定要调用PreserveExistingDefaults(),避免覆盖真正的Repository
                    }
                }
            }
        }

        private bool IsEntityProperty(PropertyInfo property, out Type entityType)
        {
            entityType = null;

            if (!property.PropertyType.IsGenericType)
            {
                return false;
            }

            if (typeof(DbSet<>) == property.PropertyType.GetGenericTypeDefinition())
            {
                Type genericArgument = property.PropertyType.GenericTypeArguments.FirstOrDefault();

                if (genericArgument == null)
                {
                    return false;
                }

                if (typeof(Entities.Entity<object>).IsAssignableFrom(genericArgument))
                {
                    entityType = genericArgument;
                    return true;
                }
            }
            return false;
        }
    }
}
