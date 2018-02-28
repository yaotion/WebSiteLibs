using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;  

namespace TF.YA.Base.Web.Page.Base.Station.ashx
{
    /// <summary>
    /// Station_Import_M 的摘要说明
    /// </summary>
    public class Station_Import_M : IHttpHandler
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
                        //复制整行数据
                        for (int j = row.FirstCellNum; j < row.Cells.Count; j++)
                        {
                            if (j == 0)
                                s.StationName = row.GetCell(j).StringCellValue;
                            if (j == 1)
                                s.NameJP = row.GetCell(j).StringCellValue;
                            if (j == 2)
                                s.StationNumber = row.GetCell(j).StringCellValue;                            
                        }
                        if (s.NameJP == "")
                        {
                            s.NameJP = TF.CommonUtility.StrToPinyin.GetChineseSpell(s.StationName);
                        }
                        stations.Add(s);
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