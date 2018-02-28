using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Page.Org.DutyUser.ashx
{
    /// <summary>
    /// DutyUser_Delete 的摘要说明
    /// </summary>
    public class DutyUser_Delete : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                LCDutyUser.DeleteDutyUser(WebLoader.Log, Conn, UserNumber);
            }
            context.Response.Write("true");
        }
        public string UserNumber
        {
            get
            {
                if (HttpContext.Current.Request["userNumber"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["userNumber"].ToString();
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