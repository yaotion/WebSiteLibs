using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
	/// <summary>
	///����: Dat_WebLog
	///˵��: ����
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
		/// ����
		/// </summary>
        public string strTrianManNumber
		{
            get { return m_strTrianManNumber; }
            set { m_strTrianManNumber = value; }
		}
		private int m_nType;
		/// <summary>
		/// ����
		/// </summary>
		public int nType
		{
			get {return m_nType;}
			set {m_nType = value;}
		}
		private DateTime? m_dtPostTime;
		/// <summary>
		/// ʱ��
		/// </summary>
		public DateTime? dtPostTime
		{
			get {return m_dtPostTime;}
			set {m_dtPostTime = value;}
		}
        private string m_strPageUrl;
		/// <summary>
        /// ���ʵ�ַ
		/// </summary>
        public string strPageUrl
		{
            get { return m_strPageUrl; }
            set { m_strPageUrl = value; }
		}
        private string m_strPageName;
        /// <summary>
        /// ҳ������
        /// </summary>
        public string strPageName
        {
            get { return m_strPageName; }
            set { m_strPageName = value; }
        }
        private string m_strClientIP;
        /// <summary>
        /// ip��ַ
        /// </summary>
        public string strClientIP
        {
            get { return m_strClientIP; }
            set { m_strClientIP = value; }
        }
	}
	/// <summary>
    ///����: Dat_WebLogList
	///˵��: �����б���
	/// </summary>
    public class Dat_WebLogList : List<Dat_WebLog>
	{
        public Dat_WebLogList()
		{}
	}
}
