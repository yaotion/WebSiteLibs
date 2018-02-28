using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.WebPlatForm.Entry
{
    public class PowerModule
    {
        private string m_PowerID;
        /// <summary>
        /// 权限id
        /// </summary>
        public string PowerID
        {
            get { return m_PowerID; }
            set { m_PowerID = value; }
        }

        private string m_PowerName;
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PowerName
        {
            get { return m_PowerName; }
            set { m_PowerName = value; }
        }
    }
}
