using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TF.YA.Statistics.Web
{
    public class FrameUtils
    {
        public static string ResourcePath
        {
            get
            {
                object obj = null;
                if (System.Configuration.ConfigurationManager.ConnectionStrings != null)
                {
                    if (System.Configuration.ConfigurationManager.ConnectionStrings["FrameHost"] != null)
                    {
                        obj = System.Configuration.ConfigurationManager.ConnectionStrings["FrameHost"].ConnectionString;
                    }
                }


                string frameHost = string.Format("http://{0}:{1}", HttpContext.Current.Request.ServerVariables["server_name"], HttpContext.Current.Request.ServerVariables["server_port"]);
                if ((obj != null) && (obj.ToString() != ""))
                {
                    frameHost = obj.ToString();
                }
                return frameHost;
            }
        }

        public static string ResourceJs
        {
            get
            {
                return ResourcePath + "/Page/Platform/js.ashx";
            }
        }
        public static string ResourceCss
        {
            get
            {
                return ResourcePath + "/Page/Platform/css.ashx";
            }
        }
    }
}