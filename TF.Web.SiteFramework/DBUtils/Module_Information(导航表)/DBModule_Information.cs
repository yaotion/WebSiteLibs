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
    ///类名: Module_InformationQueryCondition
    ///说明: URL地址查询条件类
    /// </summary>
    public class Module_InformationQueryCondition
    {
        public int page = 0;
        public int rows = 0;
        //
        public int nID = 0;
        //
        public string strID = "";
        //URL地址
        public string strURL = "";
        //URL描述
        public string strUrlDescription = "";
        //上级ID
        public string strParentID = "";
        //
        public int nsortid = 0;
        //
        public int _blank = 0;
        //图标样式
        public string strIconCss = "";

        public string sort = "";
        public string order = "";

        //父id不能为空
        public string strParentIDNotNull = "";
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {
            SqlCondition = new StringBuilder();
            SqlCondition.Append(nID != 0 ? " and nID = @nID" : "");
            SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
            SqlCondition.Append(strURL != "" ? " and strURL = @strURL" : "");
            SqlCondition.Append(strUrlDescription != "" ? " and strUrlDescription = @strUrlDescription" : "");
            SqlCondition.Append(strParentID != "" ? " and strParentID = @strParentID" : "");
            SqlCondition.Append(strParentIDNotNull != "" ? " and strParentID <> '' " : "");
            SqlCondition.Append(nsortid != 0 ? " and nsortid = @nsortid" : "");
            SqlCondition.Append(_blank != 0 ? " and _blank = @_blank" : "");
            SqlCondition.Append(strIconCss != "" ? " and strIconCss = @strIconCss" : "");
            SqlParameter[] sqlParams ={
					new SqlParameter("nID",nID),
                    new SqlParameter("strID",strID),
					new SqlParameter("strURL",strURL),
					new SqlParameter("strUrlDescription",strUrlDescription),
					new SqlParameter("strParentID",strParentID),
					new SqlParameter("nsortid",nsortid),
					new SqlParameter("_blank",_blank),
					new SqlParameter("strIconCss",strIconCss)};
            Params = sqlParams;
        }
    }
    /// <summary>
    ///类名: DBModule_Information
    ///说明: URL地址数据操作类
    /// </summary>
    public class DBModule_Information : DBOperator
    {
        public DBModule_Information(string ConnectionString) : base(ConnectionString) { }
        #region 增删改
        /// <summary>
        /// 添加数据
        /// </summary>
        public bool Add(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebPlatForm_Module_Information");
            strSql.Append("(strID,strURL,strUrlDescription,strParentID,nsortid,_blank,strIconCss,nEnable,nSource)");
            strSql.Append("values(@strID,@strURL,@strUrlDescription,@strParentID,@nsortid,@_blank,@strIconCss,@nEnable,@nSource)");
            SqlParameter[] parameters = {
                    new SqlParameter("@strID", model.strID),
                    new SqlParameter("@strURL", model.strURL),
                    new SqlParameter("@strUrlDescription", model.strUrlDescription),
                    new SqlParameter("@strParentID", model.strParentID),
                    new SqlParameter("@nsortid", model.nsortid),
                    new SqlParameter("@_blank", model._blank),
                        new SqlParameter("@nSource", model.nSource),
                    new SqlParameter("@strIconCss", model.strIconCss),
                    new SqlParameter("@nEnable", model.nEnable)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 反射添加
        /// </summary>
        public bool reflectAdd(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebPlatForm_Module_Information");
            strSql.Append("(strID,strURL,nSource,_blank,strUrlDescription)");
            strSql.Append("values(@strID,@strURL,@nSource,@_blank,@strUrlDescription)");
            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@strURL", model.strURL),
					new SqlParameter("@nSource", model.nSource),
					new SqlParameter("@_blank", model._blank),
					new SqlParameter("@strUrlDescription", model.strUrlDescription)
				};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 反射更新数据
        /// </summary>
        public bool reflectUpdate(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strURL = @strURL, ");
            strSql.Append(" _blank = @_blank, ");
            strSql.Append(" strUrlDescription = @strUrlDescription");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@strURL", model.strURL),
					new SqlParameter("@_blank", model._blank),
					new SqlParameter("@strUrlDescription", model.strUrlDescription)
					};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        ///// <summary>
        ///// 模块编辑更新时更新导航名称
        ///// </summary>
        //public bool UpdateFlowModuleUpdate(Module_Information model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("Update WebPlatForm_Module_Information set ");
        //    strSql.Append(" strUrlDescription = @strUrlDescription");
        //    strSql.Append(" where strID = @strID ");

        //    SqlParameter[] parameters = {
        //            new SqlParameter("@strID", model.strID),
        //            new SqlParameter("@strUrlDescription", model.strUrlDescription)
        //            };

        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        //}

        /// <summary>
        /// 更新数据
        /// </summary>
        public bool Update(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strURL = @strURL, ");
            strSql.Append(" strUrlDescription = @strUrlDescription, ");
            strSql.Append(" nsortid = @nsortid, ");
            strSql.Append(" _blank = @_blank, ");
            strSql.Append(" strIconCss = @strIconCss,");
            strSql.Append(" strParentID = @strParentID,");
            strSql.Append(" nEnable = @nEnable ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@strURL", model.strURL),
					new SqlParameter("@strUrlDescription", model.strUrlDescription),
					new SqlParameter("@nsortid", model.nsortid),
					new SqlParameter("@_blank", model._blank),
                    new SqlParameter("@strParentID", model.strParentID),
					new SqlParameter("@strIconCss", model.strIconCss),
					new SqlParameter("@nEnable", model.nEnable)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新catoryTopNav
        /// </summary>
        public bool UpdateCatoryTopNav(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strURL = @strURL ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@strURL", model.strURL)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 更新模块名称
        /// </summary>
        public bool UpdateUrlDescription(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strUrlDescription = @strUrlDescription, ");
            strSql.Append(" nEnable = @nEnable ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@strUrlDescription", model.strUrlDescription),
					new SqlParameter("@nEnable", model.nEnable)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 更新启用状态
        /// </summary>
        public bool UpdateEnable(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" nEnable = @nEnable ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
					new SqlParameter("@nEnable", model.nEnable)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 更新父id为null
        /// </summary>
        public bool UpdateParentIDNull(string strParentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strParentID = '' ");
            strSql.Append(" where strParentID = @strParentID and nSource=2 ");

            SqlParameter[] parameters = {
					new SqlParameter("@strParentID",strParentID)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 更新PageConfig
        /// </summary>
        public bool UpdatePageConfig(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" nsortid = @nsortid, ");
            strSql.Append(" strParentID = @strParentID");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
                   new SqlParameter("@nsortid", model.nsortid),
                    new SqlParameter("@strParentID", model.strParentID)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 更新父id
        /// </summary>
        public bool UpdateParentID(Module_Information model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Information set ");
            strSql.Append(" strParentID = @strParentID");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
                    new SqlParameter("@strParentID", model.strParentID)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public bool Delete(string strID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Information ");
            if (strID != "")
            {
                strSql.Append(" where strID = @strID ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("strID",strID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public bool DeleteByModuleID(string strModuleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Information ");
            if (strModuleID != "")
            {
                strSql.Append(" where strID in (select strPowerID from WebPlatForm_Module_Power where strModuleID=@strModuleID) ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("strModuleID",strModuleID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool Exists(Module_Information _Module_Information)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from WebPlatForm_Module_Information where strID=@strID");
            SqlParameter[] parameters = {
					 new SqlParameter("strID",_Module_Information.strID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }

        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public int MaxID(string strParentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(nsortid) as maxsortid from WebPlatForm_Module_Information where strParentID=@strParentID");
            SqlParameter[] parameters = {
					 new SqlParameter("strParentID",strParentID)};
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据DataTable
        /// </summary>
        public DataTable GetDataTable(Module_InformationQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            if (QueryCondition.page == 0)
            {
                strSql.Append("select * ");
                strSql.Append(" FROM WebPlatForm_Module_Information where 1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            else
            {
                strSql.Append(@"select top " + QueryCondition.rows.ToString() + " * from WebPlatForm_Module_Information where 1 = 1 " +
                strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Module_Information where  1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
                strSql.Append(") order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        }

        ///// <summary>
        ///// 获得数据DataTable
        ///// </summary>
        //public DataTable GetNavlist(Module_InformationQueryCondition QueryCondition)
        //{
        //    SqlParameter[] sqlParams;
        //    StringBuilder strSqlOption = new StringBuilder();
        //    QueryCondition.OutPut(out strSqlOption, out sqlParams);
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" select nID,strID,strURL,strUrlDescription,strParentID,nsortid,_blank,strIconCss,nEnable,nSource from WebPlatForm_Module_Information where nSource=1 union ");
        //    strSql.Append(" select nID,strID,strURL,strUrlDescription,strParentID,nsortid,_blank,strIconCss,nEnable,nSource FROM WebPlatForm_Module_Information where 1=1 " + strSqlOption.ToString());
        //    strSql.Append(" order by ");
        //    strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
        //    strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        //}
        ///// <summary>
        ///// 获得记录总数
        ///// </summary>
        //public int GetNavlistCount(Module_InformationQueryCondition QueryCondition)
        //{
        //    SqlParameter[] sqlParams;
        //    StringBuilder strSqlOption = new StringBuilder();
        //    QueryCondition.OutPut(out strSqlOption, out sqlParams);
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(*) ");
        //    strSql.Append(" FROM WebPlatForm_Module_Information where 1=1" + strSqlOption.ToString());
        //    return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        //}

        /// <summary>
        /// 获得数据DataTable-配置页面
        /// </summary>
        public DataTable GetDataTablePageConfig(string strModuleID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * ");
            strSql.Append(" FROM WebPlatForm_Module_Information where (strParentID='" + strModuleID + "' or strParentID='' or strParentID is null)  and nSource=2 ");
            strSql.Append(" order by nsortid asc");

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        /// <summary>
        /// 获得数据DataTable-配置页面
        /// </summary>
        public DataSet GetDataTablePageRole(string strRoleID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * ");
            strSql.Append(" FROM WebPlatForm_Module where nEnable=0 ");
            strSql.Append(" order by strModuleName asc");

            strSql.Append(@"; select a.*,(Select count(1) from WebPlatForm_Module_Relation where strModuleID=a.strModuleID
              and strRoleID='" + strRoleID + "' and strPowerID=a.strPowerID) as ncount from WebPlatForm_Module_Power  a ");

            strSql.Append(@"; select * from WebPlatForm_Module_Relation where  strRoleID='" + strRoleID + "' ");
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 获得数据List
        /// </summary>
        public Module_InformationList GetDataList(Module_InformationQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            if (QueryCondition.page == 0)
            {
                strSql.Append("select * ");
                strSql.Append(" FROM WebPlatForm_Module_Information where 1=1 " + strSqlOption.ToString());
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            else
            {
                strSql.Append(@"select top " + QueryCondition.rows.ToString() + " * from WebPlatForm_Module_Information where 1 = 1 " +
                strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Module_Information where  1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
                strSql.Append(") order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            Module_InformationList list = new Module_InformationList();
            foreach (DataRow dr in dt.Rows)
            {
                Module_Information _Module_Information = new Module_Information();
                DataRowToModel(_Module_Information, dr);
                list.Add(_Module_Information);
            }
            return list;
        }
        /// <summary>
        /// 获得记录总数
        /// </summary>
        public int GetDataCount(Module_InformationQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM WebPlatForm_Module_Information where 1=1" + strSqlOption.ToString());
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        }
        /// <summary>
        /// 获得一个实体对象
        /// </summary>
        public Module_Information GetModel(Module_InformationQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM WebPlatForm_Module_Information where 1=1 " + strSqlOption.ToString());
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            Module_Information _Module_Information = null;
            if (dt.Rows.Count > 0)
            {
                _Module_Information = new Module_Information();
                DataRowToModel(_Module_Information, dt.Rows[0]);
            }
            return _Module_Information;
        }

        /// <summary>
        /// 获得父级实体对象
        /// </summary>
        public Module_Information GetParentModel(string strID)
        {
            StringBuilder strSqlOption = new StringBuilder();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from WebPlatForm_Module_Information where strID=(select strParentID FROM WebPlatForm_Module_Information where strID=@strID)");
            SqlParameter[] sqlParams = {
					 new SqlParameter("strID",strID)};
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            Module_Information _Module_Information = null;
            if (dt.Rows.Count > 0)
            {
                _Module_Information = new Module_Information();
                DataRowToModel(_Module_Information, dt.Rows[0]);
            }
            return _Module_Information;
        }
       
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DataRowToModel(Module_Information model, DataRow dr)
        {
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
            model.strURL = ObjectConvertClass.static_ext_string(dr["strURL"]);
            model.strUrlDescription = ObjectConvertClass.static_ext_string(dr["strUrlDescription"]);
            model.strParentID = ObjectConvertClass.static_ext_string(dr["strParentID"]);
            model.nsortid = ObjectConvertClass.static_ext_int(dr["nsortid"]);
            model._blank = ObjectConvertClass.static_ext_int(dr["_blank"]);
            model.strIconCss = ObjectConvertClass.static_ext_string(dr["strIconCss"]);
            model.nEnable = ObjectConvertClass.static_ext_int(dr["nEnable"]);
            model.nSource = ObjectConvertClass.static_ext_int(dr["nSource"]);
        }
    }
}
