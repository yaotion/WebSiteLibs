using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using TF.DB.DBUtility;
using Common.Logging;
using System.Data.SqlClient;

namespace TF.YA.Base
{
    public class IFLoader
    {
        public static string GetDBConn()
        {
            return TF.CommonUtility.XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");
        }
        public static ILog GetLogger()
        {
            return Common.Logging.LogManager.GetLogger("TF.YA.Base");
        }
    }


    public class LCBaseIF
    {

        private static TF.Web.WebAPI.InterfaceOutPut result = new TF.Web.WebAPI.InterfaceOutPut();

        private class GetAllStationsIn
        {
            public int PageIndex = 1;
            public int PageCount = 1;
        }


        public class GetAllStationsOut
        {
            public int TotalCount;
            public List<YA.Base.Station> stations;
        }


        public TF.Web.WebAPI.InterfaceOutPut GetAllStations(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                GetAllStationsIn inParams = (GetAllStationsIn)Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllStationsIn>(InputString);


                List<TF.YA.Base.Station> stations = LCStation.QueryStation(IFLoader.GetLogger(), Conn, inParams.PageIndex, inParams.PageCount, "", "","","");


                GetAllStationsOut outputData = new GetAllStationsOut();
                outputData.stations = stations;
                outputData.TotalCount = LCStation.QueryStationCount(IFLoader.GetLogger(), Conn, "", "", "","");

                result.Data = outputData;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }

        public class OutGetAllPlaces
        {
            //地点列表
            public List<DutyPlace> places = new List<DutyPlace>();
        }

        public TF.Web.WebAPI.InterfaceOutPut GetAllPlaces(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                List<TF.YA.Base.DutyPlace> places = LCDutyPlace.GetAllPlace(IFLoader.GetLogger(), Conn);

                OutGetAllPlaces data = new OutGetAllPlaces();
                data.places = places;
                result.Data = data;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }




        public class OutGetAllICSections
        {
            //区段列表
            public List<ICSection> SectionList = new List<ICSection>();
        }

        /// <summary>
        /// 获取所有的写卡区段
        /// </summary>
        public TF.Web.WebAPI.InterfaceOutPut GetAllICSections(String Data)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                TF.Web.WebAPI.InterfaceOutPut output = new TF.Web.WebAPI.InterfaceOutPut();
                try
                {
                    OutGetAllICSections OutParams = new OutGetAllICSections();

                    OutParams.SectionList = TF.YA.Base.LCICSection.GetAllSections(IFLoader.GetLogger(), Conn);
                    output.Data = OutParams;
                    output.Success = 1;
                }
                catch (Exception ex)
                {
                    output.ResultText = ex.Message;
                }
                return output;
            }
        }

    }
}
