using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.DB.DBUtility;

namespace TF.WebPlatForm.DBUtils
{
    /// <summary>
    /// 数据操作基类
    /// </summary>
    public class DBOperator
    {
        //连接字符串
        public string ConnectionString = "";
        public DBOperator(string strConnectionString)
        {
            ConnectionString = strConnectionString;
        }

        /// <summary>
        /// 获取DataSet数据集
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public DataSet GetDataset(string strSql, SqlParameter sqlParams)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql, sqlParams);
        }
    }
}
