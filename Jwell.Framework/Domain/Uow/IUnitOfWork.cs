using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Framework.Domain.Repositories;

namespace Jwell.Framework.Domain.Uow
{
    public interface IUnitOfWork
    {
        string Id { get; }

        void Commit();

        Task CommitAsync();

        void Rollback();

        void Dispose();

        event EventHandler Disposed;
    }
}
