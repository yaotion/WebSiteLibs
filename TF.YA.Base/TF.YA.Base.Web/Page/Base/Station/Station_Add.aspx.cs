using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TF.YA.Base.Web.Base.Station
{
    public partial class Station_Add : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //修改模式
            if (StationName.Length > 0)
            {
                using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
                {
                    TF.YA.Base.Station s = new TF.YA.Base.Station();
                    if (TF.YA.Base.LCStation.GetStation(WebLoader.Log, Conn,StationName, s))
                    {
                        TB_StationName.Attributes.Add("readonly", "true");  
                        TB_StationName.Value = s.StationName;
                        string[] numbers = s.StationNumber.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (numbers.Length > 0)
                        {
                            string[] items = numbers[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length >= 3)
                            {
                                TB_CZ1.Value = items[0];
                                TB_JL1.Value = items[1];
                                TB_TMIS1.Value = items[2];
                            }
                        }
                        if (numbers.Length > 1)
                        {
                            string[] items = numbers[1].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length >= 3)
                            {
                                TB_CZ2.Value = items[0];
                                TB_JL2.Value = items[1];
                                TB_TMIS2.Value = items[2];
                            }
                        }

                        if (numbers.Length > 2)
                        {
                            string[] items = numbers[2].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length >= 3)
                            {
                                TB_CZ3.Value = items[0];
                                TB_JL3.Value = items[1];
                                TB_TMIS3.Value = items[2];
                            }
                        }
                
                    }
                }
            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                TF.YA.Base.Station s = new TF.YA.Base.Station();
                if (StationName.Length > 0)
                {
                    TF.YA.Base.LCStation.GetStation(WebLoader.Log, Conn, StationName, s);
                }
                else
                {
                    s.StationName = TF.DB.DBConvert.ToString(TB_StationName.Value);
                }
                string sNumber = "";
                if (!(string.IsNullOrEmpty(TB_JL1.Value) || string.IsNullOrEmpty(TB_CZ1.Value) || string.IsNullOrEmpty(TB_TMIS1.Value)))
                {
                    if (sNumber != "")
                        sNumber = sNumber + "," + string.Format("{0}-{1}-{2}", TB_JL1.Value, TB_CZ1.Value, TB_TMIS1.Value);
                    else
                        sNumber = string.Format("{0}-{1}-{2}", TB_JL1.Value, TB_CZ1.Value, TB_TMIS1.Value);
                }
                if (!(string.IsNullOrEmpty(TB_JL2.Value) || string.IsNullOrEmpty(TB_CZ2.Value) || string.IsNullOrEmpty(TB_TMIS2.Value)))
                {
                    if (sNumber != "")
                        sNumber = sNumber + "," + string.Format("{0}-{1}-{2}", TB_JL2.Value, TB_CZ2.Value, TB_TMIS2.Value);
                    else
                        sNumber = string.Format("{0}-{1}-{2}", TB_JL2.Value, TB_CZ2.Value, TB_TMIS2.Value);
                }
                if (!(string.IsNullOrEmpty(TB_JL3.Value) || string.IsNullOrEmpty(TB_CZ3.Value) || string.IsNullOrEmpty(TB_TMIS3.Value)))
                {
                    if (sNumber != "")
                        sNumber = sNumber + "," + string.Format("{0}-{1}-{2}", TB_JL3.Value, TB_CZ3.Value, TB_TMIS3.Value);
                    else
                        sNumber = string.Format("{0}-{1}-{2}", TB_JL3.Value, TB_CZ3.Value, TB_TMIS3.Value);
                }
                s.StationNumber = sNumber;
                if (StationName.Length > 0)
                {

                    TF.YA.Base.LCStation.UpdateStation(WebLoader.Log, Conn, s);
                }
                else
                {
                    TF.YA.Base.LCStation.UpdateStation(WebLoader.Log, Conn, s);
                }
                PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.appdel_do();art.dialog.close();");
            }
        }

        public string StationName
        {
            get
            {
                if (HttpContext.Current.Request["sname"] == null)
                {
                    return "";
                }
                return TF.DB.DBConvert.ToString(HttpContext.Current.Request["sname"]);
            }
        }

        
    }
}