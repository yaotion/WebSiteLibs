using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Page.Org.User.ashx
{
    /// <summary>
    /// User_Export 的摘要说明
    /// </summary>
    public class User_Export : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<TF.YA.Org.User> users = TF.YA.Org.LCUser.QueryUser(WebLoader.Log, Conn, 1, 100000, "", "", "");
                string[] fields = new string[] { "UserNumber", "UserName", "NameJP", "TelNumber", "DeptFullName","PostName" };
                string[] shownames = new string[] { "工号", "姓名", "简拼","电话","部门","职位" };
                TF.YA.Org.Web.ExcelHelperForIList<TF.YA.Org.User>.CreateAdvExcel(users, DateTime.Now.ToString("人员信息yyyyMMddHHmmss"), fields, shownames);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}