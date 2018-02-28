using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web.Base.Station.ashx
{

    public class EasyUIPage
    {
        public int total = 0;
        public object rows;
    }
    /// <summary>
    /// Station_List 的摘要说明
    /// </summary>
    public class Station_List : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strResponseJson = GetResponseJson();
            context.Response.Write(strResponseJson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetResponseJson()
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<TF.YA.Base.Station> StationList = TF.YA.Base.LCStation.QueryStation(WebLoader.Log, Conn, PageIndex, PageCount, StationName, NameJP,JLNumber,TMISNumebr);
                EasyUIPage ui = new EasyUIPage();

                ui.rows = StationList;
                ui.total = TF.YA.Base.LCStation.QueryStationCount(WebLoader.Log, Conn, StationName, NameJP, JLNumber, TMISNumebr);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ui);
            }
        }
        public string TMISNumebr
        {
            get
            {
                if (HttpContext.Current.Request["tmis"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["tmis"].ToString();
            }
        }
        public string JLNumber
        {
            get
            {
                if (HttpContext.Current.Request["jl"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["jl"].ToString();
            }
        }
        public string StationName
        {
            get
            {
                if (HttpContext.Current.Request["stationName"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["stationName"].ToString();
            }
        }
      
        public string NameJP
        {
            get
            {
                if (HttpContext.Current.Request["nameJP"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["nameJP"].ToString();
            }
        }

        public int PageIndex
        {
            get
            {
                return TF.CommonUtility.ObjectConvertClass.static_ext_int(HttpContext.Current.Request["page"]);
            }
        }
        public int PageCount
        {
            get
            {
                return TF.CommonUtility.ObjectConvertClass.static_ext_int(HttpContext.Current.Request["rows"]);
            }
        }       
    }
}