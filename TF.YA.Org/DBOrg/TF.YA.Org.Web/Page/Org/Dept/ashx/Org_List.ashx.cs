using System;
using System.Web;
using System.Collections.Generic;

namespace TF.YA.Org.Web.Org.ashx
{
    public class JsonTreeItem

    {
        public string id;
 
        public string name;
        public string state = "open";
        public string DeptTypeName;
        public string DeptData;
        public string FullParentName;
       
    }
    public class JsonTreeLeft : JsonTreeItem
    {
        public string _parentId;
    }
    public class JsonTree
    {
        public int total = 1;
        public List<JsonTreeItem> rows = new List<JsonTreeItem>();
    }
    /// <summary>
    /// Org_List 的摘要说明
    /// </summary>
    public class Org_List : IHttpHandler
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

        public string GetDeptTypeName(int DeptTypeID,List<DeptType> DeptTypeList)
        {
            for (int i = 0; i < DeptTypeList.Count; i++)
            {
                if (DeptTypeList[i].DeptTypeID == DeptTypeID)
                {
                    return DeptTypeList[i].DeptTypeName;
                }                
            }
            return "-";
        }
        public string TransDeptData(string DeptData)
        {
            if (DeptData == "") return "";
            DeptData dd = (DeptData)Newtonsoft.Json.JsonConvert.DeserializeObject<DeptData>(DeptData);
            if (dd == null)
                return "";
            string result = "";
            foreach (var item in dd.columns)
            {
                result += item.Value + ":" + dd.datas[item.Key].ToString() + ";";
            }	
		    return result;
        }
         

  
        public string GetOrgJson()
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<Dept> DeptList = TF.YA.Org.LCOrg.GetAllDepts(WebLoader.Log, Conn);
                List<DeptType> deptTypeList = LCOrg.GetAllDeptType(WebLoader.Log, Conn);

                JsonTree tree = new JsonTree();
                for (int i = 0; i < DeptList.Count; i++)
                {
                    if (DeptList[i].ParentDeptID == "")
                    {
                        JsonTreeItem item = new JsonTreeItem();
                        item.id = DeptList[i].DeptID;
                        item.name = DeptList[i].DeptName;
                        item.DeptTypeName = GetDeptTypeName(DeptList[i].DeptType, deptTypeList);
                        item.FullParentName = DeptList[i].FullParentName;
                        item.DeptData = TransDeptData(DeptList[i].DeptData);
                        tree.rows.Add(item);
                    }
                    else
                    {
                        JsonTreeLeft item = new JsonTreeLeft();
                        item.id = DeptList[i].DeptID;
                        item._parentId = DeptList[i].ParentDeptID;
                        item.name = DeptList[i].DeptName;
                        item.DeptTypeName = GetDeptTypeName(DeptList[i].DeptType, deptTypeList);
                        item.FullParentName = DeptList[i].FullParentName;
                        item.DeptData = TransDeptData(DeptList[i].DeptData);
                        tree.rows.Add(item);
                    }
                    
                }

                tree.total = tree.rows.Count;
                return Newtonsoft.Json.JsonConvert.SerializeObject(tree);
            }
        }
    }
}