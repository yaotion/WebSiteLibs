using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Page.Org.DutyUser.ashx
{

    public class EasyUIPage
    {
        public int total = 0;
        public object rows;
    }
    /// <summary>
    /// DutyUser_List 的摘要说明
    /// </summary>
    public class DutyUser_List : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orgJson = GetResponseJson();
            context.Response.Write(orgJson);
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
                List<TF.YA.Org.DutyUser> UserList = TF.YA.Org.LCDutyUser.QueryDutyUser(WebLoader.Log, Conn, PageIndex, PageCount,  UserNumber, UserName);
                EasyUIPage ui = new EasyUIPage();
              
                ui.rows = UserList;
                ui.total = TF.YA.Org.LCDutyUser.QueryDutyUserCount(WebLoader.Log, Conn,  UserNumber, UserName);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ui);
            }
        }

        public string UserNumber
        {
            get
            {
                if (HttpContext.Current.Request["userNumber"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["userNumber"].ToString();
            }
        }
       
        public string UserName
        {
            get
            {
                if (HttpContext.Current.Request["userName"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["userName"].ToString();
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