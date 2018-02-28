using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace TF.WebPlatForm.Entry
{
  /// <summary>
  ///����: WebPlatForm_Module
  ///˵��: ģ��
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
    /// ģ��id
    /// </summary>
    public string strID
    {
      get {return m_strID;}
      set {m_strID = value;}
    }
    private string m_strModuleName;
    /// <summary>
    /// ģ������
    /// </summary>
    public string strModuleName
    {
      get {return m_strModuleName;}
      set {m_strModuleName = value;}
    }

    private int m_nEnable;
    /// <summary>
    /// ���� ����
    /// </summary>
    public int nEnable
    {
        get { return m_nEnable; }
        set { m_nEnable = value; }
    }

  }
  /// <summary>
  ///����: WebPlatForm_ModuleList
  ///˵��: ģ���б���
  /// </summary>
  public class WebPlatForm_ModuleList : List<WebPlatForm_Module>
  {
    public WebPlatForm_ModuleList()
    {}
  }
}
