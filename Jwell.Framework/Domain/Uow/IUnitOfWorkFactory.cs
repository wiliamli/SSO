using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Domain.Uow
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(UnitOfWorkOptions options);
    }
}
