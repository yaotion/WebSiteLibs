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
  ///����: WebPlatForm_Dat_ErrorLogQueryCondition
  ///˵��: ϵͳ������Ϣ��ѯ������
  /// </summary>
  public class WebPlatForm_Dat_ErrorLogQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    public string sort = "";
    public string order = "";
    //
    public int? nID = null;
    //
    public string strID = "";
    //���� jsError��serverError
    public string strType = "";
    //�ͻ���IP����
    public string strClientIP = "";
    //��������
    public string strErrorContent = "";
    //���ʱ��
    public DateTime? dtAddTime;
    //����˹���
    public string strAddNumber = "";
    //���������
    public string strAddName = "";

      //��ʼʱ��
    public string strBeginTime = "";
      //����ʱ��
    public string strEndTime = "";

    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
      SqlCondition.Append(strType != "" ? " and strType = @strType" : "");
      SqlCondition.Append(strClientIP != "" ? " and strClientIP = @strClientIP" : "");
      SqlCondition.Append(strErrorContent != "" ? " and strErrorContent = @strErrorContent" : "");
      SqlCondition.Append(dtAddTime != null ? " and dtAddTime = @dtAddTime" : "");
      SqlCondition.Append(strAddNumber != "" ? " and strAddNumber = @strAddNumber" : "");
      SqlCondition.Append(strAddName != "" ? " and strAddName = @strAddName" : "");

      SqlCondition.Append(strBeginTime != "" ? " and dtAddTime >= @strBeginTime" : "");
      SqlCondition.Append(strEndTime != "" ? " and dtAddTime <= @strEndTime" : "");

      SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strID",strID),
          new SqlParameter("strType",strType),
          new SqlParameter("strClientIP",strClientIP),
          new SqlParameter("strErrorContent",strErrorContent),

          new SqlParameter("strBeginTime",strBeginTime),
          new SqlParameter("strEndTime",strEndTime),

          new SqlParameter("dtAddTime",dtAddTime),
          new SqlParameter("strAddNumber",strAddNumber),
          new SqlParameter("strAddName",strAddName)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///����: DBWebPlatForm_Dat_ErrorLog
  ///˵��: ϵͳ������Ϣ���ݲ�����
  /// </summary>
  public class DBWebPlatForm_Dat_ErrorLog : DBOperator
  {
    public DBWebPlatForm_Dat_ErrorLog(string ConnectionString) : base(ConnectionString) { }
    #region ��ɾ��
    /// <summary>
    /// �������
    /// </summary>
    public int Add(WebPlatForm_Dat_ErrorLog model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into WebPlatForm_Dat_ErrorLog");
      strSql.Append("(strID,strType,strClientIP,strErrorContent,dtAddTime,strAddNumber,strAddName)");
      strSql.Append("values(@strID,@strType,@strClientIP,@strErrorContent,@dtAddTime,@strAddNumber,@strAddName)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strType", model.strType),
          new SqlParameter("@strClientIP", model.strClientIP),
          new SqlParameter("@strErrorContent", model.strErrorContent),
          new SqlParameter("@dtAddTime", model.dtAddTime),
          new SqlParameter("@strAddNumber", model.strAddNumber),
          new SqlParameter("@strAddName", model.strAddName)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// ��������
    /// </summary>
    public bool Update(WebPlatForm_Dat_ErrorLog model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update WebPlatForm_Dat_ErrorLog set ");
      strSql.Append(" strID = @strID, ");
      strSql.Append(" strType = @strType, ");
      strSql.Append(" strClientIP = @strClientIP, ");
      strSql.Append(" strErrorContent = @strErrorContent, ");
      strSql.Append(" dtAddTime = @dtAddTime, ");
      strSql.Append(" dtAddNumber = @dtAddNumber, ");
      strSql.Append(" dtAddName = @dtAddName ");
      strSql.Append(" where strID = @strID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strID", model.strID),
          new SqlParameter("@strType", model.strType),
          new SqlParameter("@strClientIP", model.strClientIP),
          new SqlParameter("@strErrorContent", model.strErrorContent),
          new SqlParameter("@dtAddTime", model.dtAddTime),
          new SqlParameter("@strAddNumber", model.strAddNumber),
          new SqlParameter("@strAddName", model.strAddName)};

      return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    /// <summary>
    /// ɾ������
    /// </summary>
    public bool Delete(string strID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from WebPlatForm_Dat_ErrorLog ");
      strSql.Append(" where strID = @strID ");
      SqlParameter[] parameters = {
          new SqlParameter("strID",strID)};

      return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    public bool Exists(WebPlatForm_Dat_ErrorLog _WebPlatForm_Dat_ErrorLog)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from WebPlatForm_Dat_ErrorLog where strID=@strID");
       SqlParameter[] parameters = {
           new SqlParameter("strID",_WebPlatForm_Dat_ErrorLog.strID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// �������DataTable
    /// </summary>
    public DataTable GetDataTable(WebPlatForm_Dat_ErrorLogQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM WebPlatForm_Dat_ErrorLog where 1=1 " + strSqlOption.ToString());
        strSql.Append(" order by ");
        strSql.Append(QueryCondition.sort == "" ? " nID" : QueryCondition.sort);
        strSql.Append(QueryCondition.order == "" ? " desc" : " " + QueryCondition.order);
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from WebPlatForm_Dat_ErrorLog where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from WebPlatForm_Dat_ErrorLog where  1=1 " + strSqlOption.ToString());
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
    /// �������List
    /// </summary>
    public WebPlatForm_Dat_ErrorLogList GetDataList(WebPlatForm_Dat_ErrorLogQueryCondition QueryCondition)
    {
      DataTable dt = GetDataTable(QueryCondition);
      WebPlatForm_Dat_ErrorLogList list = new WebPlatForm_Dat_ErrorLogList();
      foreach (DataRow dr in dt.Rows)
      {
        WebPlatForm_Dat_ErrorLog _WebPlatForm_Dat_ErrorLog = new WebPlatForm_Dat_ErrorLog();
        DataRowToModel(_WebPlatForm_Dat_ErrorLog,dr);
        list.Add(_WebPlatForm_Dat_ErrorLog);
      }
      return list;
    }
    /// <summary>
    /// ��ü�¼����
    /// </summary>
    public int GetDataCount(WebPlatForm_Dat_ErrorLogQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM WebPlatForm_Dat_ErrorLog where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// ���һ��ʵ�����
    /// </summary>
    public WebPlatForm_Dat_ErrorLog GetModel(WebPlatForm_Dat_ErrorLogQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM WebPlatForm_Dat_ErrorLog where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      WebPlatForm_Dat_ErrorLog _WebPlatForm_Dat_ErrorLog = null;
      if (dt.Rows.Count > 0)
      {
        _WebPlatForm_Dat_ErrorLog = new WebPlatForm_Dat_ErrorLog();
        DataRowToModel(_WebPlatForm_Dat_ErrorLog,dt.Rows[0]);
      }
      return _WebPlatForm_Dat_ErrorLog;
    }
    /// <summary>
    /// ��ȡDataRow���ݵ�Model��
    /// </summary>
    private void DataRowToModel(WebPlatForm_Dat_ErrorLog model,DataRow dr)
    {
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
      model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
      model.strType = ObjectConvertClass.static_ext_string(dr["strType"]);
      model.strClientIP = ObjectConvertClass.static_ext_string(dr["strClientIP"]);
      model.strErrorContent = ObjectConvertClass.static_ext_string(dr["strErrorContent"]);
      model.dtAddTime = ObjectConvertClass.static_ext_date(dr["dtAddTime"]);
      model.strAddNumber = ObjectConvertClass.static_ext_string(dr["strAddNumber"]);
      model.strAddName = ObjectConvertClass.static_ext_string(dr["strAddName"]);
    }
  }
}
