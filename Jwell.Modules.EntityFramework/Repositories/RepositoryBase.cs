using Jwell.Modules.EntityFramework.Uow;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jwell.Framework.Domain.Repositories;
using Entities = Jwell.Framework.Domain.Entities;

namespace Jwell.Modules.EntityFramework.Repositories
{
    public class RepositoryBase<TEntity, TDbContext, TPrimaryKey> : IRepository<TEntity,TPrimaryKey>
        where TEntity : Entities.Entity<TPrimaryKey>
        where TDbContext : DbContext
    {
        protected TDbContext DbContext
        {
            get
            {
                return _dbContextResolver.ResolveDbContext<TDbContext>();
            }
        }

        protected DbSet<TEntity> Set
        {
            get
            {
                return DbContext.Set<TEntity>();
            }
        }

        private IDbContextResolver _dbContextResolver;

        public RepositoryBase(IDbContextResolver dbContextResolver)
        {
            _dbContextResolver = dbContextResolver ?? throw new ArgumentNullException(nameof(dbContextResolver));
        }

        public virtual IQueryable<TEntity> Queryable()            
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public virtual int Add(TEntity entity)
        {
            Set.Add(entity);
            return 1;
        }

        public virtual int Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return 1;
        }

        public virtual int Delete(TEntity entity)
        {
            Set.Remove(entity);
            return 1;
        }

        /// <summary>
        /// 同步SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数值</param>
        /// <example>
        ///   oracle:
        ///   sql:select * from dbo.AdminUser where name = :name and phone = :phone
        ///   parameters:new object[] { 123,"123" }
        /// </example>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteSqlCommand(string sql,params object[] parameters)
        {
           return DbContext.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,sql, parameters);
        }

        /// <summary>
        /// 异步SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数值</param>
        /// <example>
        ///   oracle:
        ///   sql:select * from dbo.AdminUser where name = :name and phone = :phone
        ///   parameters:new object[] { 123,"123" }
        /// </example>
        /// <returns>返回影响行数</returns>
        public virtual Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction, sql, parameters);
        }

        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteSqlCommand(string sql)
        {
            return DbContext.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql, new object[] { });
        }

        /// <summary>
        /// 异步SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回影响行数</returns>
        public virtual Task<int> ExecuteSqlCommandAsync(string sql)
        {
            return DbContext.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction, sql, new object[] { });
        }

        /// <summary>
        ///  同步Sql执行,并返回对应的数据实体
        /// </summary>
        /// <typeparam name="TElement">实体类型,不一定是EF实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <example>
        ///   oracle:
        ///   sql:select * from dbo.AdminUser where name = :name and phone = :phone
        ///   parameters:new object[] { 123,"123" }
        /// </example>
        /// <returns></returns>
        public virtual System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TElement>(sql,parameters);
        }

        /// <summary>
        ///  同步Sql执行,并返回对应的数据实体
        /// </summary>
        /// <typeparam name="TElement">实体类型,不一定是EF实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <example>
        ///   oracle:
        ///   sql:select * from dbo.AdminUser where name = :name and phone = :phone
        ///   parameters:new object[] { 123,"123" }
        /// </example>
        /// <returns></returns>
        public virtual System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql)
        {
            return DbContext.Database.SqlQuery<TElement>(sql, new object[] { });
        }
    }
}
