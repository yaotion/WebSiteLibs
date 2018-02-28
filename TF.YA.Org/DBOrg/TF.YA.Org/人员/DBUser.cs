using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;

namespace TF.YA.Org
{
    public class DBUser
    {
        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="AUser"></param>
        public static void AddUser(ILog Log, SqlConnection Conn,User AUser)
        {
            string strSql = @"insert into TAB_Org_User (UserNumber,UserName,NameJP,TelNumber,UserGUID,DeptID,DeptName,PostID,PostName,DeptFullName) 
                values (@UserNumber,@UserName,@NameJP,@TelNumber,@UserGUID,@DeptID,@DeptName,@PostID,@PostName,@DeptFullName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",AUser.UserNumber),
                                           new SqlParameter("UserName",AUser.UserName),
                                           new SqlParameter("NameJP",AUser.NameJP),
                                           new SqlParameter("TelNumber",AUser.TelNumber),
                                           new SqlParameter("UserGUID",AUser.UserGUID),
                                           new SqlParameter("DeptID",AUser.DeptID),
                                           new SqlParameter("DeptName",AUser.DeptName),
                                           new SqlParameter("PostID",AUser.PostID),
                                           new SqlParameter("PostName",AUser.PostName),
                                           new SqlParameter("DeptFullName",AUser.DeptFullName),
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void UpdateUser(ILog Log, SqlConnection Conn, User AUser)
        {
            string strSql = @"update TAB_Org_User set UserName=@UserName,NameJP=@NameJP,TelNumber=@TelNumber,DeptID=@DeptID,DeptName=@DeptName,
                PostID=@PostID,PostName=@PostName,DeptFullName=@DeptFullName where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",AUser.UserNumber),
                                           new SqlParameter("UserName",AUser.UserName),
                                           new SqlParameter("NameJP",AUser.NameJP),
                                           new SqlParameter("TelNumber",AUser.TelNumber),
                                           new SqlParameter("UserGUID",AUser.UserGUID),
                                           new SqlParameter("DeptID",AUser.DeptID),
                                           new SqlParameter("DeptName",AUser.DeptName),
                                           new SqlParameter("PostID",AUser.PostID),
                                           new SqlParameter("PostName",AUser.PostName),
                                           new SqlParameter("DeptFullName",AUser.DeptFullName),
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteUser(ILog Log, SqlConnection Conn,string UserNumber)
        {
            string strSql = @"delete from TAB_Org_User where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",UserNumber)
                                           
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        /// <summary>
        /// 根据工号获取人员信息
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="UserNumber"></param>
        /// <param name="AUser"></param>
        /// <returns></returns>
        public static bool GetUser(ILog Log, SqlConnection Conn, string UserNumber,User AUser)
        {
            string strSql = @"select * from TAB_Org_User where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("UserNumber",UserNumber)
            };
            DataTable  dt = SqlHelper.ExecuteDataset(Conn,CommandType.Text,strSql,sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                AUser.DeptFullName = TF.DB.DBConvert.ToString(dt.Rows[0]["DeptFullName"]);
                AUser.DeptID = TF.DB.DBConvert.ToString(dt.Rows[0]["DeptID"]);
                AUser.DeptName = TF.DB.DBConvert.ToString(dt.Rows[0]["DeptName"]);
                AUser.NameJP = TF.DB.DBConvert.ToString(dt.Rows[0]["NameJP"]);
                AUser.PostID = TF.DB.DBConvert.ToString(dt.Rows[0]["PostID"]);
                AUser.PostName = TF.DB.DBConvert.ToString(dt.Rows[0]["PostName"]);
                AUser.TelNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["TelNumber"]);
                AUser.UserGUID = TF.DB.DBConvert.ToString(dt.Rows[0]["UserGUID"]);
                AUser.UserName = TF.DB.DBConvert.ToString(dt.Rows[0]["UserName"]);
                AUser.UserNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["UserNumber"]);
                return true;
            }
           
            
            return false;
        }
        /// <summary>
        /// 添加人员的组织关系
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="Relation"></param>
        public static void AddUserDept(ILog Log, SqlConnection Conn, UserDeptRelation Relation)
        {
            string strSql = @"insert into TAB_Org_User_DeptRelation (UserNumber,DeptID,DeptLevel,DeptName) 
                values (@UserNumber,@DeptID,@DeptLevel,@DeptName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",Relation.UserNumber),
                                           new SqlParameter("DeptID",Relation.DeptID),
                                           new SqlParameter("DeptLevel",Relation.DeptLevel),
                                           new SqlParameter("DeptName",Relation.DeptName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        /// <summary>
        /// 获取人员的组织关系列表
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static List<UserDeptRelation> GetUserDept(ILog Log, SqlConnection Conn,string UserNumber)
        {
            List<UserDeptRelation> result = new List<UserDeptRelation>();

            string strSql = @"select * from TAB_Org_User_DeptRelation where UserNumber = @UserNumber order by DeptLevel";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                 new SqlParameter("UserNumber",UserNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserDeptRelation r = new UserDeptRelation();
                r.DeptID = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptID"]);
                r.DeptLevel = TF.DB.DBConvert.ToInt32(dt.Rows[i]["DeptLevel"]);
                r.DeptName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptName"]);
                r.UserNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["UserNumber"]);                
                result.Add(r);
            }
            return result;
        }
        /// <summary>
        /// 删除用户所属部门信息
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="UserNumber"></param>
        public static void DeleteUserDept(ILog Log, SqlConnection Conn, string UserNumber)
        {
            string strSql = @"delete from  TAB_Org_User_DeptRelation where UserNumber=@UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",UserNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
      
        /// <summary>
        /// 查询人员
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="UserNumber"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static List<User> QueryUser(ILog Log, SqlConnection Conn,int PageIndex,int PageCount,string DeptID,string UserNumber,string UserName)
        {
            List<User> result = new List<User>();
            string strSql = @"select top " + PageCount.ToString() + " * from View_Org_User where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (UserNumber.Trim().Length > 0)
            {
                strCondition = strCondition + " and UserNumber = @UserNumber";
                sqlParamList.Add(new SqlParameter("UserNumber", UserNumber));
            }
            if (UserName.Trim().Length > 0)
            {
                strCondition = strCondition + " and UserName like @UserName";
                sqlParamList.Add(new SqlParameter("UserName", UserName + "%"));
            }
            if (DeptID.Trim().Length > 0)
            {
                strCondition = strCondition + " and DeptID in (select DeptID from TAB_Org_Dept_Relation where HigherDepartID = @DeptID)";
                sqlParamList.Add(new SqlParameter("DeptID", DeptID));
            }
        
            strSql += strCondition + " and UserNumber  not in (select top " + ((PageIndex -1) *PageCount).ToString()
                + " UserNumber from View_Org_User where 1=1 " + strCondition + " order by UserNumber) "; 
            strSql = strSql + " order by UserNumber";
            SqlParameter[] sqlParams = sqlParamList.ToArray();
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User u = new User();
                u.DeptFullName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptFullName"]);
                u.DeptID = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptID"]);
                u.DeptName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptName"]);
                u.NameJP = TF.DB.DBConvert.ToString(dt.Rows[i]["NameJP"]);
                u.PostID = TF.DB.DBConvert.ToString(dt.Rows[i]["PostID"]);
                u.PostName = TF.DB.DBConvert.ToString(dt.Rows[i]["PostName"]);
                u.TelNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["TelNumber"]);
                u.UserGUID = TF.DB.DBConvert.ToString(dt.Rows[i]["UserGUID"]);
                u.UserName = TF.DB.DBConvert.ToString(dt.Rows[i]["UserName"]);
                u.UserNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["UserNumber"]);
                result.Add(u);
            }
            return result;
        }


        public static int QueryUserCount(ILog Log, SqlConnection Conn, string DeptID, string UserNumber, string UserName)
        {
            List<User> result = new List<User>();
            string strSql = @"select count(*) from View_Org_User where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (UserNumber.Trim().Length > 0)
            {
                strCondition = strCondition + " and UserNumber = @UserNumber";
                sqlParamList.Add(new SqlParameter("UserNumber", UserNumber));
            }
            if (UserName.Trim().Length > 0)
            {
                strCondition = strCondition + " and UserName like @UserName";
                sqlParamList.Add(new SqlParameter("UserName", UserName + "%"));
            }
            if (DeptID.Trim().Length > 0)
            {
                strCondition = strCondition + " and DeptID in (select DeptID from TAB_Org_Dept_Relation where HigherDepartID = @DeptID)";
                sqlParamList.Add(new SqlParameter("DeptID", DeptID));
            }
   
            strSql = strSql + strCondition;
            SqlParameter[] sqlParams = sqlParamList.ToArray();
            object obj = SqlHelper.ExecuteScalar(Conn, CommandType.Text, strSql, sqlParams);
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 添加人员特征
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="UserFeature"></param>
        public static void AddFeature(ILog Log, SqlConnection Conn, Feature UserFeature)
        {
            string strSql = @"insert into TAB_Org_User_Feature (UserNumber,FeatureType,FeatureContent,FeatureIndex) 
                values (@UserNumber,@FeatureType,@FeatureContent,@FeatureIndex)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("UserNumber",UserFeature.UserNumber),
                                           new SqlParameter("FeatureType",UserFeature.FeatureType),
                                           new SqlParameter("FeatureContent",UserFeature.FeatureContent),
                                           new SqlParameter("FeatureIndex",UserFeature.FeatureIndex)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }


        /// <summary>
        /// 获取人员特征信息
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static List<Feature> GetUserFeature(ILog Log, SqlConnection Conn,string UserNumber)
        {
            List<Feature> result = new List<Feature>();

            string strSql = @"select * from TAB_Org_User_Feature where UserNumber = @UserNumber order by FeatureType,FeatureIndex";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("UserNumber",UserNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Feature f = new Feature();
                f.UserNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["UserNumber"]);
                f.FeatureType = TF.DB.DBConvert.ToInt32(dt.Rows[i]["FeatureType"]);
                f.FeatureContent = dt.Rows[i]["FeatureContent"];
                f.FeatureIndex = TF.DB.DBConvert.ToInt32(dt.Rows[i]["FeatureIndex"]);
                result.Add(f);
            }
            return result;
        }

        public static bool ExistUserFeature(ILog Log, SqlConnection Conn, string UserNumber, int FeatureType, int FeatureIndex)
        {

            string strSql = @"select * from TAB_Org_User_Feature where UserNumber=@UserNumber and FeatureType=@FeatureType and FeatureIndex = @FeatureIndex";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("UserNumber",UserNumber),
                                           new SqlParameter("FeatureType",FeatureType),
                                           new SqlParameter("FeatureIndex",FeatureIndex)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public static List<FeatureType> GetFeatureTypeList(ILog Log, SqlConnection Conn)
        {
            List<FeatureType> result = new List<FeatureType>();

            string strSql = @"select * from TAB_Org_User_FeatureType order by FeatureTypeID";
            SqlParameter[] sqlParams = new SqlParameter[]{};
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FeatureType f = new FeatureType();
                f.FeatureTypeID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["FeatureTypeID"]);
                f.FeatureTypeName = TF.DB.DBConvert.ToString(dt.Rows[i]["FeatureTypeName"]);               
                result.Add(f);
            }
            return result;
        }
        /// <summary>
        /// 更新人员特征
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="UserFeature"></param>
        public static void UpdateFeature(ILog Log, SqlConnection Conn, Feature UserFeature)
        {
            string strSql = @"Update TAB_Org_User_Feature set FeatureContent=@FeatureContent where UserNumber=@UserNumber and FeatureType=@FeatureType and FeatureIndex=@FeatureIndex";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("FeatureContent",UserFeature.FeatureContent),
                new SqlParameter("UserNumber",UserFeature.UserNumber),
                new SqlParameter("FeatureType",UserFeature.FeatureType),
                new SqlParameter("FeatureIndex",UserFeature.FeatureIndex)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            
        }

        public static void DeleteUserFeature(ILog Log, SqlConnection Conn, string UserNumber)
        {
            string strSql = @"delete from TAB_Org_User_Feature where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("UserNumber",UserNumber)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

    }
}
