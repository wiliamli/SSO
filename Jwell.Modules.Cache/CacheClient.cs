using System;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    public class CacheClient : ICacheClient
    {
        /// <summary>
        /// redis数据库,[0-15之间的整数]
        /// </summary>
        private int db = 0;
        public int DB
        {
            get
            {
                return db;
            }
            set
            {
                if (value >= 0 && value < 16)
                    db = value;
                else
                    throw new ArgumentOutOfRangeException("db", db, "必须在[0,15]的范围");
            }
        }

        public Redis.RedisCache RedisCache
        {
            get
            {
                return new Redis.RedisCache(DB);
            }
        }

        public T GetCache<T>(string key)
        {
           return RedisCache.Get<T>(key);
        }

        public bool RemoveCache(string key)
        {
            return RedisCache.RemoveCache(key);
        }

        public bool SetCache<T>(string key, T value, int expireTime)
        {
            bool result = RedisCache.Set(key, value, expireTime);
            return result;
        }

        public bool IsExist(string key)
        {
            return RedisCache.IsExist(key);
        }
    }
}
