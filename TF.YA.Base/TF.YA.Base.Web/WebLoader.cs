using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TF.YA.Base.Web
{
    public class WebLoader
    {
        public static Common.Logging.ILog Log = Common.Logging.LogManager.GetLogger("TF.YA.Base.Web");
        public static string ConnString = TF.CommonUtility.XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");        
    }
}