using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
namespace TF.Common.DB
{
    /// <summary>
    /// 数据操作基类
    /// </summary>
    public class DBOperator
    {
        //连接字符串
        public string ConnectionString = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        public DBOperator(string strConnectionString)
        {
            ConnectionString = strConnectionString;
        }

        /// <summary>
        /// 执行SQL语句获取DataTable形式的数据集
        /// </summary>
        /// <param name="SQLText">查询语句</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>返回结果数据集</returns>
        public DataTable ExecSQLDatatable(string SQLText, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQLText, SQLParams).Tables[0];           
        }
     
        /// <summary>
        /// 执行SQL语句获取DataTable形式的数据集
        /// </summary>
        /// <param name="SQLText">查询语句</param>
        /// <returns>返回结果数据集</returns>
        public DataTable ExecSQLDatatable(string SQLText)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQLText).Tables[0];
        }

        /// <summary>
        /// 执行存储过程获取DataTable形式的数据集
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>返回结果数据集</returns>
        public DataTable ExecPROCDatatable(string PROCName, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, PROCName, SQLParams).Tables[0];
        }

        /// <summary>
        /// 执行存储过程获取DataTable形式的数据集
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <returns>返回结果数据集</returns>
        public DataTable ExecPROCDatatable(string PROCName)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, PROCName).Tables[0];
        }

        /// <summary>
        ///执行一个SQL语句并返回SQL语句影响的行数
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>SQL语句影响的行数</returns>
        public int ExecSQLNonQuery(string SQLText, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQLText, SQLParams);
        }
        /// <summary>
        ///执行一个SQL语句并返回SQL语句影响的行数
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns>SQL语句影响的行数</returns>
        public int ExecSQLNonQuery(string SQLText)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQLText);
        }

        /// <summary>
        ///执行一个存储过程并返回存储过程影响的行数
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>存储过程影响的行数</returns>
        public int ExecPROCNonQuery(string PROCName, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, PROCName, SQLParams);
        }

        /// <summary>
        ///执行一个存储过程并返回存储过程影响的行数
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <returns>存储过程影响的行数</returns>
        public int ExecPROCNonQuery(string PROCName)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, PROCName);
        }

        /// <summary>
        ///执行一个SQL语句并返回获取数据的第一行第一列
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>SQL语句返回的第一行第一列</returns>
        public object ExecuteSQLScalar(string SQLText, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, SQLText, SQLParams);
        }
        /// <summary>
        ///执行一个SQL语句并返回获取数据的第一行第一列
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns>SQL语句返回的第一行第一列</returns>
        public object ExecuteSQLScalar(string SQLText)
        {
            return SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, SQLText);
        }

        /// <summary>
        ///执行一个存储过程并返回获取数据的第一行第一列
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <param name="SQLParams">SQL参数</param>
        /// <returns>存储过程返回的第一行第一列</returns>
        public object ExecutePROCScalar(string PROCName, SqlParameter[] SQLParams)
        {
            return SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, PROCName, SQLParams);
        }
        /// <summary>
        ///执行一个存储过程并返回获取数据的第一行第一列
        /// </summary>
        /// <param name="PROCName">存储过程名称</param>
        /// <returns>存储过程返回的第一行第一列</returns>
        public object ExecutePROCScalar(string PROCName)
        {
            return SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, PROCName);
        }

         //SqlHelper.ExecuteNonQuery;
         //   SqlHelper.ExecuteScalar;
         //   SqlHelper.ExecuteReader;
        
    }

}
