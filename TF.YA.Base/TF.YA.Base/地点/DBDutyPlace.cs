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
    public class DBDutyPlace
    {
        public static void AddPlace(ILog Log, SqlConnection Conn, DutyPlace Place)
        {
            string strSql = @"insert into TAB_Base_DutyPlace (PlaceID,PlaceName)  values (@PlaceID,@PlaceName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PlaceID",Place.PlaceID),
                                           new SqlParameter("PlaceName",Place.PlaceName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void UpdatePlace(ILog Log, SqlConnection Conn, DutyPlace Place)
        {
            string strSql = @"update TAB_Base_DutyPlace set PlaceName=@PlaceName where PlaceID=@PlaceID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PlaceID",Place.PlaceID),
                                           new SqlParameter("PlaceName",Place.PlaceName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeletePlace(ILog Log, SqlConnection Conn, string PlaceID)
        {
            string strSql = @"delete from  TAB_Base_DutyPlace where PlaceID=@PlaceID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PlaceID",PlaceID)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static bool GetPlace(ILog Log, SqlConnection Conn, string PlaceID, DutyPlace Place)
        {
            string strSql = @"select * from TAB_Base_DutyPlace where PlaceID = @PlaceID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("PlaceID",PlaceID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {

                Place.PlaceID = TF.DB.DBConvert.ToString(dt.Rows[0]["PlaceID"]);
                Place.PlaceName = TF.DB.DBConvert.ToString(dt.Rows[0]["PlaceName"]);
               
                return true;
            }
            return false;
        }

        public static List<DutyPlace> GetAllPlace(ILog Log, SqlConnection Conn)
        {
            List<DutyPlace> result = new List<DutyPlace>();
            string strSql = @"select * from TAB_Base_DutyPlace order by PlaceID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DutyPlace Place = new DutyPlace();
                Place.PlaceID = TF.DB.DBConvert.ToString(dt.Rows[i]["PlaceID"]);
                Place.PlaceName = TF.DB.DBConvert.ToString(dt.Rows[i]["PlaceName"]);
                result.Add(Place);

            }

            return result;
        }
    }
}
