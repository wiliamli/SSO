using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Framework.Domain.Entities;

namespace Jwell.Framework.Domain.Repositories
{
    public interface IRepository<T, TPrimaryKey> : IRepository where T : Entity<TPrimaryKey>
    {
        IQueryable<T> Queryable();

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);

        int ExecuteSqlCommand(string sql, params object[] parameters);

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
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);


        /// <summary>
        /// SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回影响行数</returns>
        int ExecuteSqlCommand(string sql);


        /// <summary>
        /// 异步SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回影响行数</returns>
        Task<int> ExecuteSqlCommandAsync(string sql);


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
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

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
        IEnumerable<TElement> SqlQuery<TElement>(string sql);
    }
}
