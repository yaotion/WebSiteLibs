using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    public class RespConData
    {
        public int HasData = 0;
        public TF.YA.Statistics.ConData Data = new TF.YA.Statistics.ConData();
    }
    /// <summary>
    /// ConData 的摘要说明
    /// </summary>
    public class ConData : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            RespConData d = new RespConData();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                d.HasData = 0;
                if (TF.YA.Statistics.LCConData.GetData(WebLoader.Log, Conn, SortID, ItemID, d.Data))
                {
                    d.HasData = 1;
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(d);

        }
        public string SortID
        {
            get
            {
                if (HttpContext.Current.Request["sid"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["sid"].ToString();
            }
        }
        public string ItemID
        {
            get
            {
                if (HttpContext.Current.Request["iid"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["iid"].ToString();
            }
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