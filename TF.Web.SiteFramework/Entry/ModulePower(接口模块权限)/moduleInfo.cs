using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.WebPlatForm.Entry
{
    public class ModuleInfo
    {
        private string m_strID;
        /// <summary>
        /// 模块id
        /// </summary>
        public string strID
        {
            get { return m_strID; }
            set { m_strID = value; }
        }

        private string m_strModelName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string strModelName
        {
            get { return m_strModelName; }
            set { m_strModelName = value; }
        }

        private List<PowerModule> m_powerList;
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<PowerModule> powerList
        {
            get { return m_powerList; }
            set { m_powerList = value; }
        }
    }
}
