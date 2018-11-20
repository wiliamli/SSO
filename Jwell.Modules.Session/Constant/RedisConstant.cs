using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Session.Constant
{
    /// <summary>
    /// Redis配置常量
    /// </summary>
    internal static class RedisConstant
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        internal static string CLIENTNAME => "SESSION";

        /// <summary>
        /// redis密码
        /// </summary>
        internal static string PASSWORD => System.Configuration.ConfigurationManager.AppSettings["sessionPwd"];

        /// <summary>
        /// redis的IP地址
        /// </summary>
        internal static string IP => System.Configuration.ConfigurationManager.AppSettings["sessionIP"];

        /// <summary>
        /// 默认端口
        /// </summary>
        internal static int PORT => 6379;

        /// <summary>
        /// timespan基数，设置到秒
        /// </summary>
        internal static long TICKCARDINAL = 10000000;

        /// <summary>
        /// 默认过期时间(S)
        /// </summary>
        internal static int DEFAULTEXPIRE = 20 * 60;
    }
}
