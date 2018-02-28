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
    public class DBPlan
    {
        public static bool GetPlanSum(ILog Log, SqlConnection Conn, DateTime Day, Plan P)
        {
            string strSql = @"select * from TAB_Statistics_PlanSum where PlanDay=@PlanDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("PlanDay",Day)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                P.CQ = TF.DB.DBConvert.ToInt32(dt.Rows[0]["CQ"]);
                P.TQ = TF.DB.DBConvert.ToInt32(dt.Rows[0]["TQ"]);
                P.ZT = TF.DB.DBConvert.ToInt32(dt.Rows[0]["ZT"]);
                P.All = TF.DB.DBConvert.ToInt32(dt.Rows[0]["All"]);
                P.PlanDay = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["PlanDay"]);
                return true;
            }


            return false;
        }

        public static List<Plan> GetPlanSum(ILog Log, SqlConnection Conn, DateTime BeginTime, DateTime EndTime)
        {
            List<Plan> result = new List<Plan>();
            string strSql = "select * from TAB_Statistics_PlanSum where PlanDay >=@BeginTime and PlanDay <=@EndTime  order by PlanDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",BeginTime.Date),
                new SqlParameter("EndTime",EndTime.Date.AddDays(1).AddSeconds(-1))             
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Plan p = new Plan();
                p.PlanDay = TF.DB.DBConvert.ToDateTime(dt.Rows[i]["PlanDay"]);
                p.CQ = TF.DB.DBConvert.ToInt32(dt.Rows[i]["CQ"]);
                p.TQ = TF.DB.DBConvert.ToInt32(dt.Rows[i]["TQ"]);
                p.ZT = TF.DB.DBConvert.ToInt32(dt.Rows[0]["ZT"]);
                p.All = TF.DB.DBConvert.ToInt32(dt.Rows[0]["All"]);
                result.Add(p);
            }
            return result;
        }


        public static void UpdatePlan(ILog Log, SqlConnection Conn,Plan P)
        {
            string strSql = @"update TAB_Statistics_PlanSum set CQ=@CQ,TQ=@TQ,ZT=@ZT,[All]=@All where PlanDay = @PlanDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("CQ",P.CQ),
                new SqlParameter("TQ",P.TQ),
                new SqlParameter("ZT",P.ZT),
                new SqlParameter("All",P.All),
                new SqlParameter("PlanDay",P.PlanDay)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void AddPlan(ILog Log, SqlConnection Conn, Plan P)
        {
            string strSql = @"insert into  TAB_Statistics_PlanSum (PlanDay,CQ,TQ,ZT,[All]) values (@PlanDay,@CQ,@TQ,@ZT,@All) ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("CQ",P.CQ),
                new SqlParameter("TQ",P.TQ),
                new SqlParameter("ZT",P.ZT),
                new SqlParameter("All",P.All),
                new SqlParameter("PlanDay",P.PlanDay)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
    }
}
