using Jwell.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    [Singleton]
    public interface ICacheClient
    {
        /// <summary>
        /// 切换数据库
        /// </summary>
        int DB { get; set; }


        /// <summary>
        /// redis对象
        /// </summary>
        Redis.RedisCache RedisCache { get; }

        /// <summary>
        /// 通过key获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key);
 
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        bool SetCache<T>(string key, T value, int expireTime);


        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        bool RemoveCache(string key);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExist(string key);
    }
}
