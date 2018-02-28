using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace TF.CommonUtility
{
    /// <summary>
    ///WebConfig 的摘要说明
    /// </summary>
    public class WebConfig
    {
        public WebConfig()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void WriteConfig(string item, string key, string value)
        {
            if (item == "")
            {
                item = "appSettings";
            }
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection(item);
            if (appSection.Settings[key] == null)
            {
                appSection.Settings.Add(key, value);
                config.Save();
            }
            else
            {
                appSection.Settings.Remove(key);
                appSection.Settings.Add(key, value); config.Save();
            }
        }


        public static string ReadConfig(string item, string key)
        {
            if (item == "")
            {
                item = "appSettings";
            }
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection(item);
            if (appSection.Settings[key] == null)
            {
                return "";
            }
            else
            {
                return appSection.Settings[key].Value.ToString();
            }
        }
    }
}