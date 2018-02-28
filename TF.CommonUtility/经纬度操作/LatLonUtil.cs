using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace TF.CommonUtility
{
    public class LatLonUtil
    {

        #region 方法

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalLati_Longi">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        static public string ConvertDigitalToDegrees(string digitalLati_Longi)
        {
            double digitalDegree = Convert.ToDouble(digitalLati_Longi);
            return ConvertDigitalToDegrees(digitalDegree);
        }

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalDegree">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        static public string ConvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'°')和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');           //度的符号对应的 Unicode 代码为：00B0[1]（六十进制），显示为°。
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');           //分的符号对应的 Unicode 代码为：2032[1]（六十进制），显示为′。
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');           //秒的符号对应的 Unicode 代码为：2033[1]（六十进制），显示为″。
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'/')和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital_default(string degrees)
        {
            char ch = '.';
            return ConvertDegreesToDigital(degrees, ch);
        }

        /// <summary>
        /// 度分秒经纬度和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital(string degrees, char cflag)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf(cflag);
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf(cflag, d + 1);
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.Length;
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }

        #endregion
    }

    public class JWD { 
    static double Rc = 6378137;  // 赤道半径 
    static double Rj = 6356725;  // 极半径      
    double m_LoDeg, m_LoMin, m_LoSec;  // longtitude 经度     
    double m_LaDeg, m_LaMin, m_LaSec; 
    double m_Longitude, m_Latitude; 
    double m_RadLo, m_RadLa; 
    double Ec; 
    double Ed; 
    public JWD(double longitude, double latitude) 
    { 
      m_LoDeg = (int)longitude; 
      m_LoMin = (int)((longitude - m_LoDeg)*60); 
      m_LoSec = (longitude - m_LoDeg - m_LoMin/60)*3600; 
       
      m_LaDeg = (int)latitude; 
      m_LaMin = (int)((latitude - m_LaDeg)*60); 
      m_LaSec = (latitude - m_LaDeg - m_LaMin/60)*3600; 
       
      m_Longitude = longitude; 
      m_Latitude = latitude; 
      m_RadLo = longitude * Math.PI/180; 
      m_RadLa = latitude * Math.PI/180; 
      Ec = Rj + (Rc - Rj) * (90-m_Latitude) / 90; 
      Ed = Ec * Math.Cos(m_RadLa); 
    } 
    public static JWD GetJWDB(JWD A, double distance, double angle) 
    { 
      double dx = distance*1000 * Math.Sin(angle * Math.PI /180); 
      double dy = distance*1000 * Math.Cos(angle * Math.PI /180); 
       
      //double dx = (B.m_RadLo - A.m_RadLo) * A.Ed;       //double dy = (B.m_RadLa - A.m_RadLa) * A.Ec; 
       
  
      double BJD = (dx/A.Ed + A.m_RadLo) * 180/Math.PI; 
      double BWD = (dy/A.Ec + A.m_RadLa) * 180/Math.PI; 
      JWD B = new JWD(BJD, BWD); 
      return B; 
    } 
  
       
  
    //! 已知点A经纬度，根据B点据A点的距离，和方位，求B点的经纬度     /*! 

    public static JWD GetJWDB(double longitude, double latitude, double distance, double angle) 
    { 
      JWD A = new JWD(longitude,latitude); 
      return GetJWDB(A, distance, angle); 
    } 
      
      
    //! 计算点A 和 点B的经纬度，求他们的距离和点B相对于点A的方位     /*! 

    public static double angle(JWD A, JWD B) 
    { 
      double dx = (B.m_RadLo - A.m_RadLo) * A.Ed; 
      double dy = (B.m_RadLa - A.m_RadLa) * A.Ec; //    double out = Math.sqrt(dx * dx + dy * dy);       
        double angle = 0.0; 
       angle = Math.Atan(Math.Abs(dx/dy))*180/Math.PI; 
       // 判断象限        
        double dLo = B.m_Longitude - A.m_Longitude; 
       double dLa = B.m_Latitude - A.m_Latitude; 
        
       if(dLo > 0 && dLa <= 0) { 
         angle = (90 - angle) + 90; 
        } 
       else if(dLo <= 0 && dLa < 0) { 
         angle = angle + 180; 
        } 
       else if(dLo < 0 && dLa >= 0) { 
         angle = (90 - angle) + 270; 
        } 
      return angle; 
    } 
  
}
}
