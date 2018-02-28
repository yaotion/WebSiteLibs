using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{

    /// <summary>
    /// CTQ 的摘要说明
    /// </summary>
    public class CTQ : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            Plan pSum = new Plan();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                LCPlan.GetPlanSum(WebLoader.Log, Conn, DateTime.Now.Date, pSum);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(pSum);
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