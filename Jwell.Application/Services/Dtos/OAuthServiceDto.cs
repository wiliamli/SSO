using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Jwell.Application.Services.Dtos
{
    public class OAuthServiceDto
    {
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        private string serviceSign = "ServiceName";
        public string ServiceSign
        {
            get
            {
                return serviceSign;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    serviceSign = value;
                }
            }
        }

        /// <summary>
        /// 客户端的OAuth授权密码
        /// </summary>
        private string clientSecret = Guid.NewGuid().ToString("N");
        [JsonIgnore]
        public string ClientSecret
        {
            get
            {
                return clientSecret;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    clientSecret = value;
                }
            }
        }

        /// <summary>
        /// 访问Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 授权后的回调地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 小组Leader工号
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; }

        public string Scope => "bussinessInfo";

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime createTime = DateTime.Now;
        public DateTime CreatedTime
        {
            get
            {

                return createTime;
            }
            set
            {
                if (value != DateTime.Parse("0001-01-01"))
                {
                    createTime = value;
                }
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime modifiedTime = DateTime.Now;
        public DateTime ModifiedTime
        {
            get
            {
                return modifiedTime;
            }
            set
            {
                if (value != DateTime.Parse("0001-01-01"))
                {
                    modifiedTime = value;
                }
            }
        }
    }

    public static class OAuthServersDtoExt
    {
        public static IQueryable<OAuthServiceDto> ToDtos(this IQueryable<OAuthService> query)
        {
            return from a in query
                   select new OAuthServiceDto()
                   {
                       Id = a.ID,
                       ServiceNumber = a.ServiceNumber,
                       ClientSecret = a.ClientSecret,
                       AccessToken = a.AccessToken,
                       CreatedBy = a.CreatedBy,
                       CreatedTime = a.CreatedTime,
                       DomainName = a.DomainName,
                       ModifiedBy = a.ModifiedBy,
                       ModifiedTime = a.ModifiedTime,
                       RedirectUri = a.RedirectUri,
                       TeamLeader = a.TeamLeader,
                       ServiceSign = a.ServiceName
                   };
        }

        public static PageResult<OAuthServiceDto> ToDtos(this PageResult<OAuthService> query)
        {
            var queryDto = (from a in query.Pager
                            select new OAuthServiceDto()
                            {
                                Id = a.ID,
                                ClientSecret = a.ClientSecret,
                                AccessToken = a.AccessToken,
                                CreatedBy = a.CreatedBy,
                                CreatedTime = a.CreatedTime,
                                DomainName = a.DomainName,
                                ModifiedBy = a.ModifiedBy,
                                ModifiedTime = a.ModifiedTime,
                                RedirectUri = a.RedirectUri,
                                ServiceNumber = a.ServiceNumber,
                                TeamLeader = a.TeamLeader,
                                ServiceSign = a.ServiceName
                            }).ToList();

            return new PageResult<OAuthServiceDto>(queryDto, query.PageIndex, query.PageSize, query.TotalCount);
        }

        public static OAuthServiceDto ToDto(this OAuthService entity)
        {
            OAuthServiceDto dto = null;
            if (entity != null)
            {
                dto = new OAuthServiceDto()
                {
                    ClientSecret = entity.ClientSecret,
                    AccessToken = entity.AccessToken,
                    CreatedBy = entity.CreatedBy,
                    CreatedTime = entity.CreatedTime,
                    DomainName = entity.DomainName,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedTime = entity.ModifiedTime,
                    RedirectUri = entity.RedirectUri,
                    ServiceNumber = entity.ServiceNumber,
                    ServiceSign=entity.ServiceName,
                    TeamLeader = entity.TeamLeader
                };
            }
            return dto;
        }

        public static OAuthService ToEntity(this OAuthServiceDto dto)
        {
            OAuthService entity = null;
            if (dto != null)
            {
                entity = new OAuthService()
                {
                    ID = dto.Id,
                    ClientSecret = dto.ClientSecret,
                    AccessToken = dto.AccessToken,
                    CreatedBy = dto.CreatedBy,
                    CreatedTime = dto.CreatedTime,
                    DomainName = dto.DomainName,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedTime = dto.ModifiedTime,
                    RedirectUri = dto.RedirectUri,
                    ServiceNumber = dto.ServiceNumber,
                    ServiceName = dto.ServiceSign,
                    TeamLeader = dto.TeamLeader,
                    Scope = dto.Scope
                };
            }
            return entity;
        }
    }
}
