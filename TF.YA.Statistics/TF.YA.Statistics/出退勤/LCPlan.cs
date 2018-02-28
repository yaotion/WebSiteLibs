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
    public class LCPlan
    {
        public static bool GetPlanSum(ILog Log, SqlConnection Conn, DateTime Day, Plan P)
        {
            return DBPlan.GetPlanSum(Log,Conn,Day,P);
        }

        public static List<Plan> GetPlanSum(ILog Log, SqlConnection Conn, DateTime BeginTime, DateTime EndTime)
        {
            return DBPlan.GetPlanSum(Log,Conn,BeginTime,EndTime);
        }

        public static void UpdatePlan(ILog Log, SqlConnection Conn, Plan P)
        {
            Plan tP = new Plan();
            if (DBPlan.GetPlanSum(Log, Conn, P.PlanDay, tP))
            {
                DBPlan.UpdatePlan(Log, Conn, P);
            }
            else
            {
                DBPlan.AddPlan(Log, Conn, P);
            }
        }
    }
}
