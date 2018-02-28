using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.WebPlatForm.Entry
{
    public class userInfo
    {
        private string m_strPassword;
        /// <summary>
        /// 密码
        /// </summary>
        public string strPassword
        {
            get { return m_strPassword; }
            set { m_strPassword = value; }
        }

        private string m_strTrianmanNumber;
        /// <summary>
        /// 工号
        /// </summary>
        public string strTrianmanNumber
        {
            get { return m_strTrianmanNumber; }
            set { m_strTrianmanNumber = value; }
        }

        private string m_strTrianmanName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string strTrianmanName
        {
            get { return m_strTrianmanName; }
            set { m_strTrianmanName = value; }
        }

        private string m_strRoleID;
        /// <summary>
        /// 角色
        /// </summary>
        public string strRoleID
        {
            get { return m_strRoleID; }
            set { m_strRoleID = value; }
        }
    }
}
