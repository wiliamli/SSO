using System.Linq;
using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Framework.Paging;
using Jwell.Framework.Application.Service;
using Jwell.Repository.Repositories;
using System.Collections.Generic;

namespace Jwell.Application
{
    public class OAuthServersService : ApplicationService, IOAuthServersService
    {
        private IOAuthServiceRepository Repository { get; set; }

        public OAuthServersService(IOAuthServiceRepository repository)
        {
            Repository = repository;
        }

        public PageResult<OAuthServiceDto> GetOAuthServersDtos(PageParam page)
        {
            return Repository.Queryable().ToPageResult(page).ToDtos();
        }

        public bool Save(OAuthServiceDto dto)
        {
            bool isExist = IsExist(dto.ServiceNumber, dto.ClientSecret);
            bool result = false;
            if (!isExist)
            {
                //TODO:调用服务管理的接口，来获取服务名称
                //dto.ServiceSign = "ServiceName";
                result = Repository.Add(dto.ToEntity()) > 0;
            }
            else
            {
                if (dto.Id > 0)
                    result = Repository.Update(dto.ToEntity()) > 0;
                else
                    throw new System.Exception("Id不能为0");
            }
            return result;
        }

        public OAuthServiceDto GetOAuthServiceDtoByServerNum(string serviceNumbers)
        {
            var entity = Repository.Queryable().FirstOrDefault(m => m.ServiceNumber == serviceNumbers);

            return entity.ToDto();
        }

        public bool IsExist(string serviceNumber, string clientSecret)
        {
            int? result = Repository.SqlQuery<int>($"SELECT \"COUNT\"(1) FROM \"JWELL_SSO\".\"OAuthServiceManagement\" WHERE \"ServiceNumber\" = :serviceNumber AND \"ClientSecret\"=:clientSecret ",
                new object[] { serviceNumber, clientSecret }).FirstOrDefault();
            return result != null ? (result.Value > 0) : false;
        }

        public OAuthServiceDto GetOAuthServiceDtoByClientSecret(string clientSecret)
        {
            OAuthServiceDto dto = null;
            var entity = Repository.Queryable().FirstOrDefault(m => m.ClientSecret == clientSecret);
            if (entity != null) dto = entity.ToDto();

            return dto;
        }

        public IEnumerable<OAuthServiceDto> GetOAuthServicesByScope(string scope)
        {
            return Repository.Queryable().Where(m => m.Scope == scope).ToDtos().ToList();
        }
    }
}
