using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using System.Reflection;
using Jwell.Framework.Extensions;
using Jwell.Framework.Utilities;

namespace Jwell.Framework.Domain.Uow
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public UnitOfWorkInterceptor(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo method;
            try
            {
                method = invocation.MethodInvocationTarget;
            }
            catch
            {
                method = invocation.GetConcreteMethod();
            }

            UnitOfWorkAttribute attribute = GetUnitOfWorkAttributeOrNull(method);            
            
            if (attribute == null || attribute.IsDisabled)
            {
                //No need to a uow
                invocation.Proceed();
                return;
            }

            //No current uow, run a new one
            PerformUow(invocation, attribute.CreateOptions());
        }

        private void PerformUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncUow(invocation, options);
            }
            else
            {
                PerformSyncUow(invocation, options);
            }
        }

        private void PerformSyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            try
            {
                var uow = UnitOfWorkFactory.Create(options);
                invocation.Proceed();
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PerformAsyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            var uow = UnitOfWorkFactory.Create(options);

            try
            {
                invocation.Proceed();
            }
            catch
            {
                uow.Dispose();
                throw;
            }

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await uow.CommitAsync(),
                    exception => uow.Dispose()
                );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await uow.CommitAsync(),
                    exception => uow.Dispose()
                );
            }
        }

        private UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            UnitOfWorkAttribute attr = methodInfo.DeclaringType.GetFirstAttribute<UnitOfWorkAttribute>();

            return attr;
        }
    }
}
