using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace TF.Web.WebAPI
{
    public class WebApiUtils
    {
        public static InterfaceOutPut GetAPI(string APIUrl,string APIName,string APIData)
        {

            InterfaceOutPut result;
            Encoding encoding = Encoding.UTF8;

            string postData = string.Format("DataType={0}&Data={1}", APIName, APIData);

            //初始化Web请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(APIUrl + "?{0}", postData));
                                 
            //获取Web返回信息
            string strResponse = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();            
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                strResponse = reader.ReadToEnd();
            }
            //抛出返回为空的异常
            if (strResponse == "")
            {
                throw new NullReferenceException("服务器返回空字符串");
            }

            result = (InterfaceOutPut)Newtonsoft.Json.JsonConvert.DeserializeObject<InterfaceOutPut>(strResponse);
            if (result == null)
            {
                throw new NullReferenceException("服务器返回错误的接口格式");
            }
            return result;
        }
    }
}
