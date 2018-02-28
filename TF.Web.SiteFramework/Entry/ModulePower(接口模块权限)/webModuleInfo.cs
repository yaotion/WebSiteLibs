using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.WebPlatForm.Entry
{
    public class WebModuleInfo
    {
        private string m_strPowerID;
        /// <summary>
        /// 权限id
        /// </summary>
        public string strPowerID
        {
            get { return m_strPowerID; }
            set { m_strPowerID = value; }
        }

        private string m_strUrlDescribe;
        /// <summary>
        /// 导航名称
        /// </summary>
        public string strUrlDescribe
        {
            get { return m_strUrlDescribe; }
            set { m_strUrlDescribe = value; }
        }

        private string m_strUrl;
        /// <summary>
        /// 相对地址
        /// </summary>
        public string strUrl
        {
            get { return m_strUrl; }
            set { m_strUrl = value; }
        }

        private int m_b_Blank;
        /// <summary>
        /// 是否新页面打开 0是框架内打开 1是新页面打开 
        /// </summary>
        public int b_Blank
        {
            get { return m_b_Blank; }
            set { m_b_Blank = value; }
        }
    }
}
