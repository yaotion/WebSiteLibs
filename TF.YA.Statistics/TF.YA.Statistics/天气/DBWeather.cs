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
    public class DBWeather
    {
        /// <summary>
        /// 获取天气
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="DiDian"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static bool GetWeather(ILog Log, SqlConnection Conn, string DiDian, DateTime Day,Weather W)
        {
            string strSql = @"select * from TAB_Statistics_Weather where DiDian = @DiDian and WeatherDay=@Day";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DiDian",DiDian),
                new SqlParameter("Day",Day.Date),
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                W.DiDian = TF.DB.DBConvert.ToString(dt.Rows[0]["DiDian"]);
                W.FengLi = TF.DB.DBConvert.ToString(dt.Rows[0]["FengLi"]);
                W.ShiDu = TF.DB.DBConvert.ToString(dt.Rows[0]["ShiDu"]);
                W.WeatherDay = TF.DB.DBConvert.ToDateTime(dt.Rows[0]["WeatherDay"]);
                W.WenDu = TF.DB.DBConvert.ToString(dt.Rows[0]["WenDu"]);
                W.ZhuangKuang = TF.DB.DBConvert.ToString(dt.Rows[0]["ZhuangKuang"]);
                return true;
            }
            return false;
        }
        public static void UpdateWeather(ILog Log, SqlConnection Conn, Weather W)
        {
            string strSql = @"update TAB_Statistics_Weather set FengLi=@FengLi,ShiDu=@ShiDu,WenDu=@WenDu,ZhuangKuang=@ZhuangKuang where DiDian = @DiDian and WeatherDay=@WeatherDay";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DiDian",W.DiDian),
                new SqlParameter("WeatherDay",W.WeatherDay),
                new SqlParameter("FengLi",W.FengLi),
                new SqlParameter("ShiDu",W.ShiDu),
                new SqlParameter("WenDu",W.WenDu),
                new SqlParameter("ZhuangKuang",W.ZhuangKuang),
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static void AddWeather(ILog Log, SqlConnection Conn, Weather W)
        {
            string strSql = @"insert into  TAB_Statistics_Weather (DiDian,WeatherDay,FengLi,ShiDu,WenDu,ZhuangKuang) values (@DiDian,@WeatherDay,@FengLi,@ShiDu,@WenDu,@ZhuangKuang) ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DiDian",W.DiDian),
                new SqlParameter("WeatherDay",W.WeatherDay),
                new SqlParameter("FengLi",W.FengLi),
                new SqlParameter("ShiDu",W.ShiDu),
                new SqlParameter("WenDu",W.WenDu),
                new SqlParameter("ZhuangKuang",W.ZhuangKuang),
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
    }
}
