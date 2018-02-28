using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Page.Org.Dept
{
    public partial class Org_Dept_Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Org.Dept d = new TF.YA.Org.Dept();
                if (!LCOrg.GetDept(WebLoader.Log, Conn, DeptID, d))
                {
                    return ;
                }
                Lbl_DeptName.Text = d.DeptName;
                TF.YA.Org.DeptType dt = new DeptType();
                if (!LCOrg.GetDeptType(WebLoader.Log, Conn, d.DeptType, dt))
                {
                    return;
                }
                string strDS = dt.DeptTypeDS;
                Dictionary<string, object> ds = (Dictionary<string, object>)Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(strDS);
                DeptData deptData = (DeptData)Newtonsoft.Json.JsonConvert.DeserializeObject<DeptData>(d.DeptData);
                if (ds == null)
                    return ;
                foreach (var item in ds)
                {
                    Label lbl = new Label();
                    lbl.Text = item.Value.ToString() + ":";
                    lbl.ID = "lbl" + item.Key;
                    lbl.Width = 80;
                    lbl.Style.Add("text-align","right");
                    Div_DeptData.Controls.Add(lbl);


                    TextBox tb = new TextBox();
                    tb.Attributes["Key"] = item.Key;
                    tb.ID = "tb" + item.Key;
                    tb.Width = 200;
                    tb.Height = 13;
                    if (!IsPostBack && deptData != null)
                    {
                        tb.Text = TF.DB.DBConvert.ToString(deptData.datas[item.Key]);
                    }
                    Div_DeptData.Controls.Add(tb);
                    Div_DeptData.Controls.Add(Page.ParseControl("<br>"));            
                }                
            }
        }

     
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Org.Dept d = new TF.YA.Org.Dept();
                if (!LCOrg.GetDept(WebLoader.Log, Conn, DeptID, d))
                {
                    PageBase.static_Message_ext(this, "alert('错误的部门编号');");   
                    return;
                }
                Lbl_DeptName.Text = d.DeptName;
                TF.YA.Org.DeptType dt = new DeptType();
                if (!LCOrg.GetDeptType(WebLoader.Log, Conn, d.DeptType, dt))
                {
                    PageBase.static_Message_ext(this, "alert('错误的部门类型');");   
                    return;
                }
                string strDS = dt.DeptTypeDS;
                Dictionary<string, object> ds = (Dictionary<string, object>)Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(strDS);
                if (ds == null)
                {
                    PageBase.static_Message_ext(this, "alert('未指定部门类型的数据格式');");
                    return;
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var item in ds)
                {
                    TextBox tb = (TextBox)Div_DeptData.FindControl("tb" + item.Key);
                    if (tb != null)
                    {
                        data.Add(item.Key,tb.Text);
                    }
                }
                DeptData dd = new DeptData();
                dd.columns = ds;
                dd.datas = data;
                LCOrg.UpdateDeptData(WebLoader.Log, Conn, d.DeptID, Newtonsoft.Json.JsonConvert.SerializeObject(dd));
                PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
            }
        }
       
        public string DeptID
        {
            get
            {
                if (Request["deptID"] == null)
                {
                    return "";
                }
                return Request["deptID"].ToString();
            }
        }
    }
}