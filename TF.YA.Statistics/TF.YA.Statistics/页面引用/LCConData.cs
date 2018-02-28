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
    public class LCConData
    {
        public static bool GetData(ILog Log, SqlConnection Conn, string SortID,string ItemID,ConData D)
        {
            return DBConData.GetData(Log, Conn, SortID, ItemID,D);
        }
    }
}
