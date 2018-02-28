using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using TF.DB.DBUtility;
using TF.CommonUtility;
using TF.WebPlatForm.Entry;
using System.Text;

namespace TF.WebPlatForm.DBUtils
{


    /// <summary>
    /// 类名:WebPlatForm_Module_Relation
    /// 描述:WebPlatForm_Module_Relation查询条件类
    /// </summary>	

    public class DBTAB_Module_RelationQuery
    {
        public int page = 0;
        public int rows = 0;
        public string sort = "";
        public string order = "";
        //
        public int? nID = null;
        //
        public string strID = "";
        //模块id
        public string strModuleID = "";
        //角色id
        public string strRoleID = "";
        //模块权限id
        public string strPowerID = "";
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {
            SqlCondition = new StringBuilder();
            SqlCondition.Append(nID != null ? " and nID = @nID" : "");
            SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
            SqlCondition.Append(strModuleID != "" ? " and strModuleID = @strModuleID" : "");
            SqlCondition.Append(strRoleID != "" ? " and strRoleID = @strRoleID" : "");
            SqlCondition.Append(strPowerID != "" ? " and strPowerID = @strPowerID" : "");
            SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strID",strID),
          new SqlParameter("strModuleID",strModuleID),
          new SqlParameter("strRoleID",strRoleID),
          new SqlParameter("strPowerID",strPowerID)};
            Params = sqlParams;
        }
    }
    /// <summary>
    /// 类名:WebPlatForm_Module_Relation
    /// 描述:WebPlatForm_Module_Relation数据操作类
    /// </summary>	

    public class DBTAB_Module_Relation : DBOperator
    {
        public DBTAB_Module_Relation(string ConnectionString) : base(ConnectionString) { }
        #region 增删改
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(WebPlatForm_Module_Relation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebPlatForm_Module_Relation");
            strSql.Append("(strID,strModuleID,strRoleID,strPowerID)");
            strSql.Append("values(@strID,@strModuleID,@strRoleID,@strPowerID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strModuleID", model.strModuleID),
          new SqlParameter("@strRoleID", model.strRoleID),
          new SqlParameter("@strPowerID", model.strPowerID)};

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WebPlatForm_Module_Relation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update WebPlatForm_Module_Relation set ");
            strSql.Append(" strModuleID = @strModuleID, ");
            strSql.Append(" strRoleID = @strRoleID, ");
            strSql.Append(" strPowerID = @strPowerID ");
            strSql.Append(" where strID = @strID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strModuleID", model.strModuleID),
          new SqlParameter("@strRoleID", model.strRoleID),
          new SqlParameter("@strPowerID", model.strPowerID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string strID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Relation ");
            if (strID != "")
            {
                strSql.Append(" where strID = @strID ");
            }
            SqlParameter[] parameters = {
               new SqlParameter("strID",strID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteRolePower(WebPlatForm_Module_Relation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Relation where 1=1 ");
            strSql.Append(" and strModuleID = @strModuleID ");
            strSql.Append(" and strRoleID = @strRoleID ");
            strSql.Append(" and strPowerID = @strPowerID ");

            SqlParameter[] parameters = {
                new SqlParameter("strRoleID",model.strRoleID),
                new SqlParameter("strPowerID",model.strPowerID),
               new SqlParameter("strModuleID",model.strModuleID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 删除不存在的角色权限关系记录
        /// </summary>
        public bool DeleteRelationByPowerID(string strModuleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"delete from WebPlatForm_Module_Relation where strModuleID=@strModuleID 
            and strPowerID not in (select strPowerID from WebPlatForm_Module_Power where strModuleID=@strModuleID) and strPowerID<>'' ");

            SqlParameter[] parameters = {
					new SqlParameter("strModuleID",strModuleID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByModuleID(string strModuleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Relation ");
            if (strModuleID != "")
            {
                strSql.Append(" where strModuleID=@strModuleID");
            }
            SqlParameter[] parameters = {
					new SqlParameter("strModuleID",strModuleID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 删除一条数据byPowerID
        /// </summary>
        public bool DeleteByPowerID(string strPowerID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Relation ");
            if (strPowerID != "")
            {
                strSql.Append(" where strPowerID=@strPowerID");
            }
            SqlParameter[] parameters = {
					new SqlParameter("strPowerID",strPowerID)};

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteForRoleID(string strRoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WebPlatForm_Module_Relation where strRoleID=@strRoleID");
            SqlParameter[] parameters = {
					new SqlParameter("strRoleID",strRoleID)};
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool Exists(WebPlatForm_Module_Relation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from WebPlatForm_Module_Relation where strRoleID=@strRoleID and strModuleID=@strModuleID");
            SqlParameter[] parameters = {
					new SqlParameter("strRoleID",model.strRoleID),
                    new SqlParameter("strModuleID",model.strModuleID)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;

        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获得数据DataTable
        /// </summary>
        public DataTable GetDataTableRolePower(string strSelRoles)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct strModuleID,strPowerID ");
            strSql.Append(" FROM WebPlatForm_Module_Relation where strRoleID in (" + strSelRoles + ")");

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获得数据DataTable
        /// </summary>
        public DataTable GetDataTable(DBTAB_Module_RelationQuery QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            if (QueryCondition.page == 0)
            {
                strSql.Append("select * ");
                strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1 " + strSqlOption.ToString());
                strSql.Append(" order by ");
                strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
                strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
            }
            else
            {
                strSql.Append(@"select top " + QueryCondition.rows.ToString() + " * from WebPlatForm_Module_Relation where 1 = 1 " +
                strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Module_Relation where  1=1 " + strSqlOption.ToString());
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
        public WebPlatForm_Module_RelationList GetDataList(DBTAB_Module_RelationQuery QueryCondition)
        {
            DataTable dt = GetDataTable(QueryCondition);
            WebPlatForm_Module_RelationList list = new WebPlatForm_Module_RelationList();
            foreach (DataRow dr in dt.Rows)
            {
                WebPlatForm_Module_Relation _WebPlatForm_Module_Relation = new WebPlatForm_Module_Relation();
                DataRowToModel(_WebPlatForm_Module_Relation, dr);
                list.Add(_WebPlatForm_Module_Relation);
            }
            return list;
        }
        /// <summary>
        /// 获得记录总数
        /// </summary>
        public int GetDataCount(DBTAB_Module_RelationQuery QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1" + strSqlOption.ToString());
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        }
        /// <summary>
        /// 获得一个实体对象
        /// </summary>
        public WebPlatForm_Module_Relation GetModel(DBTAB_Module_RelationQuery QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1 " + strSqlOption.ToString());
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            WebPlatForm_Module_Relation _WebPlatForm_Module_Relation = null;
            if (dt.Rows.Count > 0)
            {
                _WebPlatForm_Module_Relation = new WebPlatForm_Module_Relation();
                DataRowToModel(_WebPlatForm_Module_Relation, dt.Rows[0]);
            }
            return _WebPlatForm_Module_Relation;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DataRowToModel(WebPlatForm_Module_Relation model, DataRow dr)
        {
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
            model.strModuleID = ObjectConvertClass.static_ext_string(dr["strModuleID"]);
            model.strRoleID = ObjectConvertClass.static_ext_string(dr["strRoleID"]);
            model.strPowerID = ObjectConvertClass.static_ext_string(dr["strPowerID"]);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public DataTable GetPowerData(DBTAB_Module_RelationQuery TAB_Module_RelationQuery)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            TAB_Module_RelationQuery.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strPowerID as PowerID");
            strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1" + strSqlOption.ToString());

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public DataTable GetData(DBTAB_Module_RelationQuery TAB_Module_RelationQuery)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            TAB_Module_RelationQuery.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1" + strSqlOption.ToString());

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        }
        ///// <summary>
        ///// 获取数据总数
        ///// </summary>
        ///// <param name="jcid"></param>
        ///// <returns></returns>
        //public int GetDataCount(DBTAB_Module_RelationQuery TAB_Module_RelationQuery)
        //{
        //    SqlParameter[] sqlParams;
        //    StringBuilder strSqlOption = new StringBuilder();
        //    TAB_Module_RelationQuery.OutPut(out strSqlOption, out sqlParams);
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(*) ");
        //    strSql.Append(" FROM WebPlatForm_Module_Relation where 1=1" + strSqlOption.ToString());
        //    return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
        //}

        ///// <summary>
        ///// 获取分页数据
        ///// </summary>
        //public DataTable GetPaginationData(DBTAB_Module_RelationQuery TAB_Module_RelationQuery)
        //{
        //    StringBuilder strSqlOption = new StringBuilder();
        //    StringBuilder strSql = new StringBuilder();
        //    SqlParameter[] sqlParams;
        //    TAB_Module_RelationQuery.OutPut(out strSqlOption, out sqlParams);
        //    strSql.Append(@"select top " + TAB_Module_RelationQuery.rows.ToString() + " * from WebPlatForm_Module_Relation  where  1=1 " + strSqlOption.ToString() + " and nID not in ( select top " + (TAB_Module_RelationQuery.page - 1) * TAB_Module_RelationQuery.rows + " nID from WebPlatForm_Module_Relation where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
        //}
        #endregion

    }
}

