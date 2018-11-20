using Jwell.Framework.Utilities;
using StackExchange.Redis;
using System;

namespace Jwell.Modules.Cache.Redis
{
    public sealed class RedisCache
    {
        private static readonly object objLock = new object();
        private IDatabase Database { get; set; }
        public RedisCache(int db = 0)
        {
            //db默认数量
            if (db >= 0 && db < 16)
            {
                Database = RedisManagement.RedisClient.GetDatabase(db);
            }
            else
            {
                throw new ArgumentOutOfRangeException("db", db, "必须在[0,15]的范围");
            }
        }

        public T Get<T>(string key)
        {
            T t = default(T);
            lock (objLock) // 线程安全，单线程操作
            {
                if (Database.KeyExists(key))
                {
                    t = Serializer.FromJson<T>(Database.StringGet(key));
                }
            }
            return t;
        }

        public bool RemoveCache(string key)
        {
            bool success = false;
            lock (objLock) // 线程安全，单线程操作
            {
                success = Database.KeyDelete(key);
            }
            return success;
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">秒为单位</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, int expireTime)
        {
            bool result = false;
            lock (objLock) // 线程安全，单线程操作
            {
                result = Database.StringSet(key, Serializer.ToJson(value), new TimeSpan(0, 0, expireTime));
            }
            return result;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExist(string key)
        {
            bool isExist = false;
            lock (objLock) // 线程安全，单线程操作
            {
                isExist = Database.KeyExists(key);
            }
            return isExist;
        }
    }
}