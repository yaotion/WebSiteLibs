using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Page.Org.DutyUser.ashx
{
    /// <summary>
    /// DutyUserPost_List 的摘要说明
    /// </summary>
    public class DutyUserPost_List : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string orgJson = GetJsonString();
            context.Response.Write(orgJson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string GetJsonString()
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<DutyUserPost> PostList = LCDutyUser.GetAllDutyPosts(WebLoader.Log, Conn);
                return Newtonsoft.Json.JsonConvert.SerializeObject(PostList);
            }
        }
    }
}