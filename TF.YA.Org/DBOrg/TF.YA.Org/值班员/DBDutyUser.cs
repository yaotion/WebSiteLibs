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
    public class DBDutyUser
    {
        public static void AddDutyPost(ILog Log, SqlConnection Conn, DutyUserPost PT)
        {
            string strSql = @"insert into TAB_Org_DutyUser_Post (PostTypeID,PostTypeName)  values (@PostTypeID,@PostTypeName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostTypeID",PT.PostTypeID),
                                           new SqlParameter("PostTypeName",PT.PostTypeName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteDutyPost(ILog Log, SqlConnection Conn, int PostTypeID)
        {
            string strSql = @"delete from TAB_Org_DutyUser_Post where PostTypeID = @PostTypeID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostTypeID",PostTypeID)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static List<DutyUserPost> GetAllDutyPosts(ILog Log, SqlConnection Conn)
        {

            List<DutyUserPost> result = new List<DutyUserPost>();
            string strSql = "select * from TAB_Org_DutyUser_Post order by PostTypeID,PostTypeName";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DutyUserPost p = new DutyUserPost();
                p.PostTypeID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["PostTypeID"]);
                p.PostTypeName = TF.DB.DBConvert.ToString(dt.Rows[i]["PostTypeName"]);
                result.Add(p);
            }

            return result;

            
            
        }
        
        
        public static void AddDutyUser(ILog Log, SqlConnection Conn, DutyUser ADutyUser)
        {
            string strSql = @"insert into TAB_Org_DutyUser (DutyUserNumber,DutyUserName,RoleID,RoleName,Password) 
                values (@DutyUserNumber,@DutyUserName,@RoleID,@RoleName,@Password)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DutyUserNumber",ADutyUser.DutyUserNumber),
                                           new SqlParameter("DutyUserName",ADutyUser.DutyUserName),
                                           new SqlParameter("RoleID",ADutyUser.RoleID),
                                           new SqlParameter("RoleName",ADutyUser.RoleName),
                                           new SqlParameter("Password",ADutyUser.Password)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void UpdateDutyUser(ILog Log, SqlConnection Conn, DutyUser ADutyUser)
        {
            string strSql = @"update TAB_Org_DutyUser set DutyUserName=@DutyUserName,RoleID=@RoleID,RoleName=@RoleName where DutyUserNumber = @DutyUserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DutyUserName",ADutyUser.DutyUserName),
                                           new SqlParameter("RoleID",ADutyUser.RoleID),
                                           new SqlParameter("RoleName",ADutyUser.RoleName),
                                           new SqlParameter("DutyUserNumber",ADutyUser.DutyUserNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteDutyUser(ILog Log, SqlConnection Conn, string DutyUserNumber)
        {
            string strSql = @"delete from TAB_Org_DutyUser where DutyUserNumber = @DutyUserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DutyUserNumber",DutyUserNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static bool GetDutyUser(ILog Log, SqlConnection Conn,string DutyUserNumber,DutyUser ADutyUser)
        {
            string strSql = @"select * from TAB_Org_DutyUser where DutyUserNumber = @DutyUserNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DutyUserNumber",DutyUserNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {

                ADutyUser.DutyUserNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["DutyUserNumber"]);
                ADutyUser.DutyUserName = TF.DB.DBConvert.ToString(dt.Rows[0]["DutyUserName"]);
                ADutyUser.Password = TF.DB.DBConvert.ToString(dt.Rows[0]["Password"]);
                ADutyUser.RoleID = TF.DB.DBConvert.ToString(dt.Rows[0]["RoleID"]);
                ADutyUser.RoleName = TF.DB.DBConvert.ToString(dt.Rows[0]["RoleName"]);
                ADutyUser.TokenID = TF.DB.DBConvert.ToString(dt.Rows[0]["TokenID"]);
                ADutyUser.TokenTime = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["TokenTime"]);                
                return true;
            }
            return false;
        }

        public static List<DutyUser> QueryDutyUser(ILog Log, SqlConnection Conn, int PageIndex, int PageCount, string DutyUserNumber, string DutyUserName)
        {
            List<DutyUser> result = new List<DutyUser>();
            string strSql = @"select top " + PageCount.ToString() + " * from TAB_Org_DutyUser where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (DutyUserNumber.Trim().Length > 0)
            {
                strCondition = strCondition + " and DutyUserNumber = @DutyUserNumber";
                sqlParamList.Add(new SqlParameter("DutyUserNumber", DutyUserNumber));
            }
            if (DutyUserName.Trim().Length > 0)
            {
                strCondition = strCondition + " and DutyUserName like @DutyUserName";
                sqlParamList.Add(new SqlParameter("DutyUserName", DutyUserName + "%"));
            }

            strSql += strCondition + " and DutyUserNumber  not in (select top " + ((PageIndex - 1) * PageCount).ToString()
                + " DutyUserNumber from TAB_Org_DutyUser where 1=1 " + strCondition + " order by DutyUserNumber) ";
            strSql = strSql + " order by DutyUserNumber";
            SqlParameter[] sqlParams = sqlParamList.ToArray();
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DutyUser u = new DutyUser();

                u.DutyUserNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["DutyUserNumber"]);
                u.DutyUserName = TF.DB.DBConvert.ToString(dt.Rows[i]["DutyUserName"]);
                u.Password = TF.DB.DBConvert.ToString(dt.Rows[i]["Password"]);
                u.RoleID = TF.DB.DBConvert.ToString(dt.Rows[i]["RoleID"]);
                u.TokenID = TF.DB.DBConvert.ToString(dt.Rows[i]["TokenID"]);
                u.TokenTime = TF.DB.DBConvert.ToDateTime(dt.Rows[i]["TokenTime"]);
                u.RoleName = TF.DB.DBConvert.ToString(dt.Rows[i]["RoleName"]);
                result.Add(u);
            }
            return result;
        }
        public static int QueryDutyUserCount(ILog Log, SqlConnection Conn, string DutyUserNumber, string DutyUserName)
        {
            List<DutyUser> result = new List<DutyUser>();
            string strSql = @"select count(*) from TAB_Org_DutyUser where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (DutyUserNumber.Trim().Length > 0)
            {
                strCondition = strCondition + " and DutyUserNumber = @DutyUserNumber";
                sqlParamList.Add(new SqlParameter("DutyUserNumber", DutyUserNumber));
            }
            if (DutyUserName.Trim().Length > 0)
            {
                strCondition = strCondition + " and DutyUserName like @DutyUserName";
                sqlParamList.Add(new SqlParameter("DutyUserName", DutyUserName + "%"));
            }
           
            SqlParameter[] sqlParams = sqlParamList.ToArray();            
            object obj = SqlHelper.ExecuteScalar(Conn, CommandType.Text, strSql, sqlParams);
            return Convert.ToInt32(obj);
        }
        //登入
        public static bool Login(ILog Log, SqlConnection Conn, string DutyUserNumber, string Password, out string TokenID, out DateTime TokenTime)
        {
            string newTokenID = Guid.NewGuid().ToString();
            DateTime newTokenTime = DateTime.Now.AddHours(12);
            string strSql = @"update TAB_Org_DutyUser set TokenID=@TokenID,TokenTime=@TokenTime where DutyUserNumber = @DutyUserNumber and Password=@Password";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("TokenID",newTokenID),
                                           new SqlParameter("TokenTime",newTokenTime),
                                           new SqlParameter("Password",Password),
                                           new SqlParameter("DutyUserNumber",DutyUserNumber)
                                       };
            int ret = SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            if (ret > 0)
            {
                TokenID = newTokenID;
                TokenTime = newTokenTime;
                return true;
            }
            TokenID = "";
            TokenTime = DateTime.MinValue;
            return false;
        }
        //登出
        public static void Logout(ILog Log, SqlConnection Conn, string TokenID)
        {
            string newTokenID = "";
            DateTime newTokenTime = DateTime.MinValue;
            string strSql = @"update TAB_Org_DutyUser set TokeID=@TokeID,TokenTime=@TokenTime where TokenID = @TokenID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("TokeID",newTokenID),
                                           new SqlParameter("TokenTime",newTokenTime)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            
        }


        //根据TokenID获取人员信息
        public static bool GetDutyUserByTokenID(ILog Log, SqlConnection Conn, string TokenID, DutyUser ADutyUser)
        {
            string strSql = @"select * from TAB_Org_DutyUser where TokenID = @TokenID and TokenTime >=@TokenTime";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TokenID",TokenID),
                new SqlParameter("TokenTime",DateTime.Now)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ADutyUser.DutyUserNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["DutyUserNumber"]);
                ADutyUser.DutyUserName = TF.DB.DBConvert.ToString(dt.Rows[0]["DutyUserName"]);
                ADutyUser.Password = TF.DB.DBConvert.ToString(dt.Rows[0]["Password"]);
                ADutyUser.RoleID = TF.DB.DBConvert.ToString(dt.Rows[0]["RoleID"]);
                ADutyUser.TokenID = TF.DB.DBConvert.ToString(dt.Rows[0]["TokenID"]);
                ADutyUser.TokenTime = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["TokenTime"]);
                return true;
            }
            return false;
        }

        public static bool ModifyPWD(ILog Log, SqlConnection Conn, string DutyUserNumber, string OldPWD, string NewPWD)
        {
            string strSql = @"update TAB_Org_DutyUser set Password=@Password where DutyUserNumber = @DutyUserNumber and Password=@OldPassword";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("Password",NewPWD),
                                           new SqlParameter("OldPassword",OldPWD),
                                           new SqlParameter("DutyUserNumber",DutyUserNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams) > 0;
        }

        public static bool ResetPWD(ILog Log, SqlConnection Conn, string DutyUserNumber)
        {
            string strSql = @"update TAB_Org_DutyUser set Password=@Password where DutyUserNumber = @DutyUserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("Password",DutyUser.DefaultPWD),
                                           new SqlParameter("DutyUserNumber",DutyUserNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams) > 0;
        }       

    }
}
