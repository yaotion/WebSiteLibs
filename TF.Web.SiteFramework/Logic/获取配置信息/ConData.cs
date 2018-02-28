using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
namespace TF.WebPlatForm.Logic
{
    public class ConData
    {
        //系统数据库配置
        public static string WebSiteConnectionString = XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");
    }

       

    public class ConstCommon
    {
        //站点名称
        public static string SiteName
        {
            get
            {
                return XmlClass.Read("XmlConfig.xml", "/XmlConfig/SystemBase/SiteName");
            }
        }

        //项目版本号
        public static string SiteVersion
        {
            get
            {
                return XmlClass.Read("SysConfig.xml", "/SysConfig/SystemBase/SiteVersion");
            }
        }

        //平台版本号
        public static string PlatformVersion
        {
            get
            {
                return XmlClass.Read("SysConfig.xml", "/SysConfig/SystemBase/PlatformVersion");
            }
        }
    }
}
