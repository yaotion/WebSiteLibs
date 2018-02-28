using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web.Base.Station.ashx
{
    /// <summary>
    /// Station_Delete 的摘要说明
    /// </summary>
    public class Station_Delete : IHttpHandler
    {        
        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";
                TF.YA.Base.LCStation.DeleteStation(WebLoader.Log, Conn, StationName);
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
        public string StationName
        {
            get
            {
                return TF.DB.DBConvert.ToString(HttpContext.Current.Request["sname"]);
            }
        }
    }
}