using Jwell.Modules.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Jwell.Modules.Dapper
{
    public class DapperHelper<T> : IDapperHelper<T> where T : BaseEntity
    {
        private IDbConnection conn = null;

        public long Add(T entity, IDbTransaction trans = null)
        {
            throw new NotImplementedException();
        }

        public long AddList(IEnumerable<T> entities, IDbTransaction trans = null)
        {
            throw new NotImplementedException();
        }


        public U ExecuteScalar<U>(string sql, object parameters = null)
        {
            try
            {
                using (conn = DapperFactory.CreateOracleConnection)
                {
                    return conn.ExecuteScalar<U>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteSql(string sql, object parameters = null, IDbTransaction trans = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }

            try
            {
                if (trans == null)
                {
                    using (conn = DapperFactory.CreateOracleConnection)
                    {
                        return conn.Execute(sql, parameters);
                    }
                }
                else
                {
                    conn = trans.Connection;
                    return conn.Execute(sql, parameters, trans);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(string sql, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }
            try
            {
                using (conn = DapperFactory.CreateOracleConnection)
                {
                    return conn.Query<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<U> Query<U>(string sql, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException("sql");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = DapperFactory.CreateOracleConnection)
                {
                    return conn.Query<U>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> QueryPaged(string sql, int pageIndex, int pageSize, out int count, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<U> QueryPaged<U>(string sql, int pageIndex, int pageSize, out int count, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity, IDbTransaction trans = null)
        {
            throw new NotImplementedException();
        }
    }
}
