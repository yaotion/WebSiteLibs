using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.WebPlatForm.Entry
{
    public class loginReplay
    {
        private int m_nSuccess;
        /// <summary>
        /// 是否成功 0失败 1成功
        /// </summary>
        public int nSuccess
        {
            get { return m_nSuccess; }
            set { m_nSuccess = value; }
        }

        private string m_tokenID;
        /// <summary>
        /// tokenID
        /// </summary>
        public string tokenID
        {
            get { return m_tokenID; }
            set { m_tokenID = value; }
        }

        private string m_strResult;
        /// <summary>
        /// 失败原因
        /// </summary>
        public string strResult
        {
            get { return m_strResult; }
            set { m_strResult = value; }
        }
    }
}
