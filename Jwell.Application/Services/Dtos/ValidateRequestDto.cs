using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    public class ValidateRequestDto
    {
        /// <summary>
        /// 返回url
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 客户端授权码
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 用于为客户端存储Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 返回客户端验证
        /// </summary>
        public string RedirctUrl { get; set; }

        /// <summary>
        /// CQRF
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 访问授权范围
        /// </summary>
        public string Scope { get; set; }
    }
}
