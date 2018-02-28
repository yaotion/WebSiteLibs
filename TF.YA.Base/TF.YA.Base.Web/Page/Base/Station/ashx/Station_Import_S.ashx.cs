using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;  
namespace TF.YA.Base.Web.Page.Base.Station.ashx
{
    /// <summary>
    /// Station_Import_S 的摘要说明
    /// </summary>
    public class Station_Import_S : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                ExecImport();
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public bool ExecImport()
        {
            if (HttpContext.Current.Request.Files.Count == 0) return false;
            HSSFWorkbook wb = new HSSFWorkbook(HttpContext.Current.Request.Files[0].InputStream);
            HSSFSheet sheet = (HSSFSheet)wb.GetSheetAt(0);
            List<TF.YA.Base.Station> stations = new List<YA.Base.Station>();
            //循环读取所有行的内容
            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {

                TF.YA.Base.Station s = new YA.Base.Station();
                //读取excel的行
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                if (row != null)
                {
                    if (i == 0) continue;
                    if (row.GetCell(0) != null)
                    {
                        s.StationName = row.GetCell(0).StringCellValue;
                        if (s.StationName == "") continue;
                        YA.Base.Station hasStation = stations.Find(item => item.StationName == s.StationName);

                        if (hasStation == null)
                        {
                            s.StationName = row.Cells[0].StringCellValue;
                            s.StationNumber = row.Cells[1].ToString() + "-" + row.Cells[2].ToString() + "-" + row.Cells[3].ToString();                            
                            s.NameJP = TF.CommonUtility.StrToPinyin.GetChineseSpell(s.StationName);
                            stations.Add(s);
                        }
                        else
                        {
                            s = hasStation;
                            s.StationNumber = s.StationNumber + "," + row.Cells[1].ToString() + "-" + row.Cells[2].ToString() + "-" + row.Cells[3].ToString();                         
                            s.NameJP = TF.CommonUtility.StrToPinyin.GetChineseSpell(s.StationName);
                        }
                    }
                }
            }
            using (System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection(WebLoader.ConnString))
            {
                for (int i = 0; i < stations.Count; i++)
                {
                    TF.YA.Base.LCStation.UpdateStation(WebLoader.Log, Conn, stations[i]);
                }
            }
            return true;
        }
    }
}