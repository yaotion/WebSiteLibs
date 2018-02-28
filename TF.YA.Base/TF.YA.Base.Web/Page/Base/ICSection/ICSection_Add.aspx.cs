using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Base.Web.Page.Base.ICSection
{
    public partial class ICSection_Add : System.Web.UI.Page
    {
        TF.YA.Base.ICSection section;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                if ((JWDNumber.Length > 0) &&(SectionNumber　> 0))
                {
                    section = new TF.YA.Base.ICSection();
                    if (TF.YA.Base.LCICSection.GetSection(WebLoader.Log, Conn, JWDNumber, SectionNumber, section))
                    {
                        TB_JWDNumber.Value = section.JWDNumber;
                        TB_JWDName.Value = section.JWDName;
                        TB_SectionNumber.Value = section.ICSectionNumber.ToString();
                        TB_SectionName.Value = section.ICSectionName;
                    }
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                if ((JWDNumber.Length > 0) && (SectionNumber > 0))
                {
                    section = new TF.YA.Base.ICSection();
                    if (TF.YA.Base.LCICSection.GetSection(WebLoader.Log, Conn, JWDNumber, SectionNumber, section))
                    {
                        section.JWDName = TB_JWDName.Value;
                        section.ICSectionName = TB_SectionName.Value;
                        TF.YA.Base.LCICSection.UpdateSection(WebLoader.Log, Conn, section);
                        PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                    }

                }
                else
                {
                    section = new TF.YA.Base.ICSection();
                    section.JWDNumber = TB_JWDNumber.Value;
                    section.JWDName = TB_JWDName.Value;
                    section.ICSectionNumber = TF.DB.DBConvert.ToInt32(TB_SectionNumber.Value);
                    section.ICSectionName = TB_SectionName.Value;
                    TF.YA.Base.LCICSection.AddSection(WebLoader.Log, Conn,section);
                    PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                }
            }
        }
        public string JWDNumber
        {
            get
            {
                if (HttpContext.Current.Request["JWDNumber"] == null)
                {
                    return "";
                }
                return HttpContext.Current.Request["JWDNumber"].ToString();
            }
        }

        public int SectionNumber
        {
            get
            {
                if (HttpContext.Current.Request["SectionNumber"] == null)
                {
                    return 0;
                }
                return TF.DB.DBConvert.ToInt32(HttpContext.Current.Request["SectionNumber"]);
            }
        }

    }
}