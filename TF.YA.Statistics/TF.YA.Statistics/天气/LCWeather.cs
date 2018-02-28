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
    public class LCWeather
    {
        /// <summary>
        /// 获取天气
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Conn"></param>
        /// <param name="DiDian"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        public static bool GetWeather(ILog Log, SqlConnection Conn, string DiDian, DateTime Day, Weather W)
        {
            return DBWeather.GetWeather(Log, Conn, DiDian, Day,W);
        }

        public static void UpdateWeather(ILog Log, SqlConnection Conn, Weather W)
        {
            Weather tW = new Weather();
            if (DBWeather.GetWeather(Log, Conn, W.DiDian, W.WeatherDay,tW))
            {
                DBWeather.UpdateWeather(Log, Conn, W);
            }
            else
            {
                DBWeather.AddWeather(Log, Conn, W);
            }
        }
    }
}
