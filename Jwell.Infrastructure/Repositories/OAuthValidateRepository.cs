using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Domain.Entities;
using Jwell.Modules.EntityFramework.Repositories;
using Jwell.Modules.EntityFramework.Uow;
using Jwell.Repository.Context;

namespace Jwell.Repository.Repositories
{
    public class OAuthValidateRepository : RepositoryBase<OAuthValidate, JwellDbContext, long>, IOAuthValidateRepository
    {
        public OAuthValidateRepository(IDbContextResolver dbContextResolver) : base(dbContextResolver)
        {
        }

        public override int Update(OAuthValidate entity)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE \"JWELL_SSO\".\"OAuthValidate\"");
            sql.Append(" SET \"ModifiedTime\" =:ModifiedTime");
            sql.Append(" WHERE \"Code\" = :Code");

            return base.ExecuteSqlCommand(sql.ToString(), new object[] { entity.ModifiedTime, entity.Code });
        }

        public override int Delete(OAuthValidate entity)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM \"JWELL_SSO\".\"OAuthValidate\"");
            sql.Append(" WHERE ");
            sql.Append(" EXISTS( SELECT \"ServiceNumber\" FROM ");
            sql.Append("\"JWELL_SSO\".\"OAuthValidate\" ");
            sql.Append(" WHERE \"Code\" = :Code )");
            return base.ExecuteSqlCommand(sql.ToString(),new object[] {  entity.Code });
        }

        public override IQueryable<OAuthValidate> Queryable()
        {
            return DbContext.OAuthValidate.AsQueryable();
        }

        public override int ExecuteSqlCommand(string sql)
        {
            return base.ExecuteSqlCommand(sql);
        }


        public override async Task<int> ExecuteSqlCommandAsync(string sql)
        {
            return await base.ExecuteSqlCommandAsync(sql);
        }

        public override int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return base.ExecuteSqlCommand(sql, parameters);
        }

        public override async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await base.ExecuteSqlCommandAsync(sql, parameters);
        }

        public override IEnumerable<TElement> SqlQuery<TElement>(string sql)
        {
            return base.SqlQuery<TElement>(sql);
        }

        public override IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return base.SqlQuery<TElement>(sql, parameters);
        }

        public int Delete(string code)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM \"JWELL_SSO\".\"OAuthValidate\"");
            sql.Append(" WHERE ");
            sql.Append(" \"Code\"=:Code");
            return base.ExecuteSqlCommand(sql.ToString(), new object[] { code });
        }
    }
}
