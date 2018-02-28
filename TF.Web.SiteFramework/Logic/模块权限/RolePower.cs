using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.WebPlatForm.Entry;
using TF.WebPlatForm.DBUtils;
using System.Data;
using TF.CommonUtility;

namespace TF.WebPlatForm.Logic
{
    public class RolePower
    {
        /// <summary>
        /// 根据模块ID
        /// 获取角色拥有的权限
        /// 返回权限id字符串逗号隔开 格式如 add,delete,edit
        /// </summary>
        /// <param name="strModuleID"></param>
        public static object[,] GetRolePowerByModuleID(string strModuleID, object[,] powerArray)
        {
            return GetUserPowers(UserInformation.LoginUser.strRoleID, strModuleID, powerArray);            
        }

        public static object[,] GetUserPowers(string RoleID, string strModuleID, object[,] powerArray)
        {
            DBTAB_Module_Relation dalRelation = new DBTAB_Module_Relation(ConData.WebSiteConnectionString);        
            string strRoleID = ObjectConvertClass.static_ext_string(RoleID);
            if (strRoleID != "")
            {
                ///兼容nid//////////////如果是nid那么先查到角色的strID然后去查模块管理////////////
                if (strRoleID.Trim().Length < 36)
                {
                    DBUserRole dalUserRole = new DBUserRole(ConData.WebSiteConnectionString);
                    TF.WebPlatForm.Entry.UserRole modelUserRole = dalUserRole.GetModel_Clear(new UserRoleQueryCondition { nID = ObjectConvertClass.static_ext_int(strRoleID) });
                    if (modelUserRole != null)
                    {
                        strRoleID = modelUserRole.strID;
                    }
                }

                ///////////////////////////////
                DataTable dtRelation = dalRelation.GetPowerData(new DBTAB_Module_RelationQuery { strModuleID = strModuleID, strRoleID = strRoleID });
                StringBuilder strRtn = new StringBuilder();
                foreach (DataRow drRelation in dtRelation.Rows)
                {
                    for (int i = 0; i < powerArray.GetLength(1); i++)
                    {
                        if (ObjectConvertClass.static_ext_string(drRelation["PowerID"]) == ObjectConvertClass.static_ext_string(powerArray[0, i]))
                        {
                            powerArray[1, i] = true;
                        }

                    }
                }
            }
            return powerArray;
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public DataTable GetRole()
        {
            DBUserRole dalRole = new DBUserRole(ConData.WebSiteConnectionString);
            DataTable dtRole = dalRole.GetDataTable(new UserRoleQueryCondition { });
            return dtRole;
        }
    }
}
