using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Org.ashx
{
    /// <summary>
    /// Org_Post_Del 的摘要说明
    /// </summary>
    public class Org_Post_Del : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";
                LCOrg.DeletePost(WebLoader.Log, Conn, PostID);
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
        public string PostID
        {
            get
            {
                if (HttpContext.Current.Request["postID"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["postID"].ToString();
            }
        }
    }
}