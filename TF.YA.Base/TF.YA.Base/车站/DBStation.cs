using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;
namespace TF.YA.Base
{
    public class DBStation
    {
        public static void AddStation(ILog Log, SqlConnection Conn, Station AStation)
        {
            string strSql = @"insert into TAB_Base_Station (StationName,NameJP,StationNumber) 
                values (@StationName,@NameJP,@StationNumber)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("StationName",AStation.StationName),
                                           new SqlParameter("NameJP",AStation.NameJP),
                                           new SqlParameter("StationNumber",AStation.StationNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }            
        public static void AddStationNumber(ILog Log, SqlConnection Conn, string StationName,int JLH,int CZH,int TMIS)
        {
            string strSql = @"insert into TAB_Base_Station_Number (StationName,JLH,CZH,TMIS) 
                values (@StationName,@JLH,@CZH,@TMIS)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("StationName",StationName),
                                           new SqlParameter("JLH",JLH),
                                           new SqlParameter("CZH",CZH),
                                           new SqlParameter("TMIS",TMIS)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static bool ExistStationNumber(ILog Log, SqlConnection Conn, string StationName, int JLH, int CZH)
        {
            string strSql = @"select count(*) from TAB_Base_Station_Number where JLH=@JLH and CZH=@CZH and StationName=@StationName";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("CZH",CZH),
                                           new SqlParameter("JLH",JLH),
                                           new SqlParameter("StationName",StationName)
                                       };
            return TF.DB.DBConvert.ToInt32(SqlHelper.ExecuteScalar(Conn, CommandType.Text, strSql, sqlParams)) > 0;
        }
        public static void DeleteStationNumber(ILog Log, SqlConnection Conn, string StationName)
        {
            string strSql = @"delete from TAB_Base_Station_Number where StationName=@StationName";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("StationName",StationName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void UpdateStation(ILog Log, SqlConnection Conn, Station AStation)
        {
            string strSql = @"update TAB_Base_Station set StationNumber=@StationNumber where StationName=@StationName";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("StationName",AStation.StationName),
                                           new SqlParameter("StationNumber",AStation.StationNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteStation(ILog Log, SqlConnection Conn, string StationName)
        {
            string strSql = @"delete from  TAB_Base_Station where StationName=@StationName";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("@StationName",StationName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static bool GetStation(ILog Log, SqlConnection Conn, string StationName, Station AStation)
        {

            string strSql = @"select top 1 * from TAB_Base_Station where StationName=@StationName ";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("@StationName",StationName)
                                       };
            
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0) 
            {

                AStation.nid = TF.DB.DBConvert.ToInt32(dt.Rows[0]["nid"]);
                AStation.StationName = TF.DB.DBConvert.ToString(dt.Rows[0]["StationName"]);
                AStation.NameJP = TF.DB.DBConvert.ToString(dt.Rows[0]["NameJP"]);
                AStation.StationNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["StationNumber"]);                
                return true;
               
            }
            return false;
        }
        public static List<Station> QueryStation(ILog Log, SqlConnection Conn, int PageIndex, int PageCount, string StationName, string NameJP, string JLNumber, string TMISNumber)
        {
            List<Station> result = new List<Station>();
            string strSql = @"select top " + PageCount.ToString() + " * from TAB_Base_Station where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (NameJP.Trim().Length > 0)
            {
                strCondition = strCondition + " and NameJP like @NameJP";
                sqlParamList.Add(new SqlParameter("NameJP", NameJP + "%"));
            }
            if (StationName.Trim().Length > 0)
            {
                strCondition = strCondition + " and StationName like @StationName";
                sqlParamList.Add(new SqlParameter("StationName", StationName + "%"));
            }
            if (JLNumber.Length > 0)
            {
                strCondition = strCondition + " and StationName in (select StationName from TAB_Base_Station_Number where JLH=@JLH)";
                sqlParamList.Add(new SqlParameter("JLH", JLNumber));
            }
            if (TMISNumber.Length > 0)
            {
                strCondition = strCondition + " and StationName in (select StationName from TAB_Base_Station_Number where TMIS=@TMIS)";
                sqlParamList.Add(new SqlParameter("TMIS", TMISNumber));
            }

            strSql += strCondition + " and nid  not in (select top " + ((PageIndex - 1) * PageCount).ToString()
                + " nid from TAB_Base_Station where 1=1 " + strCondition + " order by nid) ";
            strSql = strSql + " order by nid";
            SqlParameter[] sqlParams = sqlParamList.ToArray();
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Station s = new Station();
                s.nid = TF.DB.DBConvert.ToInt32(dt.Rows[i]["nid"]);
                s.StationName = TF.DB.DBConvert.ToString(dt.Rows[i]["StationName"]);
                s.NameJP = TF.DB.DBConvert.ToString(dt.Rows[i]["NameJP"]);
                s.StationNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["StationNumber"]); 
                result.Add(s);
            }
            return result;
        }

        public static int QueryStationCount(ILog Log, SqlConnection Conn, string StationName, string NameJP, string JLNumber, string TMISNumber)
        {
            List<Station> result = new List<Station>();
            string strSql = @"select count(*) from TAB_Base_Station where 1=1 ";
            string strCondition = "";
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            if (NameJP.Trim().Length > 0)
            {
                strCondition = strCondition + " and NameJP like @NameJP";
                sqlParamList.Add(new SqlParameter("NameJP", NameJP + "%"));
            }
            if (StationName.Trim().Length > 0)
            {
                strCondition = strCondition + " and StationName like @StationName";
                sqlParamList.Add(new SqlParameter("StationName", StationName + "%"));
            }
            if (JLNumber.Length > 0)
            {
                strCondition = strCondition + " and StationName in (select StationName from TAB_Base_Station_Number where JLH=@JLH)";
                sqlParamList.Add(new SqlParameter("JLH", JLNumber));
            }
            if (TMISNumber.Length > 0)
            {
                strCondition = strCondition + " and StationName in (select StationName from TAB_Base_Station_Number where TMIS=@TMIS)";
                sqlParamList.Add(new SqlParameter("TMIS", TMISNumber));
            }
            strSql += strCondition;
            SqlParameter[] sqlParams = sqlParamList.ToArray();
            object obj = SqlHelper.ExecuteScalar(Conn, CommandType.Text, strSql, sqlParams);
            
            return Convert.ToInt32(obj);
        }

    }
}
