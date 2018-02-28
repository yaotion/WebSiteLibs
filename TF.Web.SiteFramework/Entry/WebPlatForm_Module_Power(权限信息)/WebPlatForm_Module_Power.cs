using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
  /// <summary>
  ///类名: WebPlatForm_Module_Power
  ///说明: 模块权限
  /// </summary>
  public class WebPlatForm_Module_Power
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
    private string m_strPowerName;
    /// <summary>
    /// 权限名称
    /// </summary>
    public string strPowerName
    {
      get {return m_strPowerName;}
      set {m_strPowerName = value;}
    }
    private string m_strPowerID;
    /// <summary>
    /// 权限ID
    /// </summary>
    public string strPowerID
    {
      get {return m_strPowerID;}
      set {m_strPowerID = value;}
    }
    private string m_strModuleID;
    /// <summary>
    /// 模块ID
    /// </summary>
    public string strModuleID
    {
      get {return m_strModuleID;}
      set {m_strModuleID = value;}
    }
  }
  /// <summary>
  ///类名: WebPlatForm_Module_PowerList
  ///说明: 模块权限列表类
  /// </summary>
  public class WebPlatForm_Module_PowerList : List<WebPlatForm_Module_Power>
  {
    public WebPlatForm_Module_PowerList()
    {}
  }
}
