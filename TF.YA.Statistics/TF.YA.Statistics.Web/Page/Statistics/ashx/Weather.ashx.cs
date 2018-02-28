using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    
    /// <summary>
    /// Weather 的摘要说明
    /// </summary>
    public class Weather : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            TF.YA.Statistics.Weather w = new TF.YA.Statistics.Weather();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Statistics.LCWeather.GetWeather(WebLoader.Log, Conn,"东胜",DateTime.Now,w);
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