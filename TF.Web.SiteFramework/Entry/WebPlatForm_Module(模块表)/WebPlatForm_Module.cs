using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
  /// <summary>
  ///类名: WebPlatForm_Module
  ///说明: 模块
  /// </summary>
  public class WebPlatForm_Module
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
    /// 模块id
    /// </summary>
    public string strID
    {
      get {return m_strID;}
      set {m_strID = value;}
    }
    private string m_strModuleName;
    /// <summary>
    /// 模块名称
    /// </summary>
    public string strModuleName
    {
      get {return m_strModuleName;}
      set {m_strModuleName = value;}
    }

    private int m_nEnable;
    /// <summary>
    /// 启用 禁用
    /// </summary>
    public int nEnable
    {
        get { return m_nEnable; }
        set { m_nEnable = value; }
    }

  }
  /// <summary>
  ///类名: WebPlatForm_ModuleList
  ///说明: 模块列表类
  /// </summary>
  public class WebPlatForm_ModuleList : List<WebPlatForm_Module>
  {
    public WebPlatForm_ModuleList()
    {}
  }
}
