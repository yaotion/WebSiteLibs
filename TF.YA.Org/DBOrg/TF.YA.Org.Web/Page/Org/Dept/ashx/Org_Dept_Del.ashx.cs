using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Org.ashx
{
    /// <summary>
    /// Org_Dept_Del 的摘要说明
    /// </summary>
    public class Org_Dept_Del : IHttpHandler
    {        
        public void ProcessRequest(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                context.Response.ContentType = "text/plain";
                try
                {
                    LCOrg.DeleteDept(WebLoader.Log, Conn, DeptID);

                    context.Response.Write("true");
                }
                catch (Exception e)
                {
                    context.Response.Write("false");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
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
    }
}