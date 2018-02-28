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
    public class LCTrainCount
    {
        public static bool GetTrainCount(ILog Log, SqlConnection Conn, string JWDCode, TrainCount tc)
        {
            return DBTrainCount.GetTrainCount(Log, Conn, JWDCode, tc);
        }
        public static void UpdateTrainCount(ILog Log, SqlConnection Conn, TrainCount tc)
        {
            TrainCount tW = new TrainCount();
            if (DBTrainCount.GetTrainCount(Log, Conn, tc.JWDCode, tW))
            {
                DBTrainCount.UpdateTrainCount(Log, Conn, tc);
            }
            else
            {
                DBTrainCount.AddTrainCount(Log, Conn, tc);
            }
        }
        
    }

    public class LCTrainTJ
    {
        public static List<TrainTJ> QueryTJ(ILog Log, SqlConnection Conn, DateTime BeginTime, DateTime EndTime, string JWDCode)
        {
            return DBTrainTJ.QueryTJ(Log, Conn, BeginTime, EndTime, JWDCode);
        }
        public static void UpdateTrainTJ(ILog Log, SqlConnection Conn, TrainTJ TTJ)
        {
            TrainTJ tW = new TrainTJ();
            if (DBTrainTJ.TrainTJ(Log, Conn,TTJ.TJDay, tW))
            {
                DBTrainTJ.UpdateTrainTJ(Log, Conn, TTJ);
            }
            else
            {
                DBTrainTJ.AddTrainTJ(Log, Conn, TTJ);
            }
        }
    }
}
