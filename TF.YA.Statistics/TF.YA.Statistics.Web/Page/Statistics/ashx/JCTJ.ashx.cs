using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Statistics.Web.Page.ashx
{
    public class JCTJDataItem
    {
        public List<string> dates = new List<string>();
        public List<object> datas = new List<object>();
    }
    public class JCTJData
    {
        public JCTJDataItem tr = new JCTJDataItem();
        public JCTJDataItem zx = new JCTJDataItem();
        public JCTJDataItem zz = new JCTJDataItem();
        public JCTJDataItem sd = new JCTJDataItem();
        public JCTJDataItem cl = new JCTJDataItem();
    }
    /// <summary>
    /// JCTJ 的摘要说明
    /// </summary>
    public class JCTJ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string strJson = GetResponseJson();
            context.Response.Write(strJson);
        }
        public string GetResponseJson()
        {

            JCTJData datas = new JCTJData();
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<TrainTJ> tjList = TF.YA.Statistics.LCTrainTJ.QueryTJ(WebLoader.Log, Conn,DateTime.Now.AddDays(-9),DateTime.Now,"1001");
                for (int i = 0; i < tjList.Count; i++)
                {
                    datas.tr.dates.Add(tjList[i].TJDay.ToString("MM.dd"));
                    datas.tr.datas.Add(tjList[i].TR);

                    datas.zx.dates.Add(tjList[i].TJDay.ToString("MM.dd"));
                    datas.zx.datas.Add(tjList[i].ZX);

                    datas.zz.dates.Add(tjList[i].TJDay.ToString("MM.dd"));
                    datas.zz.datas.Add(tjList[i].ZZ);

                    datas.sd.dates.Add(tjList[i].TJDay.ToString("MM.dd"));
                    datas.sd.datas.Add(tjList[i].SD);


                    datas.cl.dates.Add(tjList[i].TJDay.ToString("MM.dd"));
                    datas.cl.datas.Add(tjList[i].CL);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);

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