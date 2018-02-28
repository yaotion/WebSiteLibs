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
    public class DBTrainBJ
    {
        public static  List<TrainBJ>  GetBJSum(ILog Log, SqlConnection Conn, DateTime BeginTime, DateTime EndTime)
        {
            List<TrainBJ> result = new List<TrainBJ>();
            string strSql = "select sum(BJCount) BJSumCount,BJItemName from TAB_Statistics_BJ where BJDay >=@BeginTime and BJDay <=@EndTime group by BJItemName order by BJItemName";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",BeginTime.Date),
                new SqlParameter("EndTime",EndTime.Date.AddDays(1).AddSeconds(-1))             
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TrainBJ bj = new TrainBJ();
                bj.BJItenName = TF.DB.DBConvert.ToString(dt.Rows[i]["BJItemName"]);
                bj.BJCount = TF.DB.DBConvert.ToInt32(dt.Rows[i]["BJSumCount"]);
                result.Add(bj);
            }           
            return result;
        }

        public static bool GetBJ(ILog Log,SqlConnection Conn,DateTime BJDay,string BJItemID,TrainBJ BJ)        
        {
            string strSql = "select * from TAB_Statistics_BJ where BJDay =@BJDay and BJItemID=@BJItemID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BJDay",BJDay.Date),
                new SqlParameter("BJItemID",BJItemID)             
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                BJ.BJItenName = TF.DB.DBConvert.ToString(dt.Rows[0]["BJItemName"]);
                BJ.BJCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["BJCount"]);
                return true;
            }
            return false;
        }
        public static void UpdateBJ(ILog Log, SqlConnection Conn, TrainBJ BJ)
        {
            string strSql = "update TAB_Statistics_BJ set BJCount=@BJCount,BJItemName=@BJItemName where BJDay =@BJDay and BJItemID=@BJItemID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BJDay",BJ.BJDay),
                new SqlParameter("BJItemID",BJ.BJItemID),             
                new SqlParameter("BJItemName",BJ.BJItenName),
                new SqlParameter("BJCount",BJ.BJCount)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void AddBJ(ILog Log, SqlConnection Conn, TrainBJ BJ)
        {
            string strSql = "insert into TAB_Statistics_BJ (BJDay,BJItemID,BJCount,BJItemName) values (@BJDay,@BJItemID,@BJCount,@BJItemName)";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BJDay",BJ.BJDay),
                new SqlParameter("BJItemID",BJ.BJItemID),             
                new SqlParameter("BJItemName",BJ.BJItenName),
                new SqlParameter("BJCount",BJ.BJCount)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
    }
}
