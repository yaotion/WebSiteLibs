using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    public class JsonSubClass
    {
        public string name = "";
        public List<Int32> data = new List<int>();
    }
    public class JsonClass
    {
        public List<string> countDates = new List<string>();
        public List<JsonSubClass> countDatas = new List<JsonSubClass>();
    }
    /// <summary>
    /// CTQCount 的摘要说明
    /// </summary>
    public class CTQCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {
            JsonClass jc = new JsonClass();

          
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<Plan> planList = LCPlan.GetPlanSum(WebLoader.Log, Conn, DateTime.Now.AddDays(-9),DateTime.Now.Date);
                JsonSubClass cq = new JsonSubClass();
                cq.name = "出勤";

                JsonSubClass tq = new JsonSubClass();
                tq.name = "退勤";

                for (int i = 0; i < planList.Count; i++)
                {
                    jc.countDates.Add(planList[i].PlanDay.ToString("MM.d"));
                    cq.data.Add(planList[i].CQ);
                    tq.data.Add(planList[i].TQ);
                }
                jc.countDatas.Add(cq);
                jc.countDatas.Add(tq);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(jc);
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