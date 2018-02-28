using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TF.DB.DBUtility;
using System.Data;
using System.Web;
using TF.CommonUtility;

namespace TF.WebPlatForm.DBUtils
{
    /// <summary>
    ///Module 的摘要说明
    /// </summary>
    public class Module:DBOperator
    {
        public Module(string ConnectionString)
            : base(ConnectionString)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public DataTable GetDataSetModuleRole(string strRoleID)
        {
            DataSet ds = new DataSet();
            StringBuilder strSqlOption = new StringBuilder();
            string strSql = @"select '" + strRoleID + @"' as strRoleID,a.strID as strModuleID,a.strURL,
a.strUrlDescription,a.strParentID,tempModuleInfor1.strParentURLDescription,
tempModuleInfor2.strGrandUrlDescription,a.nsortid,a._blank,
a.strIconCss,a.nSource from WebPlatForm_Module_Information a
             LEFT OUTER JOIN
                          (SELECT strID, strUrlDescription AS strParentURLDescription, strParentID
                            FROM WebPlatForm_Module_Information AS WebPlatForm_Module_Information_2) AS tempModuleInfor1 ON 
                      tempModuleInfor1.strID = a.strParentID LEFT OUTER JOIN
                          (SELECT strID, strUrlDescription AS strGrandUrlDescription
                            FROM WebPlatForm_Module_Information AS WebPlatForm_Module_Information_1) AS tempModuleInfor2 ON 
                      tempModuleInfor2.strID = tempModuleInfor1.strParentID
where  nEnable=1 and (nSource=1 or ( nSource=2 and a.strID in (select strPowerID from WebPlatForm_Module_Relation where strRoleID='" + strRoleID + "'))) order by nsortid ;";
            
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql).Tables[0];
            
        }
        public DataTable GetModuleRole(string strRoleID, string strParentID)
        {
            DataTable dt = new DataTable();
            StringBuilder strSqlOption = new StringBuilder();
            strSqlOption.Append(strParentID != "" ? " and strParentID=@strParentID" : "");
            strSqlOption.Append(strRoleID != "" ? " and strRoleID=@strRoleID" : "");
            strSqlOption.Append(" and nEnable=1");
            string strSql = "select distinct strModuleID,strURL,strUrlDescription,strParentID,strRoleID,strName,strParentURLDescription,strGrandUrlDescription,nsortid,_blank,strIconCss from VIEW_WebPlatForm_Role where 1=1" + strSqlOption.ToString() + " order by nsortid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strRoleID",strRoleID),
                                           new SqlParameter("@strParentID",strParentID)
                                       };
            dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql, sqlParams).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="nRoleID"></param>
        /// <param name="nParentID"></param>
        /// <returns></returns>
        public DataTable GetModuleRole(string strRoleID)
        {
            DataTable dt = new DataTable();
            StringBuilder strSqlOption = new StringBuilder();
            strSqlOption.Append(strRoleID != "" ? " and strRoleID=@strRoleID " : "");
            strSqlOption.Append(" and nEnable=1");
            string strSql = "select distinct strModuleID,strURL,strUrlDescription,strParentID,strRoleID,strName,strParentURLDescription,strGrandUrlDescription,nsortid,_blank,strIconCss from VIEW_WebPlatForm_Role where 1=1" + strSqlOption.ToString() + " order by nsortid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strRoleID",strRoleID)
                                       };
            dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql, sqlParams).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取登录后转向页面
        /// </summary>
        /// <returns></returns>
        public string GetLoginReturnUrl(string strRoleID)
        {
            string strSql = string.Format("select top 1 strURL from VIEW_WebPlatForm_Role where nRoleID={0} and strURL <>'' order by nsortid desc",
                strRoleID);
            return TF.CommonUtility.ObjectConvertClass.static_ext_string(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
    }
}
