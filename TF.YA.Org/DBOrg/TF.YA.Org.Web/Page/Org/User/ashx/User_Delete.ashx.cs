using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.User.ashx
{
    /// <summary>
    /// User_Delete 的摘要说明
    /// </summary>
    public class User_Delete : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";
                LCUser.DeleteUser(WebLoader.Log, Conn, UserNumber);
                context.Response.Write("true");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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
    }
}