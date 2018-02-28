using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace TF.WebPlatForm.Entry
{ /// <summary>
    ///类名: WebPlatForm_Module_Relation
    ///说明: 模块角色关系
    /// </summary>
    public class WebPlatForm_Module_Relation
    {
        private int m_nID;
        /// <summary>
        /// 
        /// </summary>
        public int nID
        {
            get { return m_nID; }
            set { m_nID = value; }
        }
        private string m_strID;
        /// <summary>
        /// 
        /// </summary>
        public string strID
        {
            get { return m_strID; }
            set { m_strID = value; }
        }
        private string m_strModuleID;
        /// <summary>
        /// 模块id
        /// </summary>
        public string strModuleID
        {
            get { return m_strModuleID; }
            set { m_strModuleID = value; }
        }
        private string m_strRoleID;
        /// <summary>
        /// 角色id
        /// </summary>
        public string strRoleID
        {
            get { return m_strRoleID; }
            set { m_strRoleID = value; }
        }
        private string m_strPowerID;
        /// <summary>
        /// 模块权限id
        /// </summary>
        public string strPowerID
        {
            get { return m_strPowerID; }
            set { m_strPowerID = value; }
        }
    }
    /// <summary>
    ///类名: WebPlatForm_Module_RelationList
    ///说明: 模块角色关系列表类
    /// </summary>
    public class WebPlatForm_Module_RelationList : List<WebPlatForm_Module_Relation>
    {
        public WebPlatForm_Module_RelationList()
        { }
    }
}