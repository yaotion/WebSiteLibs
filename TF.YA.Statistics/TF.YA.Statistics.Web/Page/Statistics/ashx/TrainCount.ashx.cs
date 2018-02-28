using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    /// <summary>
    /// TrainCount 的摘要说明
    /// </summary>
    public class TrainCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
                    
            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            TF.YA.Statistics.TrainCount w = new TF.YA.Statistics.TrainCount();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Statistics.LCTrainCount.GetTrainCount(WebLoader.Log, Conn, "1001", w);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(w);
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