using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Org.Web.Org.ashx
{
    /// <summary>
    /// Post_List 的摘要说明
    /// </summary>
    public class Post_List : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orgJson = GetOrgJson();
            context.Response.Write(orgJson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetOrgJson()
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<Post> PostList = TF.YA.Org.LCOrg.GetAllPosts(WebLoader.Log, Conn);                                
                return Newtonsoft.Json.JsonConvert.SerializeObject(PostList);
               
            }
        }
    }
}