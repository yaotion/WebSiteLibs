using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.Logging;

namespace TF.YA.Statistics
{
    public class LCTrainBJReader
    {
        public static List<TrainBJ> ReadBJSums(string TrainBJConn,DateTime BeginTime)
        {
            List<TrainBJ> result = new List<TrainBJ>();
            string strSql = "select * FROM TAB_Dic_CaveatCategory where strTypeCode <> 'XiTongBaoJing' order by strTypeCode";
            DataTable dtType = TF.DB.DBUtility.SqlHelper.ExecuteDataset(TrainBJConn,CommandType.Text,strSql).Tables[0];
            
            strSql = @"select COUNT(*) as bjCount,strCaveatCategoryName from TAB_Dat_TrainCaveat where nIsReady=1 and nOpinionType<>3
 and dtCaveatTime >=@BeginTime and dtCaveatTime <= @EndTime group by strCaveatCategoryName";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",BeginTime.Date),
                new SqlParameter("EndTime",BeginTime.Date.AddDays(1).AddSeconds(-1))
            };

            DataTable dtBJData = TF.DB.DBUtility.SqlHelper.ExecuteDataset(TrainBJConn, CommandType.Text, strSql,sqlParams).Tables[0];

            for (int i = 0; i < dtType.Rows.Count; i++)
            {
                TrainBJ bj = new TrainBJ();
                bj.BJDay = BeginTime;
                bj.BJItemID = dtType.Rows[i]["strTypeCode"].ToString();
                bj.BJItenName = dtType.Rows[i]["strTypeName"].ToString();
                bj.BJCount = 0;
                for (int k = 0; k < dtBJData.Rows.Count; k++)
                {
                    if (dtBJData.Rows[k]["strCaveatCategoryName"].ToString() == bj.BJItenName)
                    {                        
                        bj.BJCount = TF.DB.DBConvert.ToInt32(dtBJData.Rows[k]["bjCount"]);
                        break;
                    }
                    
                }
                result.Add(bj);
            }
            return result;

        }
      
    }

    public class LCTrainBJSync
    {
        public static void ExecSync(ILog Log,string TrainBJConn,string LocalDBConn, DateTime SyncDay)
        {
            using (SqlConnection Conn = new SqlConnection(LocalDBConn))
            {
                List<TrainBJ> sourceBJ = LCTrainBJReader.ReadBJSums(TrainBJConn, SyncDay);
                for (int i = 0; i < sourceBJ.Count; i++)
                {
                    LCTrainBJ.UpdateBJ(Log, Conn, sourceBJ[i]);
                }
            }
        }
    }
}
