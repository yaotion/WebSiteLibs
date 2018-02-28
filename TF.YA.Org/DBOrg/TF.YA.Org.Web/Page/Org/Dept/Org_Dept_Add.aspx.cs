using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Org
{
    public partial class Org_Dept_Add : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<DeptType> deptTypeList = TF.YA.Org.LCOrg.GetAllDeptType(WebLoader.Log, Conn);
                for (int i = 0; i < deptTypeList.Count; i++)
                {
                    DDLDeptType.Items.Add(new ListItem(deptTypeList[i].DeptTypeName, deptTypeList[i].DeptTypeID.ToString()));
                }
                //修改模式
                if (DeptID != "")
                {
                    Dept d = new Dept();
                    if (LCOrg.GetDept(WebLoader.Log, Conn, DeptID, d))
                    {
                        LblParentDept.Text = d.ParentDeptName;
                        TB_DeptID.Value = d.DeptID;
                        TB_DeptName.Text = d.DeptName;
                        DDLDeptType.SelectedIndex = DDLDeptType.Items.IndexOf(DDLDeptType.Items.FindByValue(d.DeptType.ToString()));
                    }
                    return;
                }
                //添加模式
                HDD_ParentID.Value = ParentID;

                LblParentDept.Text = "无上级部门";
                if (ParentID != "")
                {
                    TF.YA.Org.Dept dpt = new Dept();
                    if (LCOrg.GetDept(WebLoader.Log, Conn, ParentID, dpt))
                    {
                        LblParentDept.Text = dpt.DeptName;
                    }
                }
             
            }
                            
        }

        
        
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {

                Dept d = new Dept();
                if (LCOrg.GetDept(WebLoader.Log, Conn, DeptID, d))
                {
                    d.DeptName = TB_DeptName.Text;
                    d.DeptType = Int32.Parse(DDLDeptType.SelectedValue);                
                    LCOrg.UpdateDept(WebLoader.Log, Conn, d.ParentDeptID, d);
                    PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");                    
                }
                else
                {
                    d = new Dept();                                        
                    d.DeptID = TB_DeptID.Value;
                    d.DeptName = TB_DeptName.Text;
                    d.DeptType = Int32.Parse(DDLDeptType.SelectedValue);
                    d.ParentDeptID = ParentID;                                            
                    LCOrg.AddDept(WebLoader.Log, Conn, ParentID, d);                        
                    PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                }
            }
        }

        public string ParentID
        {
            get {
                if (Request["parentID"] == null)
                {
                    return "";
                }
                return Request["parentID"].ToString();
            }
        }
        public string DeptID
        {
            get {
                if (Request["deptID"] == null)
                {
                    return "";
                }
                return Request["deptID"].ToString();
            }
        }
        
    }
}