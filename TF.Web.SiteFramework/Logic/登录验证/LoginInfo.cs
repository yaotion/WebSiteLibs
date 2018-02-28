
using System;
using System.Data;
using System.Configuration;
using System.Web;
using TF.CommonUtility;
using TF.WebPlatForm.DBUtils;
using TF.WebPlatForm.Entry;
using System.Reflection;

namespace TF.WebPlatForm.Logic
{
    public class UserInformation
    {
        //定义操作员信息在Cookie中的目录
        public static string CookieName = "Platform" + XmlClass.Read("XmlConfig.xml", "/XmlConfig/SystemBase/SitePort");
        public static string ItemName = "UserLogin";
        public static long ValidSecond = 60 * 60 * 24;

        /// <summary> 返回登录用户的信息
        /// </summary>
        public static userInfo LoginUser
        {
            get
            {
                string tokenID = Cookie.ReadCookie(CookieName, ItemName);
                userInfo ui = new userInfo();
                if ((tokenID == null) || (tokenID.Length == 0))
                {
                    return ui;
                }
                System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + XmlClass.Read("SysConfig.xml", "/SysConfig/login/dllName")); //加载DLL
                System.Type t = ass.GetType(XmlClass.Read("SysConfig.xml", "/SysConfig/login/ClassName"));
                IUserInfo dal = (IUserInfo)System.Activator.CreateInstance(t);
                ui = dal.GetUserInfoByTokenID(tokenID);
                return ui;
            }
        }

        /// <summary> 返回登录用户的信息
        /// </summary>
        public static string strTokenID
        {
            get
            {
                string tokenID = Cookie.ReadCookie(CookieName, ItemName);
                return tokenID;
            }
        }

        /// <summary> 判断是否登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                string tokenID = Cookie.ReadCookie(CookieName, ItemName);
                if ((tokenID == null) || (tokenID.Length == 0)) return false;
                System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + XmlClass.Read("SysConfig.xml", "/SysConfig/login/dllName")); //加载DLL
                System.Type t = ass.GetType(XmlClass.Read("SysConfig.xml", "/SysConfig/login/ClassName"));
                IUserInfo dal = (IUserInfo)System.Activator.CreateInstance(t);
                return dal.IsLogin(tokenID);
            }
        }
        /// <summary> 重设密码
        /// </summary>
        public bool SetPassword(string strTrainmanNumber,string strOldPwd,string strNewPwd)
        {
                string tokenID = Cookie.ReadCookie(CookieName, ItemName);
                if ((tokenID == null) || (tokenID.Length == 0)) return false;
                System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + XmlClass.Read("SysConfig.xml", "/SysConfig/login/dllName")); //加载DLL
                System.Type t = ass.GetType(XmlClass.Read("SysConfig.xml", "/SysConfig/login/ClassName"));
                IUserInfo dal = (IUserInfo)System.Activator.CreateInstance(t);
                return dal.SetPassword(strTrainmanNumber,strOldPwd,strNewPwd);
        }
    }
}
