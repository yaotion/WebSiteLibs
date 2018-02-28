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
    
    public class LCStation
    {
        public static List<Station> QueryStation(ILog Log, SqlConnection Conn, int PageIndex, int PageCount, string StationName, string NameJP,string JLNumber,string TMISNumber)
        {
            return TF.YA.Base.DBStation.QueryStation(Log, Conn, PageIndex, PageCount, StationName, NameJP, JLNumber, TMISNumber);
        }

        public static int QueryStationCount(ILog Log, SqlConnection Conn, string StationName, string NameJP, string JLNumber, string TMISNumber)
        {
            return TF.YA.Base.DBStation.QueryStationCount(Log, Conn, StationName, NameJP, JLNumber, TMISNumber);
        }
        public static void DeleteStation(ILog Log, SqlConnection Conn, string StationName)
        {
            TF.YA.Base.DBStation.DeleteStation(Log, Conn, StationName);
            TF.YA.Base.DBStation.DeleteStationNumber(Log, Conn, StationName);            
        }

        public static bool GetStation(ILog Log, SqlConnection Conn, string StationName, Station AStation)
        {
            return TF.YA.Base.DBStation.GetStation(Log, Conn, StationName, AStation);
        }
   
        public static void UpdateStation(ILog Log, SqlConnection Conn, Station AStation)
        {           
            List<string> numebrList = AStation.StationNumber.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();


            AStation.StationNumber = "";
            for (int i = 0; i < numebrList.Count; i++)
            {
                if (i == 0)
                {
                    AStation.StationNumber = numebrList[i];
                }
                else
                {
                    AStation.StationNumber += "," + numebrList[i];
                }
            }
            TF.YA.Base.Station TS = new Station();
            if (TF.YA.Base.DBStation.GetStation(Log, Conn, AStation.StationName, TS))
            {
                TF.YA.Base.DBStation.UpdateStation(Log, Conn, AStation);
            }
            else {
                TF.YA.Base.DBStation.AddStation(Log, Conn, AStation);
            }
            

         
            //插入车站运行记录车站号交路号信息
            TF.YA.Base.DBStation.UpdateStation(Log, Conn, AStation);
            for (int i = 0; i < numebrList.Count; i++)
            {
                string[] yjLtem = numebrList[i].Split(new char[] { '-' });
                if (yjLtem.Length < 3) continue;
                if (!DBStation.ExistStationNumber(Log, Conn, AStation.StationName, TF.DB.DBConvert.ToInt32(yjLtem[0]), TF.DB.DBConvert.ToInt32(yjLtem[1])))
                {
                    DBStation.AddStationNumber(Log, Conn, AStation.StationName, TF.DB.DBConvert.ToInt32(yjLtem[0]), TF.DB.DBConvert.ToInt32(yjLtem[1]), TF.DB.DBConvert.ToInt32(yjLtem[2]));
                }
            }
        }
    }

   
    

}
