using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    public class AQTJData
    {
        public List<List<object>> yjData = new List<List<object>>();
    }
    /// <summary>
    /// AQTJ 的摘要说明
    /// </summary>
    public class AQTJ : IHttpHandler
    {
        //[['车况报警', 45], ['值乘报警', 26], ['LKJ报警', 8], ['系统报警', 6]]

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            
            AQTJData d = new AQTJData();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<TrainBJ> bjList =  TF.YA.Statistics.LCTrainBJ.GetBJSum(WebLoader.Log, Conn,DateTime.Now,DateTime.Now);
                for (int i = 0; i < bjList.Count; i++)
                {                    
                    List<object> item = new List<object>();
                    item.Add(bjList[i].BJItenName);
                    item.Add(bjList[i].BJCount);
                    d.yjData.Add(item);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(d.yjData);
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