using Jwell.Framework.Ioc;
using Jwell.Modules.Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Session
{
    [Singleton]
    public interface ISessionManager
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool SetSession(SessionModel session);

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="session"></param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        bool SetSession(SessionModel session, int expire);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        SessionModel GetSession(SessionModel session);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool RemoveSession(SessionModel session);

        /// <summary>
        /// 是否存在该Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool IsExist(SessionModel session);
    }
}
