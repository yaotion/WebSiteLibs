using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
	/// <summary>
	///类名: Dat_WebLog
	///说明: 车型
	/// </summary>
    public class Dat_WebLog
	{
		private int m_nID;
		/// <summary>
		/// 
		/// </summary>
		public int nID
		{
			get {return m_nID;}
			set {m_nID = value;}
		}
		private string m_strID;
		/// <summary>
		/// 
		/// </summary>
        public string strID
		{
			get {return m_strID;}
			set {m_strID = value;}
		}
        private string m_strTrianManNumber;
		/// <summary>
		/// 工号
		/// </summary>
        public string strTrianManNumber
		{
            get { return m_strTrianManNumber; }
            set { m_strTrianManNumber = value; }
		}
		private int m_nType;
		/// <summary>
		/// 类型
		/// </summary>
		public int nType
		{
			get {return m_nType;}
			set {m_nType = value;}
		}
		private DateTime? m_dtPostTime;
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime? dtPostTime
		{
			get {return m_dtPostTime;}
			set {m_dtPostTime = value;}
		}
        private string m_strPageUrl;
		/// <summary>
        /// 访问地址
		/// </summary>
        public string strPageUrl
		{
            get { return m_strPageUrl; }
            set { m_strPageUrl = value; }
		}
        private string m_strPageName;
        /// <summary>
        /// 页面名称
        /// </summary>
        public string strPageName
        {
            get { return m_strPageName; }
            set { m_strPageName = value; }
        }
        private string m_strClientIP;
        /// <summary>
        /// ip地址
        /// </summary>
        public string strClientIP
        {
            get { return m_strClientIP; }
            set { m_strClientIP = value; }
        }
	}
	/// <summary>
    ///类名: Dat_WebLogList
	///说明: 车型列表类
	/// </summary>
    public class Dat_WebLogList : List<Dat_WebLog>
	{
        public Dat_WebLogList()
		{}
	}
}
