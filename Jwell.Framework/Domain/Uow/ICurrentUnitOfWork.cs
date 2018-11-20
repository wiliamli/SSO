using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Domain.Uow
{
    /// <summary>
    /// 表示当前线程启用的UnitOfWork
    /// </summary>
    public interface ICurrentUnitOfWork
    {
        /// <summary>
        /// 获取或设置当前线程的UnitOfWork
        /// 不能重复设置
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        IUnitOfWork Current { get; set; }
    }
}
