using Jwell.Modules.Session.Constant;
using StackExchange.Redis;

namespace Jwell.Modules.Session.Redis
{
    /// <summary>
    /// 主要是为后期，由统一配置根据系统分配服务器IP，账户等
    /// 开数据库访问
    /// 目前先由非静态处理，再到后期单例
    /// </summary>
    public class RedisManagement
    {
        public static ConnectionMultiplexer RedisClient { get; private set; }
        #region 静态单例 
        static RedisManagement()
        {
            RedisClient = ConnectionMultiplexer.Connect($"{RedisConstant.IP}:{RedisConstant.PORT},password={RedisConstant.PASSWORD},abortConnect=false");
        }
        #endregion
    }
}
