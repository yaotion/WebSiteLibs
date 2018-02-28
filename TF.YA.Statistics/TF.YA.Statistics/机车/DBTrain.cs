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
    public class DBTrainCount
    {
        public static bool GetTrainCount(ILog Log, SqlConnection Conn, string JWDCode, TrainCount tc)
        {
            string strSql = @"select * from TAB_Statistics_Train_Count where JWDCode = @JWDCode";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("JWDCode",JWDCode)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                tc.JWDCode = TF.DB.DBConvert.ToString(dt.Rows[0]["JWDCode"]);
                tc.JWDName = TF.DB.DBConvert.ToString(dt.Rows[0]["JWDName"]);
                tc.PeiShuCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["PeiShuCount"]);
                tc.ZhiPeiCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["ZhiPeiCount"]);
                tc.YunYongCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["YunYongCount"]);
                tc.FeiYongCount = TF.DB.DBConvert.ToInt32(dt.Rows[0]["FeiYongCount"]);
                return true;
            }


            return false;
        }

        public static void AddTrainCount(ILog Log, SqlConnection Conn,TrainCount tc)
        {
            string strSql = @"insert into  TAB_Statistics_Train_Count (JWDCode,JWDName,PeiShuCount,ZhiPeiCount,YunYongCount,FeiYongCount) values (@JWDCode,@JWDName,@PeiShuCount,@ZhiPeiCount,@YunYongCount,@FeiYongCount) ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("JWDCode",tc.JWDCode),
                new SqlParameter("JWDName",tc.JWDName),
                new SqlParameter("PeiShuCount",tc.PeiShuCount),
                new SqlParameter("ZhiPeiCount",tc.ZhiPeiCount),
                new SqlParameter("YunYongCount",tc.YunYongCount),
                new SqlParameter("FeiYongCount",tc.FeiYongCount),
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void UpdateTrainCount(ILog Log, SqlConnection Conn, TrainCount tc)
        {
            string strSql = @"update TAB_Statistics_Train_Count set JWDName=@JWDName,PeiShuCount=@PeiShuCount,ZhiPeiCount=@ZhiPeiCount,YunYongCount=@YunYongCount,FeiYongCount=@FeiYongCount where JWDCode = @JWDCode";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("JWDCode",tc.JWDCode),
                new SqlParameter("JWDName",tc.JWDName),
                new SqlParameter("PeiShuCount",tc.PeiShuCount),
                new SqlParameter("ZhiPeiCount",tc.ZhiPeiCount),
                new SqlParameter("YunYongCount",tc.YunYongCount),
                new SqlParameter("FeiYongCount",tc.FeiYongCount),
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
    }

    public class DBTrainTJ
    {
        public static List<TrainTJ> QueryTJ(ILog Log, SqlConnection Conn,DateTime BeginTime,DateTime EndTime,string JWDCode)
        {
            List<TrainTJ> result = new List<TrainTJ>();
            string strSql = "select * from TAB_Statistics_Train_TJ where TJDay >=@BeginTime and TJDay <=@EndTime order by TJDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("JWDCode",JWDCode),
                new SqlParameter("BeginTime",BeginTime.Date),
                new SqlParameter("EndTime",EndTime.Date.AddDays(1).AddSeconds(-1))             
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TrainTJ ttj = new TrainTJ();
                ttj.TJDay = TF.DB.DBConvert.ToDateTime(dt.Rows[i]["TJDay"]);
                ttj.JWDCode = TF.DB.DBConvert.ToString(dt.Rows[i]["JWDCode"]);
                ttj.JWDName = TF.DB.DBConvert.ToString(dt.Rows[i]["JWDName"]);
                ttj.SD = Convert.ToDouble(dt.Rows[i]["SD"]);
                ttj.TR = Convert.ToDouble(dt.Rows[i]["TR"]);
                ttj.ZX = Convert.ToDouble(dt.Rows[i]["ZX"]);
                ttj.ZZ = Convert.ToDouble(dt.Rows[i]["ZZ"]);
                ttj.CL = Convert.ToDouble(dt.Rows[i]["CL"]);               
                result.Add(ttj);
            }

            return result;
        }

        public static bool TrainTJ(ILog Log, SqlConnection Conn, DateTime TJDay,TrainTJ TTJ)
        {
            string strSql = "select * from TAB_Statistics_Train_TJ where TJDay=@TJDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TJDay",TJDay)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn,CommandType.Text,strSql,sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TTJ.TJDay = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["TJDay"]);
                TTJ.SD = Convert.ToDouble(dt.Rows[0]["SD"]);
                TTJ.TR = Convert.ToDouble(dt.Rows[0]["TR"]);
                TTJ.ZX = Convert.ToDouble(dt.Rows[0]["ZX"]);
                TTJ.ZZ = Convert.ToDouble(dt.Rows[0]["ZZ"]);
                TTJ.CL = Convert.ToDouble(dt.Rows[0]["CL"]);
                return true;
            }
            return false;
        }
        public static void AddTrainTJ(ILog Log, SqlConnection Conn, TrainTJ TTJ)
        {
            string strSql = "insert into TAB_Statistics_Train_TJ  (TJDay,SD,TR,ZX,ZZ,CL) values (@TJDay,@SD,@TR,@ZX,@ZZ,@CL)";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TJDay",TTJ.TJDay),
                new SqlParameter("SD",TTJ.SD.ToString("F0")),
                new SqlParameter("TR",TTJ.TR.ToString("F1")),
                new SqlParameter("ZX",TTJ.ZX.ToString("F0")),
                new SqlParameter("ZZ",TTJ.ZZ.ToString("F1")),
                new SqlParameter("CL",TTJ.CL.ToString("F0"))
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void UpdateTrainTJ(ILog Log, SqlConnection Conn, TrainTJ TTJ)
        {
            string strSql = "update TAB_Statistics_Train_TJ  set SD=@SD,TR=@TR,ZX=@ZX,ZZ=@ZZ,CL=@CL where TJDay=@TJDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TJDay",TTJ.TJDay),
                new SqlParameter("SD",TTJ.SD.ToString("F0")),
                new SqlParameter("TR",TTJ.TR.ToString("F1")),
                new SqlParameter("ZX",TTJ.ZX.ToString("F0")),
                new SqlParameter("ZZ",TTJ.ZZ.ToString("F1")),
                new SqlParameter("CL",TTJ.CL.ToString("F0"))
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
    }
}
