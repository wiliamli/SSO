using Jwell.Modules.Session.Constant;
using Jwell.Modules.Session.Model;
using Jwell.Modules.Session.Redis;

namespace Jwell.Modules.Session
{
    /// <summary>
    /// Session 
    /// SSO御用，不公共开放
    /// </summary>
    public class SessionManager: ISessionManager,ISessionChangeDB
    {
        private RedisCache sessionCache = null;

        public bool SetSession(SessionModel session)
        {
            sessionCache = new RedisCache();
            return sessionCache.Add(session.SessionID, session, RedisConstant.DEFAULTEXPIRE);
        }

        public bool SetSession(SessionModel session,int expire)
        {
            sessionCache = new RedisCache();
            return sessionCache.Add(session.SessionID, session, expire);
        }

        /// <summary>
        /// 根据SessionID获取Session
        /// </summary>
        /// <param name="session">session对象中SessionID赋值</param>
        /// <returns></returns>
        public SessionModel GetSession(SessionModel session)
        {
            sessionCache = new RedisCache();
            return sessionCache.Get<SessionModel>(session.SessionID);
        }

        public bool RemoveSession(SessionModel session)
        {
            sessionCache = new RedisCache();
            return sessionCache.RemoveCache(session.SessionID);
        }

        public bool IsExist(SessionModel session)
        {
            sessionCache = new RedisCache();
            return sessionCache.IsExist(session.SessionID);
        }

        #region ISessionChangeDB
        //public bool SetSession(SessionModel session, int db = 1)
        //{
        //    sessionCache = new RedisCache(db);
        //    return sessionCache.SetAdd(session.SessionID, session, RedisConstant.DEFAULTEXPIRE);
        //}

        public bool SetSession(SessionModel session, int expire, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Add(session.SessionID, session, expire);
        }

        public SessionModel GetSession(SessionModel session, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Get<SessionModel>(session.SessionID);
        }

        public bool RemoveSession(SessionModel session, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.RemoveCache(session.SessionID);
        }

        public bool IsExist(SessionModel session, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.IsExist(session.SessionID);
        }

        /// <summary>
        /// 设置其他值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire">过期时间</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Set(string key, string value, int expire, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Add(key, value, expire);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Get<string>(key);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExist(string key, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.IsExist(key);
        }

        /// <summary>
        /// 是否存在该Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Push(string key, string value, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Push(key,value);
        }

        /// <summary>
        ///  push到redis的Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public string Pop(string key, int db = 1)
        {
            sessionCache = new RedisCache(db);
            return sessionCache.Pop(key);
        }
        #endregion
    }
}
