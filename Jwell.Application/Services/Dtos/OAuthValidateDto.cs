using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using System;
using System.Linq;

namespace Jwell.Application.Services.Dtos
{
    public class OAuthValidateDto
    {
        public long ID { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Code过期时间，统一配置指定
        /// </summary>
        public int CodeExpire { get; set; }

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用Token可以拿用户数据
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token令牌
        /// </summary>
        public string AccessToken { get; set;}

        /// <summary>
        /// State值
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 访问范围
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

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
        public string ModifiedBy
        {
            get;set;
        }

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

    public static class OAuthValidateDtoExt
    {
        public static IQueryable<OAuthValidateDto> ToDtos(this IQueryable<OAuthValidate> query)
        {
            return from a in query
                   select new OAuthValidateDto()
                   {
                       ID = a.ID,
                       Code = a.Code,
                       CodeExpire = a.CodeExpire,
                       EmployeeID = a.EmployeeID,
                       CreatedBy = a.CreatedBy,
                       CreatedTime = a.CreatedTime,
                       Password = a.Password,
                       ModifiedBy = a.ModifiedBy,
                       ModifiedTime = a.ModifiedTime,
                       ServiceNumber = a.ServiceNumber,
                       State = a.State,
                       Token = a.Token,
                       Scope = a.Scope,
                       AccessToken = a.AccessToken
                   };
        }

        public static PageResult<OAuthValidateDto> ToDtos(this PageResult<OAuthValidate> query)
        {
            var queryDto = (from a in query.Pager
                            select new OAuthValidateDto()
                            {
                                ID = a.ID,
                                Code = a.Code,
                                CodeExpire = a.CodeExpire,
                                CreatedBy = a.CreatedBy,
                                CreatedTime = a.CreatedTime,
                                EmployeeID = a.EmployeeID,
                                ModifiedBy = a.ModifiedBy,
                                ModifiedTime = a.ModifiedTime,
                                Password = a.Password,
                                ServiceNumber = a.ServiceNumber,
                                State = a.State,
                                Token = a.Token,
                                Scope = a.Scope,
                                AccessToken = a.AccessToken
                            }).ToList();

            return new PageResult<OAuthValidateDto>(queryDto, query.PageIndex, query.PageSize, query.TotalCount);
        }

        public static OAuthValidateDto ToDto(this OAuthValidate entity)
        {
            OAuthValidateDto dto = null;
            if (entity != null)
            {
                dto = new OAuthValidateDto()
                {
                    Code = entity.Code,
                    CodeExpire = entity.CodeExpire,
                    CreatedBy = entity.CreatedBy,
                    CreatedTime = entity.CreatedTime,
                    EmployeeID = entity.EmployeeID,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedTime = entity.ModifiedTime,
                    Password = entity.Password,
                    ServiceNumber = entity.ServiceNumber,
                    State = entity.State,
                    Token = entity.Token,
                    Scope = entity.Scope,
                    AccessToken = entity.AccessToken
                };
            }
            return dto;
        }

        public static OAuthValidate ToEntity(this OAuthValidateDto dto)
        {
            OAuthValidate entity = null;
            if (dto != null)
            {
                entity = new OAuthValidate()
                {
                    ID = dto.ID,
                    Code = dto.Code,
                    CodeExpire = dto.CodeExpire,
                    CreatedBy = dto.CreatedBy,
                    CreatedTime = dto.CreatedTime,
                    EmployeeID = dto.EmployeeID,
                    ModifiedBy = dto.ModifiedBy,
                    ModifiedTime = dto.ModifiedTime,
                    Password = dto.Password,
                    ServiceNumber = dto.ServiceNumber,
                    State = dto.State,
                    Token = dto.Token,
                    Scope = dto.Scope,
                    AccessToken = dto.AccessToken
                };
            }
            return entity;
        }
    }
}
