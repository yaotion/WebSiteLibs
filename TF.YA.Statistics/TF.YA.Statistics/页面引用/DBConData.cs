using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;

namespace TF.YA.Statistics
{
    public class DBConData
    {
        public static bool GetData(ILog Log, SqlConnection Conn, string SortID,string ItemID,ConData D)
        {
            string strSql = @"select * from TAB_Statistics_ConData where SortID = @SortID and ItemID = @ItemID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("SortID",SortID),
                new SqlParameter("ItemID",ItemID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                D.ItemID = TF.DB.DBConvert.ToString(dt.Rows[0]["ItemID"]);
                D.ItemName = TF.DB.DBConvert.ToString(dt.Rows[0]["ItemName"]);
                D.SortID = TF.DB.DBConvert.ToString(dt.Rows[0]["SortID"]);
                D.ItemData = TF.DB.DBConvert.ToString(dt.Rows[0]["ItemData"]);
                return true;
            }


            return false;
        }
    }
}
