using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TF.CommonUtility
{
    /// <summary>
    ///ConfigClass 的摘要说明
    /// </summary>
    public class ConfigClass 
    {
        public ConfigClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取配置文件键值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetConfig(string id)
        {
            try
            {
                return ConfigurationManager.AppSettings[id].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}

