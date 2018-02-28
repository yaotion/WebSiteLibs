using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web.Page.Base.Station.ashx
{
    /// <summary>
    /// Station_Export 的摘要说明
    /// </summary>
    public class Station_Export : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<TF.YA.Base.Station> stations = TF.YA.Base.LCStation.QueryStation(WebLoader.Log, Conn, 1, 100000, "", "","","");
                string[] fields = new string[] { "StationName", "NameJP", "StationNumber"};
                string[] shownames = new string[] { "车站名", "简拼",  "车站号码" };
                TF.YA.Base.Web.ExcelHelperForIList<TF.YA.Base.Station>.CreateAdvExcel(stations, DateTime.Now.ToString("车站信息yyyyMMddHHmmss"), fields, shownames);
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