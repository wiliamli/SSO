using Jwell.Domain.Entities;
using Jwell.Framework.Domain.Repositories;

namespace Jwell.Repository.Repositories
{
    /// <summary>
    /// OAuth授权
    /// </summary>
    public interface IOAuthValidateRepository : IRepository<OAuthValidate, long>
    {
        /// <summary>
        ///  删除当前系统已登录验证信息
        /// </summary>
        /// <param name="code">code码</param>
        /// <returns></returns>
        int Delete(string code);

    }
}
