using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web.Page.Base.ICSection
{
    /// <summary>
    /// ICSection_Delete 的摘要说明
    /// </summary>
    public class ICSection_Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";

                LCICSection.DeleteSection(WebLoader.Log, Conn, JWDNumber,SectionNumber);
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

        public string JWDNumber
        {
            get
            {
                if (HttpContext.Current.Request["JWDNumber"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["JWDNumber"].ToString();
            }
        }

        public int SectionNumber
        {
            get
            {
                if (HttpContext.Current.Request["SectionNumber"] == null)
                {
                    return 0;
                }
                return TF.DB.DBConvert.ToInt32(HttpContext.Current.Request["SectionNumber"]);
            }
        }
    }
}