using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Page.Org.DutyUser.ashx
{
    /// <summary>
    /// DutyUserPost_Delete 的摘要说明
    /// </summary>
    public class DutyUserPost_Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                LCDutyUser.DeleteDutyPost(WebLoader.Log, Conn, PostTypeID);
            }
            context.Response.Write("true");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public int PostTypeID
        {
            get
            {
                if (HttpContext.Current.Request["postTypeID"] == null)
                {
                    return 0;
                }
                return TF.DB.DBConvert.ToInt32(HttpContext.Current.Request["postTypeID"]);
            }
        }
    }
}