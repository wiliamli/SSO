using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 定义SSO登录的参数名称
    /// 首字母消息为了和get参数对应
    /// </summary>
    public enum QueryKeyMenu
    {
        /// <summary>
        /// 客户端授权码
        /// </summary>
        clientSecret,

        /// <summary>
        /// 客户端返回Url
        /// </summary>
        returnUrl,

        /// <summary>
        /// 访问token
        /// </summary>
        accessToken,

        /// <summary>
        /// 请求类型
        /// </summary>
        responseType,

        /// <summary>
        /// 防止CQRS攻击
        /// </summary>
        state,

        /// <summary>
        /// 验证过了后，302转跳到客户端验证地址
        /// </summary>
        redirctUrl,

        /// <summary>
        /// 访问授权范围
        /// </summary>
        scope

    }
}