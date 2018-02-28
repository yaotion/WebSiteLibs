using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using TF.DB.DBUtility;
using TF.CommonUtility;
using System.Collections.Generic;
using TF.WebPlatForm.Entry;

namespace TF.WebPlatForm.DBUtils
{
    /// <summary>
    ///类名: WebPlatForm_ModuleQueryCondition
    ///说明: 模块查询条件类
    /// </summary>
    public class WebPlatForm_ModuleQueryCondition
    {
        public int? page = null;
        public int? rows = null;
        public string sort = "";
        public string order = "";
        //
        public int? nID = null;
        //模块id
        public string strID = "";
        //模块名称
        public string strModuleName = "";
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {
            SqlCondition = new StringBuilder();
            SqlCondition.Append(nID != null ? " and nID = @nID" : "");
            SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
            SqlCondition.Append(strModuleName != "" ? " and strModuleName = @strModuleName" : "");
            SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strID",strID),
          new SqlParameter("strModuleName",strModuleName)};
            Params = sqlParams;
        }
    }
    /// <summary>
    ///类名: DBWebPlatForm_Module
    ///说明: 模块数据操作类
    /// </summary>
    public class DBWebPlatForm_Module : DBOperator
    {
        public DBWebPlatForm_Module(string ConnectionString) : base(ConnectionString) { }
        #region 增删改
        /// <summary>
        /// 添加数据
        /// </summary>
        public int Add(WebPlatForm_Module model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebPlatForm_Module");
            strSql.Append("(strID,strModuleName)");
            strSql.Append("values(@strID,@strModuleName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strModuleName", model.strModuleName)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool Update(WebPlatForm_Module model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module set ");
            strSql.Append(" strModuleName = @strModuleName ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strModuleName", model.strModuleName)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public bool updateEnable(WebPlatForm_Module model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module set ");
            strSql.Append(" nEnable = @nEnable ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@nEnable", model.nEnable)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public bool Delete(string strID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module ");
            if (strID != "")
            {
                strSql.Append(" where strID = @strID ");
            }
            SqlParameter[] parameters = {
          new SqlParameter("strID",strID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool Exists(WebPlatForm_Module _WebPlatForm_Module)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from WebPlatForm_Module where strID=@strID");
            SqlParameter[] parameters = {
           new SqlParameter("strID",_WebPlatForm_Module.strID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        /// <summary>
        /// 获得数据DataTable
        /// </summary>
        public DataTable GetDataTable(WebPlatForm_ModuleQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            if (QueryCondition.page == null)
            {
                strSql.Append("select * ");
                strSql.Append(" FROM WebPlatForm_Module where 1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            else
            {
                strSql.Append(@"select top " + QueryCondition.rows.ToString() + " * from WebPlatForm_Module where 1 = 1 " +
                strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Module where  1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
                strSql.Append(") order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        }



        /// <summary>
        /// 获得数据List
        /// </summary>
        public WebPlatForm_ModuleList GetDataList(WebPlatForm_ModuleQueryCondition QueryCondition)
        {
            DataTable dt = GetDataTable(QueryCondition);
            WebPlatForm_ModuleList list = new WebPlatForm_ModuleList();
            foreach (DataRow dr in dt.Rows)
            {
                WebPlatForm_Module _WebPlatForm_Module = new WebPlatForm_Module();
                DataRowToModel(_WebPlatForm_Module, dr);
                list.Add(_WebPlatForm_Module);
            }
            return list;
        }
        /// <summary>
        /// 获得记录总数
        /// </summary>
        public int GetDataCount(WebPlatForm_ModuleQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM WebPlatForm_Module where 1=1" + strSqlOption.ToString());
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        }
        /// <summary>
        /// 获得一个实体对象
        /// </summary>
        public WebPlatForm_Module GetModel(WebPlatForm_ModuleQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM WebPlatForm_Module where 1=1 " + strSqlOption.ToString());
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            WebPlatForm_Module _WebPlatForm_Module = null;
            if (dt.Rows.Count > 0)
            {
                _WebPlatForm_Module = new WebPlatForm_Module();
                DataRowToModel(_WebPlatForm_Module, dt.Rows[0]);
            }
            return _WebPlatForm_Module;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DataRowToModel(WebPlatForm_Module model, DataRow dr)
        {
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
            model.strModuleName = ObjectConvertClass.static_ext_string(dr["strModuleName"]);
        }
    }
}
