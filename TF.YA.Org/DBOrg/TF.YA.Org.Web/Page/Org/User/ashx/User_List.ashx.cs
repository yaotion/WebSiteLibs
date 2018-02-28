using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.YA.Org;

namespace TF.YA.Org.Web
{
    public class  EasyUIPage
    {
        public int total = 0;
        public object rows;
    }

    /// <summary>
    /// User_List 的摘要说明
    /// </summary>
    public class User_List : IHttpHandler
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
                List<TF.YA.Org.User> UserList = TF.YA.Org.LCUser.QueryUser(WebLoader.Log, Conn, PageIndex, PageCount, DeptID, UserNumber, UserName);
                EasyUIPage ui = new EasyUIPage();
                for (int i = 0; i < UserList.Count; i++)
                {
                    UserList[i].DeptFullName = UserList[i].ReorderFullName();
                }
                ui.rows = UserList;
                ui.total = TF.YA.Org.LCUser.QueryUserCount(WebLoader.Log, Conn, DeptID, UserNumber, UserName);
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
        public string DeptID
        {
            get
            {
                if (HttpContext.Current.Request["deptID"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["deptID"].ToString();
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
            get{
                return TF.CommonUtility.ObjectConvertClass.static_ext_int(HttpContext.Current.Request["page"]);
            }
        }
        public int PageCount
        {
            get{
                return TF.CommonUtility.ObjectConvertClass.static_ext_int(HttpContext.Current.Request["rows"]);
            }
        }         
    }
}