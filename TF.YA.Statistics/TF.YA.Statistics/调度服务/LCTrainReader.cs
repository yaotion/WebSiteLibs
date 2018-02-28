using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using Common.Logging;
using System.Data;

using System.Data.SqlClient;
namespace TF.YA.Statistics
{
    public class LCTrainSync
    {
        public static void ReadTrainCount(string JCConn, TrainCount tc)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
            
            };
            string strSql = @"select count(1) as 'PS',
                        isnull(sum(case when nIsControlTrain=1 then 1 else  0 end),0) as 'ZP',
                        isnull(sum(case when nIsApplicationTrain=1 then 1 else  0 end),0) as 'YY',
                        isnull(sum(case when nIsApplicationTrain=2 then 1 else  0 end),0) as 'FY'
                        from TAB_Dic_Train";
            DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(JCConn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                tc.PeiShuCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["PS"]);
                tc.ZhiPeiCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["ZP"]);
                tc.YunYongCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["YY"]);
                tc.FeiYongCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["FY"]);
            }
        }
        public static bool ExecSync(ILog log, string JCConn, string DBConn,string JCJWDID)
        {
            using (SqlConnection Conn = new SqlConnection(DBConn))
            {
                TrainCount tc = new TrainCount();
                tc.JWDCode = JCJWDID;                
                ReadTrainCount(JCConn, tc);
                LCTrainCount.UpdateTrainCount(log, Conn, tc);
            }
            return true;
        }
    }
}
