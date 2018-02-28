using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using TF.WebPlatForm.DBUtils;
using TF.WebPlatForm.Entry;
using System.Data;
using System.IO;
namespace TF.WebPlatForm.Logic
{
    /// <summary>
    ///上传日志
    /// </summary>
    public class PostLog
    {
        /// <summary>
        /// 保存日志
        /// </summary>
        public static void SaveLog(int nType, string strPageUrl, string strPageName)
        {
            try
            {
                Dat_WebLog model = new Dat_WebLog();
                model.strTrianManNumber = UserInformation.LoginUser.strTrianmanNumber;
                model.dtPostTime = DateTime.Now;
                model.nType = nType;
                model.strPageUrl = strPageUrl;
                model.strPageName = strPageName;
                model.strClientIP = System.Web.HttpContext.Current.Request.UserHostAddress;
                DBDat_WebLog dal = new DBDat_WebLog(ConData.WebSiteConnectionString);
                dal.Add(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 日志（字符串）
        /// </summary>
        /// <param name="_str"></param>
        public static void log(string _str,string dir)
        {
            string directory = "~/"+dir+"/" + DateTime.Now.ToString("yyyy-MM") + "/";
            string filepath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string logFile = System.Web.HttpContext.Current.Server.MapPath(filepath);
            StreamWriter sw = null;
            try
            {
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
                }
                if (File.Exists(logFile))
                {
                    sw = File.AppendText(logFile);
                }
                else
                {
                    sw = File.CreateText(logFile);
                }
                sw.WriteLine(System.DateTime.Now.ToString() + "   IP:" + System.Web.HttpContext.Current.Request.UserHostAddress);
                sw.WriteLine(_str);
                sw.WriteLine("---------------------------------------------------------------------");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch (Exception)
            {
            }
        }

    }
}
