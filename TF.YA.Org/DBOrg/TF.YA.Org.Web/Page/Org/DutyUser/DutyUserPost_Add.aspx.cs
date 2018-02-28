using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Page.Org.DutyUser
{
    public partial class DutyUserPost_Add : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {

                List<PostType> postTypeList = TF.YA.Org.LCOrg.GetAllPostType(WebLoader.Log, Conn);
                for (int i = 0; i < postTypeList.Count; i++)
                {
                    DDLPostType.Items.Add(new ListItem(postTypeList[i].PostTypeName, postTypeList[i].PostTypeID.ToString()));
                }
               
            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {

                
                DutyUserPost  p = new DutyUserPost();
                p.PostTypeID = Int32.Parse(DDLPostType.SelectedValue);
                p.PostTypeName = DDLPostType.SelectedItem.Text;
                


                LCDutyUser.AddDutyPost(WebLoader.Log, Conn, p);
                PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
            }
        }

    }
}