using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using TF.DB.DBUtility;
using TF.CommonUtility;
using System.Collections.Generic;
using TF.WebPlatForm.Entry;

namespace TF.WebPlatForm.DBUtils
{
  /// <summary>
  ///����: WebPlatForm_Module_PowerQueryCondition
  ///˵��: ģ��Ȩ�޲�ѯ������
  /// </summary>
  public class WebPlatForm_Module_PowerQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    public string sort = "";
    public string order = "";
    //
    public int? nID = null;
    //
    public string strID = "";
    //Ȩ������
    public string strPowerName = "";
    //Ȩ��ID
    public string strPowerID = "";
    //ģ��ID
    public string strModuleID = "";
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
      SqlCondition.Append(strPowerName != "" ? " and strPowerName = @strPowerName" : "");
      SqlCondition.Append(strPowerID != "" ? " and strPowerID = @strPowerID" : "");
      SqlCondition.Append(strModuleID != "" ? " and strModuleID = @strModuleID" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strID",strID),
          new SqlParameter("strPowerName",strPowerName),
          new SqlParameter("strPowerID",strPowerID),
          new SqlParameter("strModuleID",strModuleID)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///����: DBWebPlatForm_Module_Power
  ///˵��: ģ��Ȩ�����ݲ�����
  /// </summary>
  public class DBWebPlatForm_Module_Power : DBOperator
  {
    public DBWebPlatForm_Module_Power(string ConnectionString) : base(ConnectionString) { }
    #region ��ɾ��
    /// <summary>
    /// �������
    /// </summary>
    public int Add(WebPlatForm_Module_Power model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into WebPlatForm_Module_Power");
      strSql.Append("(strPowerName,strPowerID,strModuleID)");
      strSql.Append("values(@strPowerName,@strPowerID,@strModuleID)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strPowerName", model.strPowerName),
          new SqlParameter("@strPowerID", model.strPowerID),
          new SqlParameter("@strModuleID", model.strModuleID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// ��������
    /// </summary>
    public bool Update(WebPlatForm_Module_Power model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update WebPlatForm_Module_Power set ");
      strSql.Append(" strPowerName = @strPowerName, ");
      strSql.Append(" strPowerID = @strPowerID");
      strSql.Append(" where strID = @strID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strPowerName", model.strPowerName),
          new SqlParameter("@strPowerID", model.strPowerID)};

      return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    /// <summary>
    /// ɾ������
    /// </summary>
    public bool Delete(string strID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from WebPlatForm_Module_Power ");
      if (strID != "")
      {
          strSql.Append(" where strID = @strID ");
      }
      SqlParameter[] parameters = {
          new SqlParameter("strID",strID)};

      return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }

    /// <summary>
    /// ɾ������
    /// </summary>
    public bool DeleteByPowerID(string strPowerID)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from WebPlatForm_Module_Power ");
        if (strPowerID != "")
        {
            strSql.Append(" where strPowerID = @strPowerID ");
        }
        SqlParameter[] parameters = {
          new SqlParameter("strPowerID",strPowerID)};

        return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }

    /// <summary>
    /// ɾ��ģ��id������Ȩ��
    /// </summary>
    public bool DeleteByModuleID(string strModuleID)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from WebPlatForm_Module_Power ");
        strSql.Append(" where strModuleID = @strModuleID ");
        SqlParameter[] parameters = {
          new SqlParameter("strModuleID",strModuleID)};

        return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }  
    #endregion
    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    public bool Exists(WebPlatForm_Module_Power _WebPlatForm_Module_Power)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from WebPlatForm_Module_Power where strID=@strID");
        
       SqlParameter[] parameters = {
           new SqlParameter("strID",_WebPlatForm_Module_Power.strID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }

    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    public bool ExistsByPowerID(WebPlatForm_Module_Power _WebPlatForm_Module_Power)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select count(*) from WebPlatForm_Module_Power where strPowerID=@strPowerID");

        SqlParameter[] parameters = {
           new SqlParameter("strPowerID",_WebPlatForm_Module_Power.strPowerID)};

        return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }

    /// <summary>
    /// �������DataTable
    /// </summary>
    public DataTable GetDataTable(WebPlatForm_Module_PowerQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM WebPlatForm_Module_Power where 1=1 " + strSqlOption.ToString());
        strSql.Append(" order by ");
        strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
        strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from WebPlatForm_Module_Power where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from WebPlatForm_Module_Power where  1=1 " + strSqlOption.ToString());
        strSql.Append(" order by ");
        strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
        strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
        strSql.Append(") order by ");
        strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
        strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
      }
      return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }


    /// <summary>
    /// �������DataTable
    /// </summary>
    public DataTable GetDataHasWebInfo(WebPlatForm_Module_PowerQueryCondition QueryCondition)
    {
        SqlParameter[] sqlParams;
        StringBuilder strSqlOption = new StringBuilder();
        QueryCondition.OutPut(out strSqlOption, out sqlParams);
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select a.*, (select Count(1) from WebPlatForm_Module_Information where strID=a.strPowerID) as webInfoCount ");
            strSql.Append(" FROM WebPlatForm_Module_Power a where 1=1 " + strSqlOption.ToString());
            strSql.Append(" order by ");
            strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
            strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
       
        return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }

    /// <summary>
    /// �������List
    /// </summary>
    public WebPlatForm_Module_PowerList GetDataList(WebPlatForm_Module_PowerQueryCondition QueryCondition)
    {
      DataTable dt = GetDataTable(QueryCondition);
      WebPlatForm_Module_PowerList list = new WebPlatForm_Module_PowerList();
      foreach (DataRow dr in dt.Rows)
      {
        WebPlatForm_Module_Power _WebPlatForm_Module_Power = new WebPlatForm_Module_Power();
        DataRowToModel(_WebPlatForm_Module_Power,dr);
        list.Add(_WebPlatForm_Module_Power);
      }
      return list;
    }
    /// <summary>
    /// ��ü�¼����
    /// </summary>
    public int GetDataCount(WebPlatForm_Module_PowerQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM WebPlatForm_Module_Power where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// ���һ��ʵ�����
    /// </summary>
    public WebPlatForm_Module_Power GetModel(WebPlatForm_Module_PowerQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM WebPlatForm_Module_Power where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      WebPlatForm_Module_Power _WebPlatForm_Module_Power = null;
      if (dt.Rows.Count > 0)
      {
        _WebPlatForm_Module_Power = new WebPlatForm_Module_Power();
        DataRowToModel(_WebPlatForm_Module_Power,dt.Rows[0]);
      }
      return _WebPlatForm_Module_Power;
    }
    /// <summary>
    /// ��ȡDataRow���ݵ�Model��
    /// </summary>
    private void DataRowToModel(WebPlatForm_Module_Power model,DataRow dr)
    {
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
      model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
      model.strPowerName = ObjectConvertClass.static_ext_string(dr["strPowerName"]);
      model.strPowerID = ObjectConvertClass.static_ext_string(dr["strPowerID"]);
      model.strModuleID = ObjectConvertClass.static_ext_string(dr["strModuleID"]);
    }
  }
}
