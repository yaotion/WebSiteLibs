using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Common.Logging;
using System.Data.SqlClient;
namespace TF.YA.Statistics
{
    public class WeatherTempData
    {
        public string shidu;
        public string wendu;
        public List<WeatherData> forecast;
    }
    public class SourceWeather
    {
        public string date;
        public int status;
        public string city ;
        public WeatherTempData data;

        public WeatherData weatherinfo;
    }
    public class WeatherData
    {
        public string date;
        public string sunrise;
        public string high;
        public string low;
        public string sunset;
        public string fx;
        public string fl;
        public string type;
        public string notice;

       
    }
    public class LCWeatherReader
    {

        public string weatherUrl = "http://www.sojson.com/open/api/weather/json.shtml?city={0}";
        public bool GetCityWeather(string CityID,out SourceWeather W)
        {
            string wUrl = string.Format(weatherUrl,CityID);
            string w = GetWebResp(wUrl);

            W =(SourceWeather)Newtonsoft.Json.JsonConvert.DeserializeObject<SourceWeather>(w);
            if (W == null)
                return false;
            if (W.status != 200)
              return false;
            return true;
        }

        public string GetWebResp(string webUrl)
        {


            //初始化Web请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webUrl);

            //获取Web返回信息
            string strResponse = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                strResponse = reader.ReadToEnd();
            }
            return strResponse;
        }
    }

    public class LCWeatherSync
    {
        public static bool ExecSync(ILog log,string CityID,string DBConn)
        {
            LCWeatherReader wr = new LCWeatherReader();
            SourceWeather W ;
            if (!wr.GetCityWeather(CityID,out W))
            {
                return false;
            }
            Weather loclW = new Weather();
            loclW.WeatherDay = DateTime.Now.Date;
            loclW.DiDian = W.city;
            loclW.FengLi = W.data.forecast[0].fl;
            loclW.ShiDu = W.data.shidu;
            loclW.WenDu = W.data.wendu;
            loclW.ZhuangKuang = W.data.forecast[0].type;
            using (SqlConnection Conn = new SqlConnection(DBConn))
            {
                LCWeather.UpdateWeather(log, Conn, loclW);
            }
            return true;
        }
    }
}
