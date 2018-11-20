using Jwell.Modules.Dapper.Core;
using System.Collections.Generic;
using System.Data;

namespace Jwell.Modules.Dapper
{
    public interface IDapperHelper<T> where T : BaseEntity
    {
        /// <summary>
        /// 根据sql查询指定实体
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, object parameters = null);

        /// <summary>
        /// 根据sql查询指定实体
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<U> Query<U>(string sql, object parameters = null);

        /// <summary>
        /// 根据sql查询指定实体 [分页查询]
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">记录总数</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> QueryPaged(string sql, int pageIndex, int pageSize, out int count, object parameters = null);

        /// <summary>
        /// 根据sql查询指定实体 [分页查询]
        /// </summary>
        /// <typeparam name="U">指定实体</typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<U> QueryPaged<U>(string sql, int pageIndex, int pageSize, out int count, object parameters = null);

        /// <summary>
        /// 添加实体进数据库
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        long Add(T entity, IDbTransaction trans = null);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities">实体类集合</param>
        /// <returns></returns>
        long AddList(IEnumerable<T> entities, IDbTransaction trans = null);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(T entity, IDbTransaction trans = null);

        /// <summary>
        /// 返回单个对象值
        /// </summary>
        /// <typeparam name="U">自定义类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        U ExecuteScalar<U>(string sql, object parameters = null);

        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSql(string sql, object parameters = null, IDbTransaction trans = null);

        /// <summary>
        /// 根据Guid获取对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetById(string id);

    }
}
