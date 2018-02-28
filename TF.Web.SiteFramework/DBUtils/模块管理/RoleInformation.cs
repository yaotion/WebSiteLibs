using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TF.DB.DBUtility;
using System.Data;
using TF.WebPlatForm.Logic;
namespace TF.WebPlatForm.DBUtils
{
    /// <summary>
    ///RoleInformation 角色类
    /// </summary>
    public class RoleInformation
    {
        public RoleInformation()
        {

        }
        public DataTable GetRoleInfor()
        {
            string strSql = "select * from TAB_Module_Role";
            return SqlHelper.ExecuteDataset(ConData.WebSiteConnectionString, CommandType.Text, strSql).Tables[0];
        }

        public string Delete(string strID)
        {
            string strSql = "select count(*) from TAB_System_DutyUser where nDeleteState=0 and nRoleID=" + strID;
            object obj = SqlHelper.ExecuteScalar(ConData.WebSiteConnectionString, CommandType.Text, strSql);
            int nResult = 0;
            if (obj.ToString() == "0")
            {
                strSql = "delete WebPlatForm_Module_Relation where nRoleID=" + strID;
                nResult = SqlHelper.ExecuteNonQuery(ConData.WebSiteConnectionString, CommandType.Text, strSql);
                strSql = "delete TAB_Module_Role where nID=" + strID;
                nResult += SqlHelper.ExecuteNonQuery(ConData.WebSiteConnectionString, CommandType.Text, strSql);
                return "";
            }
            else
            {
                return "该用户组中还含有" + obj.ToString() + "个用户,请先移除,然后再删除用户组";
            }

        }

        public int Update(string strID, string strRoleName)
        {
            string strSql = "update TAB_Module_Role set strRoleName=@RoleName where nID=@nID";
            SqlParameter[] SqlPs ={
                                     new SqlParameter("RoleName",strRoleName),
                                     new SqlParameter("nID",strID)
                                 };
            return SqlHelper.ExecuteNonQuery(ConData.WebSiteConnectionString, CommandType.Text, strSql, SqlPs);
        }

        public DataTable GetModuleRoleInfo(string RoleID)
        {

            string strSql = "select * from VIEW_WebPlatForm_Role where nRoleID=@RoleID and nParentID>1 and strURL is not null and strURL<>''";
            SqlParameter[] SqlParams ={
                                         new SqlParameter("RoleID",RoleID)
                                     };
            return SqlHelper.ExecuteDataset(ConData.WebSiteConnectionString, CommandType.Text, strSql, SqlParams).Tables[0];
        }
    }
}