using Jwell.Modules.Configure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Repositories.HR
{
    public class SqlHelper
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public static string GetSqlConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["JwellHR"].ConnectionString;
        }

        /// <summary>
        /// 封装一个执行的sql 返回受影响的行数
        /// </summary>
        /// <param name="sqlText">执行的sql脚本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行sql，返回查询结果中的第一行第一列的值
        /// </summary>
        /// <param name="sqlText">执行的sql脚本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>查询结果中的第一行第一列的值</returns>
        public static object ExecuteScalar(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行sql 返回一个DataTable
        /// </summary>
        /// <param name="sqlText">执行的sql脚本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>返回一个DataTable</returns>
        public static DataTable ExecuteDataTable(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlText, GetSqlConnectionString()))
            {
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddRange(parameters);
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 执行sql脚本
        /// </summary>
        /// <param name="sqlText">执行的sql脚本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>返回一个SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sqlText, params SqlParameter[] parameters)
        {
            //SqlDataReader要求，它读取数据的时候有，它独占它的SqlConnection对象，而且SqlConnection必须是Open状态
            SqlConnection conn = new SqlConnection(GetSqlConnectionString());//不要释放连接，因为后面还需要连接打开状态
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = sqlText;
            cmd.Parameters.AddRange(parameters);
            //CommandBehavior.CloseConnection当SqlDataReader释放的时候，顺便把SqlConnection对象也释放掉
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
