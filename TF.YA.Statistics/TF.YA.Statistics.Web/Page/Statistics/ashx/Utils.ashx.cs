using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.Statistics.ashx
{
    /// <summary>
    /// Utils 的摘要说明
    /// </summary>
    public class Utils : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strResponse = "";
            if (MethodName == "getlcnow")
            {
                strResponse = TF.YA.Statistics.StaticUtils.GetNowDateString();
            }
            context.Response.Write(strResponse);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string MethodName
        {
            get
            {
                if (HttpContext.Current.Request["m"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["m"].ToString();
            }
        }
    }
}