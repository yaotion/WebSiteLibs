using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
  /// <summary>
  ///类名: WebPlatForm_Dat_ErrorLog
  ///说明: 系统错误信息
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
    /// 类型 jsError、serverError
    /// </summary>
    public string strType
    {
      get {return m_strType;}
      set {m_strType = value;}
    }
    private string m_strClientIP;
    /// <summary>
    /// 客户端IP名称
    /// </summary>
    public string strClientIP
    {
      get {return m_strClientIP;}
      set {m_strClientIP = value;}
    }
    private string m_strErrorContent;
    /// <summary>
    /// 错误内容
    /// </summary>
    public string strErrorContent
    {
      get {return m_strErrorContent;}
      set {m_strErrorContent = value;}
    }
    private DateTime? m_dtAddTime;
    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime? dtAddTime
    {
      get {return m_dtAddTime;}
      set {m_dtAddTime = value;}
    }
    private string m_strAddNumber;
    /// <summary>
    /// 添加人工号
    /// </summary>
    public string strAddNumber
    {
        get { return m_strAddNumber; }
        set { m_strAddNumber = value; }
    }
    private string m_strAddName;
    /// <summary>
    /// 添加人姓名
    /// </summary>
    public string strAddName
    {
        get { return m_strAddName; }
        set { m_strAddName = value; }
    }
  }
  /// <summary>
  ///类名: WebPlatForm_Dat_ErrorLogList
  ///说明: 系统错误信息列表类
  /// </summary>
  public class WebPlatForm_Dat_ErrorLogList : List<WebPlatForm_Dat_ErrorLog>
  {
    public WebPlatForm_Dat_ErrorLogList()
    {}
  }
}
