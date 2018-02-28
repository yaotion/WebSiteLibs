using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Base.Web.Base.DutyPlace
{
    public partial class DutyPlace_Add : System.Web.UI.Page
    {
        
        TF.YA.Base.DutyPlace dutyPlace;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                if (PlaceID.Length > 0)
                {
                    dutyPlace = new TF.YA.Base.DutyPlace();
                    if (TF.YA.Base.LCDutyPlace.GetPlace(WebLoader.Log, Conn, PlaceID, dutyPlace))
                    {
                        TB_PlaceID.Value = dutyPlace.PlaceID;
                        TB_PlaceName.Value = dutyPlace.PlaceName;
                    }
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                if (PlaceID.Length > 0)
                {
                    dutyPlace = new TF.YA.Base.DutyPlace();
                    if (TF.YA.Base.LCDutyPlace.GetPlace(WebLoader.Log, Conn, PlaceID, dutyPlace))
                    {
                        dutyPlace.PlaceName = TB_PlaceName.Value;
                        TF.YA.Base.LCDutyPlace.UpdatePlace(WebLoader.Log, Conn, dutyPlace);
                        PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                    }

                }
                else
                {
                    dutyPlace = new YA.Base.DutyPlace();
                    dutyPlace.PlaceID = TB_PlaceID.Value;
                    dutyPlace.PlaceName = TB_PlaceName.Value;
                    TF.YA.Base.LCDutyPlace.AddPlace(WebLoader.Log, Conn, dutyPlace);
                    PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
                }
            }
        }


        public string PlaceID
        {
            get
            {
                if (Request["placeID"] == null)
                {
                    return "";
                }
                return Request["placeID"].ToString();
            }
        }
    }
}