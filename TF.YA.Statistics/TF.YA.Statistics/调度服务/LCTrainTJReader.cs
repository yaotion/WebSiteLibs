using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Common.Logging;

namespace TF.YA.Statistics
{
    public class LCTrainTJReader
    {
        public static bool ReadTrainTJ(string TrainBJConn, DateTime TJDay,TrainTJ TTJ)
        {
      
            string strSql = "select * from TAB_Dat_RunStatistics where dtRunStatistics>=@BeginTime and dtRunStatistics<=@EndTime";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",TJDay.Date),
                new SqlParameter("EndTime",TJDay.Date.AddDays(1).AddSeconds(-1))
            };
            DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(TrainBJConn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TTJ.TJDay = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["dtRunStatistics"]);
                TTJ.SD = TF.DB.DBConvert.ToDouble(dt.Rows[0]["nJssd"]);
                TTJ.TR = TF.DB.DBConvert.ToDouble(dt.Rows[0]["nJctr"]);
                TTJ.ZX = TF.DB.DBConvert.ToDouble(dt.Rows[0]["nZxgl"]);
                TTJ.ZZ = TF.DB.DBConvert.ToDouble(dt.Rows[0]["nQyzzdgl"]);
                TTJ.CL = TF.DB.DBConvert.ToDouble(dt.Rows[0]["nJctrcl"]);
                return true;
            }
            return false;
       
        }
    }
    public class LCTrainTJSync
    {
        public static void ExecSync(ILog Log, string TrainTJConn, string LocalDBConn, DateTime SyncDay)
        {
            using (SqlConnection Conn = new SqlConnection(LocalDBConn))
            {
                TrainTJ ttj = new TrainTJ();
                if (LCTrainTJReader.ReadTrainTJ(TrainTJConn,SyncDay,ttj))
                {
                    LCTrainTJ.UpdateTrainTJ(Log, Conn, ttj);
                }
               
            }
        }
    }
}
