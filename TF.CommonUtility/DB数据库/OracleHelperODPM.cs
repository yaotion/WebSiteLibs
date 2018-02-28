using System;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace TF.DB
{

    /// <summary>
    ///Oracle数据库操作帮助类
    /// </summary>
    public abstract class OracleHelperODPM
    {

        ////从配置文件中读取配置好的连接字符串
       // public static readonly string OracleConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;


        //为缓存的参数创建一个hashtable
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 增删改操作使用此方法（需要一个存在的连接）
        /// </summary> 
        /// <param name="conn">一个存在的OracleConnection参数</param>
        /// <param name="commandType">命令类型（sql或者存储过程）</param>
        /// <param name="commandText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">命令所需参数数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            // 创建一个OracleCommand
            OracleCommand cmd = new OracleCommand();
            cmd.BindByName = true;
            //调用静态方法PrepareCommand完成赋值操作
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            //执行命令返回
            int val = cmd.ExecuteNonQuery();
            //清空参数
            cmd.Parameters.Clear();
            return val;
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            cmd.BindByName = true;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            // Create the DataAdapter & DataSet
            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                // Return the dataset
                return ds;
            }
        }
        public static object ExecuteScalar(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            OracleCommand cmd = new OracleCommand();
            cmd.BindByName = true;
            PrepareCommand(cmd, connection, (OracleTransaction)null, cmdType, cmdText, commandParameters);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            return retval;
        }
     

        /// <summary>
        /// 一个静态的预处理函数
        /// </summary>
        /// <param name="cmd">存在的OracleCommand对象</param>
        /// <param name="conn">存在的OracleConnection对象</param>
        /// <param name="trans">存在的OracleTransaction对象</param>
        /// <param name="cmdType">命令类型（sql或者存在过程）</param>
        /// <param name="cmdText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //如果连接未打开，先打开连接
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //未要执行的命令设置参数
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //如果传入了事务，需要将命令绑定到指定的事务上去
            if (trans != null)
                cmd.Transaction = trans;

            //将传入的参数信息赋值给命令参数
            if (commandParameters != null)
            {
                cmd.Parameters.AddRange(commandParameters);
            }
        }
    }
}