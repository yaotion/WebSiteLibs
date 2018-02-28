using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.WebPlatForm.Logic;
using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;

namespace TF.YA.Org
{
    public class LCDutyUser
    {
        public static void DeleteDutyPost(ILog Log, SqlConnection Conn, int PostTypeID)
        {
            DBDutyUser.DeleteDutyPost(Log,Conn,PostTypeID);
        }
        public static List<DutyUserPost> GetAllDutyPosts(ILog Log, SqlConnection Conn)
        {
            return DBDutyUser.GetAllDutyPosts(Log,Conn);
        }
        public static void AddDutyPost(ILog Log, SqlConnection Conn, DutyUserPost PT)
        {
            DBDutyUser.AddDutyPost(Log,Conn,PT);
        }
        public static void AddDutyUser(ILog Log, SqlConnection Conn, DutyUser ADutyUser)
        {
            DBDutyUser.AddDutyUser(Log,Conn,ADutyUser);
        }
        public static void UpdateDutyUser(ILog Log, SqlConnection Conn, DutyUser ADutyUser)
        {
            DBDutyUser.UpdateDutyUser(Log,Conn,ADutyUser);
        }
        public static void DeleteDutyUser(ILog Log, SqlConnection Conn, string DutyUserNumber)
        {
            DBDutyUser.DeleteDutyUser(Log, Conn, DutyUserNumber);
        }
        public static bool GetDutyUser(ILog Log, SqlConnection Conn, string DutyUserNumber, DutyUser ADutyUser)
        {
            return DBDutyUser.GetDutyUser(Log, Conn, DutyUserNumber, ADutyUser);
        }
        public static List<DutyUser> QueryDutyUser(ILog Log, SqlConnection Conn, int PageIndex, int PageCount, string DutyUserNumber, string DutyUserName)
        {
            return DBDutyUser.QueryDutyUser(Log, Conn, PageIndex, PageCount, DutyUserNumber, DutyUserName);
        }

        public static int QueryDutyUserCount(ILog Log, SqlConnection Conn, string DutyUserNumber, string DutyUserName)
        {
            return DBDutyUser.QueryDutyUserCount(Log, Conn, DutyUserNumber, DutyUserName);
        }

        public static bool ResetPWD(ILog Log, SqlConnection Conn, string DutyUserNumber)
        {
            return DBDutyUser.ResetPWD(Log, Conn, DutyUserNumber);
        }

        public static bool GetDutyUserByTokenID(ILog Log, SqlConnection Conn, string TokenID, DutyUser ADutyUser)
        {

            return DBDutyUser.GetDutyUserByTokenID(Log,Conn,TokenID,ADutyUser);
        }
        public static bool Login(ILog Log, SqlConnection Conn, string DutyUserNumber, string Password, out string TokenID, out DateTime TokenTime)
        {
            return DBDutyUser.Login(Log,Conn,DutyUserNumber,Password,out TokenID,out TokenTime);
        }
        public static bool ModifyPWD(ILog Log, SqlConnection Conn, string DutyUserNumber, string OldPWD, string NewPWD)
        {
            return DBDutyUser.ModifyPWD(Log, Conn, DutyUserNumber, OldPWD,  NewPWD);
        }

    }
    public class LCDutyLogin : IUserInfo
    {
       
        private ILog logger = Common.Logging.LogManager.GetLogger("LCDutyLogin");
        public WebPlatForm.Entry.userInfo GetUserInfoByTokenID(string strTokenID)
        {
            WebPlatForm.Entry.userInfo result = new WebPlatForm.Entry.userInfo();
            using(SqlConnection Conn = new SqlConnection(TF.WebPlatForm.Logic.ConData.WebSiteConnectionString))
            {
                DutyUser u =new DutyUser();
                if (!LCDutyUser.GetDutyUserByTokenID(logger, Conn, strTokenID, u))
                {
                    result.strPassword = "";
                    result.strRoleID = "";
                    result.strTrianmanName = "";
                    result.strTrianmanNumber = "";
                    return result;
                }
                result.strPassword = "";
                result.strRoleID = u.RoleID;
                result.strTrianmanName = u.DutyUserName;
                result.strTrianmanNumber = u.DutyUserNumber;
                return result;
            }
            
        }

        public bool IsLogin(string strTokenID)
        {
            using (SqlConnection Conn = new SqlConnection(TF.WebPlatForm.Logic.ConData.WebSiteConnectionString))
            {
                DutyUser u = new DutyUser();
                if (!LCDutyUser.GetDutyUserByTokenID(logger, Conn, strTokenID, u))
                {
                    return false;
                }
                return true;
            }
        }
        private class LoginReq
        {
            //用户名
            public string name;
            //密码
            public string pwd;
        }
        public WebPlatForm.Entry.loginReplay Login(string loginInfo)
        {
            LoginReq req = (LoginReq)Newtonsoft.Json.JsonConvert.DeserializeObject<LoginReq>(loginInfo);
            
            WebPlatForm.Entry.loginReplay result = new WebPlatForm.Entry.loginReplay();
            if (req == null)
            {
                result.nSuccess = 0;
                result.strResult= "错误的数据格式";
                result.tokenID = "";
                return result;
            }

            using (SqlConnection Conn = new SqlConnection(TF.WebPlatForm.Logic.ConData.WebSiteConnectionString))
            {
                string TokenID;
                DateTime TokenTime;
                DutyUser u = new DutyUser();
                if (!LCDutyUser.Login(logger, Conn, req.name, req.pwd, out TokenID, out TokenTime))
                {
                    result.nSuccess = 0;
                    result.strResult = "用户名或密码不正确";
                    result.tokenID = "";
                    return result;

                }
                result.nSuccess = 1;
                result.strResult = "登录成功";
                result.tokenID = TokenID;
                return result;
            }
        }

        public bool SetPassword(string strTrainmanNumber, string strOldPwd, string strNewPwd)
        {
            
            using (SqlConnection Conn = new SqlConnection(TF.WebPlatForm.Logic.ConData.WebSiteConnectionString))
            {
                return LCDutyUser.ModifyPWD(logger, Conn, strTrainmanNumber, strOldPwd, strNewPwd);               
            }
        }

      
    }
}
