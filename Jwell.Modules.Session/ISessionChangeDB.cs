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
    public interface ISessionChangeDB
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool SetSession(SessionModel session, int db = 1);

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="session"></param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        bool SetSession(SessionModel session, int expire, int db = 1);

        /// <summary>
        /// 设置其他值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire">过期时间</param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Set(string key, string value, int expire, int db = 1);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key, int db = 1);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExist(string key, int db = 1);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        SessionModel GetSession(SessionModel session, int db = 1);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool RemoveSession(SessionModel session, int db = 1);

        /// <summary>
        /// 是否存在该Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool IsExist(SessionModel session, int db = 1);

        /// <summary>
        /// push到redis的Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Push(string key, string value, int db = 1);

        /// <summary>
        /// 从redis中pop数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        string Pop(string key, int db = 1);
    }
}
