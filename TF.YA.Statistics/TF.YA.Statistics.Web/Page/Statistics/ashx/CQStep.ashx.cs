using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    public class StepData
    {
        public string FlowName = "";
        public string PageUrl = "http://192.168.1.166:20006/Page/Attendance/StepForDataBoard.aspx";
    }
    /// <summary>
    /// CQStep 的摘要说明
    /// </summary>
    public class CQStep : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            StepData d = new StepData();            
            return Newtonsoft.Json.JsonConvert.SerializeObject(d);
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