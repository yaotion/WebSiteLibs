using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace TF.YA.Sync
{
    

        /// <summary>  
        /// 有关HTTP请求的辅助类  
        /// </summary>  
        public class HttpCommon
        {
            private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            /// <summary>
            /// 无参数读取
            /// </summary>
            /// <param name="Url">地址</param>

            /// <returns>返回请求字符</returns>
            public static RespData Get(string Url, out string error)
            {
                error = string.Empty;
                string strconn = string.Empty;
                RespData resultdata = null;
                string type = "UTF-8";
                try
                {
                    System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                    // Get the response instance.
                    wReq.Method = "GET";
                    System.Net.WebResponse wResp = wReq.GetResponse();
                    System.IO.Stream respStream = wResp.GetResponseStream();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                    {
                        strconn = reader.ReadToEnd();

                        resultdata = Newtonsoft.Json.JsonConvert.DeserializeObject<RespData>(strconn);
                    }
                }
                catch (System.Exception ex)
                {
                    error = ex.Message;
                }
                return resultdata;
            }
            public static String GetStr(string Url, out string error)
            {
                error = string.Empty;
                string strconn = string.Empty;
              //  RespData resultdata = null;
                string type = "UTF-8";
                try
                {
                    System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                    // Get the response instance.
                    wReq.Method = "GET";
                    System.Net.WebResponse wResp = wReq.GetResponse();
                    System.IO.Stream respStream = wResp.GetResponseStream();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                    {
                        strconn = reader.ReadToEnd();

                      //  resultdata = Newtonsoft.Json.JsonConvert.DeserializeObject<RespData>(strconn);
                    }
                }
                catch (System.Exception ex)
                {
                    error = ex.Message;
                }
                return strconn;
            }
            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true; //总是接受  
            }
            /// <summary>
            ///  向接口发送报警数据
            /// </summary>
            /// <param name="URL">地址</param>
            /// <param name="requestobj">对象请求值</param>
            /// <param name="error">返回错误值</param>
            /// <returns></returns>
            public static bool Post(string URL, RequData requestobj, out string error)
            {
                //string strEncoding = "UTF-8";
                error = string.Empty;
                bool bret = false;
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = null;
                try
                {
                    //如果是发送HTTPS请求  
                    if (URL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                        request = WebRequest.Create(URL) as HttpWebRequest;
                        request.ProtocolVersion = HttpVersion.Version10;
                    }
                    else
                    {
                        request = (HttpWebRequest)WebRequest.Create(URL);
                    }

                    request.Method = "post";
                    request.Accept = "text/html, application/xhtml+xml, */*";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = DefaultUserAgent;
                    if (string.IsNullOrEmpty(requestobj.Encoding))
                    {
                        requestobj.Encoding = "UTF-8";
                    }
                    if (!string.IsNullOrEmpty(requestobj.UserAgent))
                    {
                        request.UserAgent = requestobj.UserAgent;
                    }
                    if (requestobj.Timeout.HasValue)
                    {
                        request.Timeout = requestobj.Timeout.Value;
                    }
                    if (requestobj.Cookies != null)
                    {
                        request.CookieContainer = new CookieContainer();
                        request.CookieContainer.Add(requestobj.Cookies);
                    }
                    string strPostdata = requestobj.ParamName + requestobj.ParamValue;//已经序列化的值 
                    byte[] buffer = encoding.GetBytes(strPostdata);
                    request.ContentLength = buffer.Length;
                    Stream sr = request.GetRequestStream();
                    sr.Write(buffer, 0, buffer.Length);
                    sr.Close();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(requestobj.Encoding)))
                    {
                        string ret = reader.ReadToEnd();
                        RespData resultdata = Newtonsoft.Json.JsonConvert.DeserializeObject<RespData>(ret);
                        if (resultdata.Success == "0")
                        {
                            bret = true;
                        }

                    }
                }
                catch (Exception errormsg)
                {
                    error = errormsg.Message;
                }
                return bret;

            }

        }  
   
}
