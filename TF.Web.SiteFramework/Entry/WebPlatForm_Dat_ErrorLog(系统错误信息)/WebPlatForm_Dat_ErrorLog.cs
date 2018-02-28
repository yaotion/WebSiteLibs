using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
  /// <summary>
  ///����: WebPlatForm_Dat_ErrorLog
  ///˵��: ϵͳ������Ϣ
  /// </summary>
  public class WebPlatForm_Dat_ErrorLog
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
    private string m_strType;
    /// <summary>
    /// ���� jsError��serverError
    /// </summary>
    public string strType
    {
      get {return m_strType;}
      set {m_strType = value;}
    }
    private string m_strClientIP;
    /// <summary>
    /// �ͻ���IP����
    /// </summary>
    public string strClientIP
    {
      get {return m_strClientIP;}
      set {m_strClientIP = value;}
    }
    private string m_strErrorContent;
    /// <summary>
    /// ��������
    /// </summary>
    public string strErrorContent
    {
      get {return m_strErrorContent;}
      set {m_strErrorContent = value;}
    }
    private DateTime? m_dtAddTime;
    /// <summary>
    /// ���ʱ��
    /// </summary>
    public DateTime? dtAddTime
    {
      get {return m_dtAddTime;}
      set {m_dtAddTime = value;}
    }
    private string m_strAddNumber;
    /// <summary>
    /// ����˹���
    /// </summary>
    public string strAddNumber
    {
        get { return m_strAddNumber; }
        set { m_strAddNumber = value; }
    }
    private string m_strAddName;
    /// <summary>
    /// ���������
    /// </summary>
    public string strAddName
    {
        get { return m_strAddName; }
        set { m_strAddName = value; }
    }
  }
  /// <summary>
  ///����: WebPlatForm_Dat_ErrorLogList
  ///˵��: ϵͳ������Ϣ�б���
  /// </summary>
  public class WebPlatForm_Dat_ErrorLogList : List<WebPlatForm_Dat_ErrorLog>
  {
    public WebPlatForm_Dat_ErrorLogList()
    {}
  }
}
