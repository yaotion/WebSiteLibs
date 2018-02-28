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
    public class LCTrainBJ
    {
        public static List<TrainBJ> GetBJSum(ILog Log, SqlConnection Conn, DateTime BeginTime, DateTime EndTime)
        {
            return DBTrainBJ.GetBJSum(Log,Conn,BeginTime,EndTime);
        }

        public static void UpdateBJ(ILog Log, SqlConnection Conn, TrainBJ BJ)
        {
            TrainBJ tBJ = new TrainBJ();
            if (DBTrainBJ.GetBJ(Log, Conn, BJ.BJDay, BJ.BJItemID, tBJ))
            {
                DBTrainBJ.UpdateBJ(Log, Conn, BJ);
            }
            else
            {
                DBTrainBJ.AddBJ(Log, Conn, BJ);
            }
        }
    }
}
