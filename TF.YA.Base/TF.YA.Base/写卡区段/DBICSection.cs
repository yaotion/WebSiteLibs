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
    class DBICSection
    {
        public static void AddSection(ILog Log, SqlConnection Conn, ICSection Section)
        {
            string strSql = @"insert into TAB_Base_ICSection (JWDNumber,JWDName,ICSectionNumber,ICSectionName) 
                values (@JWDNumber,@JWDName,@ICSectionNumber,@ICSectionName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("JWDNumber",Section.JWDNumber),
                                           new SqlParameter("JWDName",Section.JWDName),
                                           new SqlParameter("ICSectionNumber",Section.ICSectionNumber),
                                           new SqlParameter("ICSectionName",Section.ICSectionName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void UpdateSection(ILog Log, SqlConnection Conn, ICSection Section)
        {
            string strSql = @"update TAB_Base_ICSection set JWDName=@JWDName,ICSectionName=@ICSectionName 
                where JWDNumber=@JWDNumber and ICSectionNumber=@ICSectionNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                            new SqlParameter("JWDName",Section.JWDName),
                                           new SqlParameter("ICSectionName",Section.ICSectionName),
                                           new SqlParameter("JWDNumber",Section.JWDNumber),
                                           new SqlParameter("ICSectionNumber",Section.ICSectionNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void DeleteSection(ILog Log, SqlConnection Conn, string JWDNumber, int  SectionNumber)
        {
            string strSql = @"delete from  TAB_Base_ICSection where JWDNumber=@JWDNumber and ICSectionNumber=@ICSectionNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                          new SqlParameter("ICSectionNumber",SectionNumber),
                                           new SqlParameter("JWDNumber",JWDNumber)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static bool GetSection(ILog Log, SqlConnection Conn, string JWDNumber, int SectionNumber, ICSection Section)
        {
            string strSql = @"select top 1 * from TAB_Base_ICSection where JWDNumber=@JWDNumber and ICSectionNumber=@ICSectionNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("JWDNumber",JWDNumber),
                                           new SqlParameter("ICSectionNumber",SectionNumber)
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {

                Section.JWDNumber = TF.DB.DBConvert.ToString(dt.Rows[0]["JWDNumber"]);
                Section.JWDName = TF.DB.DBConvert.ToString(dt.Rows[0]["JWDName"]);
                Section.ICSectionNumber = TF.DB.DBConvert.ToInt32(dt.Rows[0]["ICSectionNumber"]);
                Section.ICSectionName = TF.DB.DBConvert.ToString(dt.Rows[0]["ICSectionName"]);
                return true;

            }
            return false;
        }

        public static List<ICSection> GetAllSections(ILog Log, SqlConnection Conn)
        {
            List<ICSection> result = new List<ICSection>();
            string strSql = @"select * from TAB_Base_ICSection order by JWDNumber,ICSectionNumber ";
            SqlParameter[] sqlParams = new SqlParameter[]{

                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ICSection s = new ICSection();
                s.JWDNumber = TF.DB.DBConvert.ToString(dt.Rows[i]["JWDNumber"]);
                s.JWDName = TF.DB.DBConvert.ToString(dt.Rows[i]["JWDName"]);
                s.ICSectionNumber = TF.DB.DBConvert.ToInt32(dt.Rows[i]["ICSectionNumber"]);
                s.ICSectionName = TF.DB.DBConvert.ToString(dt.Rows[i]["ICSectionName"]);     
                result.Add(s);
            }
            return result;
        }
    }
}
