using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
          
namespace TF.YA.Statistics.Web.Page.ashx
{
    /// <summary>
    /// BJ_List 的摘要说明
    /// </summary>
    public class BJ_List : IHttpHandler
    {

        public class EasyUIPage
        {
            public int total = 0;
            public object rows;
        }
        public class DataItem
        {
            public int id;
            public string train;
            public string bjItem;
            public string bjTime;
            public string bjSection;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orgJson = GetResponseJson();
            context.Response.Write(orgJson);
        }
        public string GetResponseJson()
        {
            EasyUIPage ui = new EasyUIPage();
            List<DataItem> d = new List<DataItem>();
            DataItem di = new DataItem();
            di.id = 1;
            di.bjItem = "车况报警";
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-10).ToString("HH:mm");
            di.train = "0512";
            d.Add(di);

            di = new DataItem();
            di.id = 2;
            di.bjItem = "值乘报警";
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-12).ToString("HH:mm");
            di.train = "0154";
            d.Add(di);

            di = new DataItem();
            di.bjItem = "LKJ报警";
            di.id = 3;
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-14).ToString(" HH:mm");
            di.train = "2148";
            d.Add(di);

            di = new DataItem();
            di.bjItem = "系统报警";
            di.id = 4;
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-16).ToString("HH:mm");
            di.train = "1478";
            d.Add(di);

            di = new DataItem();
            di.bjItem = "车况报警";
            di.id = 5;
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-18).ToString("HH:mm");
            di.train = "2049";
            d.Add(di);

            di = new DataItem();
            di.bjItem = "车况报警";
            di.id = 6;
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-18).ToString("HH:mm");
            di.train = "2049";
            d.Add(di);


            di = new DataItem();
            di.bjItem = "车况报警";
            di.id = 7;
            di.bjSection = "包西";
            di.bjTime = DateTime.Now.AddMinutes(-18).ToString("HH:mm");
            di.train = "2049";
            d.Add(di);


          
            ui.rows = d;
            ui.total = 7;
            return Newtonsoft.Json.JsonConvert.SerializeObject(ui);
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