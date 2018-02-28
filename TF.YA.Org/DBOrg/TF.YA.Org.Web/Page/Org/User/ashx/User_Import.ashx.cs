using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;  
namespace TF.YA.Org.Web.Page.Org.User.ashx
{
    /// <summary>
    /// User_Import 的摘要说明
    /// </summary>
    public class User_Import : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                ExecImport();
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //string[] fields = new string[] { "UserNumber", "UserName", "NameJP", "TelNumber", "DeptFullName", "PostName" };
        //string[] shownames = new string[] { "工号", "姓名", "简拼", "电话", "部门", "职位" };
        public bool ExecImport()
        {
            if (HttpContext.Current.Request.Files.Count == 0) return false;
            HSSFWorkbook wb = new HSSFWorkbook(HttpContext.Current.Request.Files[0].InputStream);
            HSSFSheet sheet = (HSSFSheet)wb.GetSheetAt(0);
            List<TF.YA.Org.User> userList = new List<YA.Org.User>();
            //循环读取所有行的内容
            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {

                TF.YA.Org.User s = new YA.Org.User();
                //读取excel的行
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                if (row != null)
                {
                    if (i == 0) continue;
                    if (row.GetCell(0) != null)
                    {                        
                        s.UserNumber = row.GetCell(0).ToString();
                        if (s.UserNumber == "") continue;
                        s.UserName = row.GetCell(1).ToString();
                        s.NameJP = TF.CommonUtility.StrToPinyin.GetChineseSpell(s.UserName);
                        s.TelNumber = row.GetCell(3).ToString();
                        s.DeptFullName = row.GetCell(4).ToString();
                        s.PostName = row.GetCell(5).ToString();
                        userList.Add(s);
                    }
                }
            }
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    TF.YA.Org.User u = new YA.Org.User();
                    
                    //已经存在且姓名不同时覆盖
                    if (TF.YA.Org.LCUser.GetUser(WebLoader.Log, Conn, userList[i].UserNumber, u))
                    {
                        if (IsRecover > 0)
                        {
                            TF.YA.Org.Dept d = new TF.YA.Org.Dept();
                            if (TF.YA.Org.LCOrg.GetDeptByFullName(WebLoader.Log, Conn, userList[i].DeptFullName, d))
                            {
                                u.DeptID = d.DeptID;
                                u.DeptName = d.DeptName;
                            }

                            if ((u.UserName != userList[i].UserName) || (u.NameJP != userList[i].NameJP)
                                || (u.TelNumber != userList[i].TelNumber) || (u.PostName != userList[i].PostName)
                                || (u.DeptFullName != d.FullParentName))
                            {
                                u.UserName = userList[i].UserName;
                                u.NameJP = userList[i].NameJP;

                                u.TelNumber = userList[i].TelNumber;
                                u.PostID = "";
                                Post p = new Post();
                                if (TF.YA.Org.LCOrg.GetPostByName(WebLoader.Log, Conn, userList[i].PostName, p))
                                {
                                    u.PostID = p.PostID;
                                    u.PostName = p.PostName;
                                }                
                                TF.YA.Org.LCUser.UpdateUser(WebLoader.Log, Conn, u);
                            }
                        }
                    }
                    else
                    {
                        u.UserName = userList[i].UserName;
                        u.NameJP = userList[i].NameJP;
                        u.TelNumber = userList[i].TelNumber;
                        u.PostID = "";
                        Post p = new Post();
                        if (TF.YA.Org.LCOrg.GetPostByName(WebLoader.Log, Conn, userList[i].PostName, p))
                        {
                            u.PostID = p.PostID;
                            u.PostName = p.PostName;
                        }
                        TF.YA.Org.Dept d = new TF.YA.Org.Dept();
                        if (TF.YA.Org.LCOrg.GetDeptByFullName(WebLoader.Log, Conn, userList[i].DeptFullName, d))
                        {
                            u.DeptID = d.DeptID;
                            u.DeptName = d.DeptName;
                        }
                        TF.YA.Org.LCUser.AddUser(WebLoader.Log, Conn, userList[i]);
                    }
                }
            }
            return true;
        }
        public int IsRecover
        {
            get{
                if (HttpContext.Current.Request["r"] == null)
                {
                    return 0;
                }
                return TF.DB.DBConvert.ToInt32(HttpContext.Current.Request["r"]);
            }
            
        }
    }
}