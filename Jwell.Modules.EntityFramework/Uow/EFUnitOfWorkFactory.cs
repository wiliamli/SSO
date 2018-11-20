using Autofac;
using System;
using System.Collections.Concurrent;
using System.Linq;
using Jwell.Framework.Ioc;
using System.Reflection;
using Jwell.Framework.Domain.Uow;

namespace Jwell.Modules.EntityFramework.Uow
{
    [Singleton]
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private ILifetimeScope _lifetime;

        private ICurrentUnitOfWork _currentUnitOfWork;

        public EFUnitOfWorkFactory(ILifetimeScope lifetime, ICurrentUnitOfWork currentUnitOfWork)
        {
            _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            _currentUnitOfWork = currentUnitOfWork;
        }

        public IUnitOfWork Create(UnitOfWorkOptions options)
        {
            if (_currentUnitOfWork.Current != null)
            {
                return _currentUnitOfWork.Current;
            }

            TypedParameter parameter = new TypedParameter(typeof(UnitOfWorkOptions), options);

            IUnitOfWork uow = _lifetime.Resolve<EFUnitOfWork>(parameter);

            //这里捕捉重复开始UnitOfWork的异常, 如果报错就释放UnitOfWork
            try
            {
                _currentUnitOfWork.Current = uow;
            }
            catch
            {
                uow.Dispose();
                throw;
            }

            uow.Disposed += Uow_Disposed;
            return uow;
        }

        private void Uow_Disposed(object sender, EventArgs e)
        {
            _currentUnitOfWork.Current = null;
        }
    }
}
