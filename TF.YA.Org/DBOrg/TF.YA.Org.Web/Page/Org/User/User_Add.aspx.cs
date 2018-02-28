using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TF.YA.Org;

namespace TF.YA.Org.Web
{
    public partial class User_Add : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                List<Post> PostList = TF.YA.Org.LCOrg.GetAllPosts(WebLoader.Log, Conn);
                for (int i = 0; i < PostList.Count; i++)
                {
                    DDL_Post.Items.Add(new ListItem(PostList[i].PostName, PostList[i].PostID));
                }
                if (UserNumber != "")
                {
                    YA.Org.User u = new YA.Org.User();
                    YA.Org.LCUser.GetUser(WebLoader.Log, Conn, UserNumber, u);
                    TB_UserNumber.Value = u.UserNumber;
                    TB_UserName.Value = u.UserName;
                    TB_NameJP.Value = u.NameJP;
                    TB_TelNumber.Value = u.TelNumber;
                    TB_DeptID.Value = u.DeptID;
                    TB_Dept.Value = u.DeptName;
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Org.User u = new TF.YA.Org.User();
                if (UserNumber != "")
                {
                    TF.YA.Org.LCUser.GetUser(WebLoader.Log, Conn, UserNumber, u);
                }
                else
                {
                    u.UserNumber = TB_UserNumber.Value;
                    u.UserGUID = Guid.NewGuid().ToString();
                }
                u.UserName = TB_UserName.Value;
                u.NameJP = TF.CommonUtility.StrToPinyin.GetChineseSpell(TB_UserName.Value);
                u.TelNumber = TB_TelNumber.Value;
                u.DeptID = TB_DeptID.Value;
                u.DeptName = TB_Dept.Value;
                u.PostID = DDL_Post.SelectedValue;
                u.PostName = DDL_Post.SelectedItem.Text;
                if (UserNumber == "")
                    TF.YA.Org.LCUser.AddUser(WebLoader.Log, Conn, u);
                else
                    TF.YA.Org.LCUser.UpdateUser(WebLoader.Log, Conn, u);
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