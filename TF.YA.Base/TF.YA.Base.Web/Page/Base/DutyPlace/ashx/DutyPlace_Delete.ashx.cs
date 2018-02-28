using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web.Base.DutyPlace
{
    /// <summary>
    /// DutyPlace_Delete 的摘要说明
    /// </summary>
    public class DutyPlace_Delete : IHttpHandler
    {        
        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";

                LCDutyPlace.DeletePlace(WebLoader.Log, Conn, PlaceID);
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
        public string PlaceID
        {
            get
            {
                if (HttpContext.Current.Request["placeID"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["placeID"].ToString();
            }
        }
    }
}