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
    public class LCPlanReader
    {
        public static void GetPlanSum(string YAConn,DateTime BeginTime,DateTime EndTime,Plan P)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("BeginTime",BeginTime),
                new SqlParameter("EndTime",EndTime)
            };
            string strSql = "select count(*) from TAB_Plan_Train where dtBeginWorkTime >= @BeginTime and dtBeginWorkTime <=@EndTime";
            int tempCount = Convert.ToInt32(TF.DB.DBUtility.SqlHelper.ExecuteScalar(YAConn,CommandType.Text,strSql,sqlParams));
            P.CQ = tempCount;

            strSql = "select count(*) from TAB_Plan_Train where dtLastArriveTime >= @BeginTime and dtLastArriveTime <=@EndTime";
            tempCount = Convert.ToInt32(TF.DB.DBUtility.SqlHelper.ExecuteScalar(YAConn, CommandType.Text, strSql, sqlParams));
            P.TQ = tempCount;

            strSql = "select count(*) from TAB_Plan_Train where nPlanState = 7";
            tempCount = Convert.ToInt32(TF.DB.DBUtility.SqlHelper.ExecuteScalar(YAConn, CommandType.Text, strSql, sqlParams));
            P.ZT = tempCount;

            strSql = "select count(*) from TAB_Nameplate_Group where len(strTrainmanGUID1)> 0 or  len(strTrainmanGUID2)> 0  or len(strTrainmanGUID3)> 0  or len(strTrainmanGUID4)> 0 ";
            tempCount = Convert.ToInt32(TF.DB.DBUtility.SqlHelper.ExecuteScalar(YAConn, CommandType.Text, strSql, sqlParams));
            P.All = tempCount;

        }
    }
    public class LCPlanSync
    {
        public static bool ExecSync(ILog log, string YAConn, string DBConn)
        {
            Plan P = new Plan();
            P.PlanDay = DateTime.Now.Date;
            LCPlanReader.GetPlanSum(YAConn, DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1), P);

            using (SqlConnection Conn = new SqlConnection(DBConn))
            {
                LCPlan.UpdatePlan(log, Conn, P);
            }
            return true;
        }
    }
}
