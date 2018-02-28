using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Org.Web.Org
{
    public partial class Post_Add : System.Web.UI.Page
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
                //修改模式
                if (PostID != "")
                {
                    Post p = new Post();
                    if (LCOrg.GetPost(WebLoader.Log, Conn, PostID, p))
                    {
                        TB_PostID.Value = p.PostID;
                        TB_PostName.Value = p.PostName;
                        DDLPostType.SelectedIndex = DDLPostType.Items.IndexOf(DDLPostType.Items.FindByValue(p.PostType.ToString()));
                    }
                    return;
                }
            }
                  
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {

                Post p = new Post();
                if (PostID != "")
                {

                    if (LCOrg.GetPost(WebLoader.Log, Conn, PostID, p))
                    {

                        p.PostName = TB_PostName.Value;
                        p.PostType = Int32.Parse(DDLPostType.SelectedValue);
                        LCOrg.UpdatePost(WebLoader.Log, Conn, p);
                    }
                    PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                    return;
                }
                p = new Post();
                p.PostID = TB_PostID.Value;
                p.PostName = TB_PostName.Value;
                p.PostType = Int32.Parse(DDLPostType.SelectedValue);


                LCOrg.AddPost(WebLoader.Log, Conn, p);
                PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
            }
        }

        public string PostID
        {
            get
            {
                if (Request["postID"] == null)
                {
                    return "";
                }
                return Request["postID"].ToString();
            }
        }
    }
}