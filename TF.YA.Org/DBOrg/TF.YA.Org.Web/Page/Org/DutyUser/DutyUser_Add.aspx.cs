using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TF.YA.Org.Web.Page.Org.DutyUser
{
    public partial class DutyUser_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.WebPlatForm.Logic.RolePower r = new WebPlatForm.Logic.RolePower();
                DataTable dtRole = r.GetRole();
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
			         DDL_Role.Items.Add(new ListItem(dtRole.Rows[i]["strName"].ToString(),dtRole.Rows[i]["strID"].ToString()));
                }
              
                if (UserNumber != "")
                {
                    YA.Org.DutyUser u = new YA.Org.DutyUser();
                    YA.Org.LCDutyUser.GetDutyUser(WebLoader.Log, Conn, UserNumber, u);
                    TB_UserNumber.Value = u.DutyUserNumber;
                    TB_UserName.Value = u.DutyUserName;
                    DDL_Role.SelectedIndex = DDL_Role.Items.IndexOf(DDL_Role.Items.FindByValue(u.RoleID));
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Org.DutyUser u = new TF.YA.Org.DutyUser();
                if (UserNumber != "")
                {
                    TF.YA.Org.LCDutyUser.GetDutyUser(WebLoader.Log, Conn, UserNumber, u);
                    u.RoleID = DDL_Role.SelectedValue;
                    u.RoleName = DDL_Role.SelectedItem.Text;
                    u.DutyUserName = TB_UserName.Value;
                }
                else
                {
                    u.RoleID = DDL_Role.SelectedValue;
                    u.RoleName = DDL_Role.SelectedItem.Text;
                    u.DutyUserNumber = TB_UserNumber.Value;
                    u.DutyUserName = TB_UserName.Value;
                    u.Password = TF.YA.Org.DutyUser.DefaultPWD;
                }
               
                if (UserNumber == "")
                    TF.YA.Org.LCDutyUser.AddDutyUser(WebLoader.Log, Conn, u);
                else
                    TF.YA.Org.LCDutyUser.UpdateDutyUser(WebLoader.Log, Conn, u);

                PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
            }
        }
        public string UserNumber
        {
            get
            {
                if (Request["userNumber"] == null)
                {
                    return "";
                }
                return Request["userNumber"].ToString();
            }
        }
        
    }
}