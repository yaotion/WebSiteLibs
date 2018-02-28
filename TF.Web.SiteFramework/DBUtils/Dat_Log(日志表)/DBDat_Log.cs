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
	///类名: Dat_WebLogQueryCondition
	///说明: 车型查询条件类
	/// </summary>
	public class Dat_WebLogQueryCondition
	{
		public int page = 0;
		public int rows = 0;
		//
		public int nID = 0;
		//
        public string strID = "";
		//工号
        public string strTrianManNumber = "";
		//类型
		public int nType = 0;
		//时间
		public DateTime? dtPostTime;
		//访问地址
        public string strPageUrl = "";
        public string dtBeginTime = "";
        public string dtEndTime = "";
		public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
		{
			SqlCondition = new StringBuilder();
			SqlCondition.Append(nID != 0 ? " and nID = @nID" : "");
			SqlCondition.Append(strID != "" ? " and strID = @strID" : "");
            SqlCondition.Append(strTrianManNumber != "" ? " and strTrianManNumber = @strTrianManNumber" : "");
			SqlCondition.Append(nType != 0 ? " and nType = @nType" : "");
			SqlCondition.Append(dtPostTime != null ? " and dtPostTime = @dtPostTime" : "");
            SqlCondition.Append(strPageUrl != "" ? " and strPageUrl = @strPageUrl" : "");
            SqlCondition.Append(dtBeginTime != "" ? " and dtPostTime >= @dtBeginTime" : "");
            SqlCondition.Append(dtEndTime != "" ? " and dtPostTime <=@dtEndTime" : "");
			SqlParameter[] sqlParams ={
					new SqlParameter("nID",nID),
					new SqlParameter("strID",strID),
					new SqlParameter("strTrianManNumber",strTrianManNumber),
					new SqlParameter("nType",nType),
					new SqlParameter("dtPostTime",dtPostTime),
					new SqlParameter("strPageUrl",strPageUrl),
                    new SqlParameter("dtBeginTime",dtBeginTime),
                    new SqlParameter("dtEndTime",dtEndTime)};
			Params = sqlParams;
		}
	}
	/// <summary>
	///类名: DBDat_WebLog
	///说明: web数据操作类
	/// </summary>
	public class DBDat_WebLog : DBOperator
	{
        public DBDat_WebLog(string ConnectionString) : base(ConnectionString) { }
		#region 增删改
		/// <summary>
		/// 添加数据
		/// </summary>
        public int Add(Dat_WebLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WebPlatForm_Dat_WebLog");
            strSql.Append("(strTrianManNumber,nType,dtPostTime,strPageUrl,strPageName,strClientIP)");
            strSql.Append("values(@strTrianManNumber,@nType,@dtPostTime,@strPageUrl,@strPageName,@strClientIP)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@strTrianManNumber", model.strTrianManNumber),
					new SqlParameter("@nType", model.nType),
					new SqlParameter("@dtPostTime", model.dtPostTime),
					new SqlParameter("@strPageUrl", model.strPageUrl),
					new SqlParameter("@strPageName", model.strPageName),
					new SqlParameter("@strClientIP", model.strClientIP)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters));
		}
		/// <summary>
		/// 更新数据
		/// </summary>
		public bool Update(Dat_WebLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Update WebPlatForm_Dat_WebLog set ");
            strSql.Append(" strTrianManNumber = @strTrianManNumber, ");
			strSql.Append(" nType = @nType, ");
			strSql.Append(" dtPostTime = @dtPostTime, ");
            strSql.Append(" strPageUrl = @strPageUrl, ");
            strSql.Append(" strPageName = @strPageName,");
            strSql.Append(" strClientIP = @strClientIP");
			strSql.Append(" where strID = @strID ");

			SqlParameter[] parameters = {
					new SqlParameter("@strID", model.strID),
						new SqlParameter("@strTrianManNumber", model.strTrianManNumber),
					new SqlParameter("@nType", model.nType),
					new SqlParameter("@dtPostTime", model.dtPostTime),
					new SqlParameter("@strPageUrl", model.strPageUrl),
					new SqlParameter("@strPageName", model.strPageName),
					new SqlParameter("@strClientIP", model.strClientIP)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		/// <summary>
		/// 删除数据
		/// </summary>
		public bool Delete(string strID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WebPlatForm_Dat_WebLog ");
			strSql.Append(" where strID = @strID ");
			SqlParameter[] parameters = {
					new SqlParameter("strID",strID)};

			return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql.ToString(), parameters) > 0;
		}
		#endregion
		/// <summary>
		/// 检查数据是否存在
		/// </summary>
		public bool Exists(Dat_WebLog _Dat_WebLog)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from WebPlatForm_Dat_WebLog where strID=@strID");
			 SqlParameter[] parameters = {
					 new SqlParameter("strID",_Dat_WebLog.strID)};

			return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), parameters)) > 0;
		}
		/// <summary>
		/// 获得数据DataTable
		/// </summary>
		public DataTable GetDataTable(Dat_WebLogQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
				strSql.Append(" FROM WebPlatForm_Dat_WebLog where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from WebPlatForm_Dat_WebLog where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Dat_WebLog where  1=1 " + strSqlOption.ToString() + " order by dtPostTime desc) order by dtPostTime desc");
			}
			return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
		}
		/// <summary>
		/// 获得数据List
		/// </summary>
		public Dat_WebLogList GetDataList(Dat_WebLogQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			if (QueryCondition.page == 0)
			{
				strSql.Append("select * ");
                strSql.Append(" FROM WebPlatForm_Dat_WebLog where 1=1 " + strSqlOption.ToString());
			}else
			{
				strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from WebPlatForm_Dat_WebLog where 1 = 1 "+
				strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
                " nID from WebPlatForm_Dat_WebLog where  1=1 " + strSqlOption.ToString() + " order by dtPostTime desc) order by dtPostTime desc");
			}
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			Dat_WebLogList list = new Dat_WebLogList();
			foreach (DataRow dr in dt.Rows)
			{
				Dat_WebLog _Dat_WebLog = new Dat_WebLog();
				DataRowToModel(_Dat_WebLog,dr);
				list.Add(_Dat_WebLog);
			}
			return list;
		}
		/// <summary>
		/// 获得记录总数
		/// </summary>
		public int GetDataCount(Dat_WebLogQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) ");
            strSql.Append(" FROM WebPlatForm_Dat_WebLog where 1=1" + strSqlOption.ToString());
			return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams));
		}
		/// <summary>
		/// 获得一个实体对象
		/// </summary>
		public Dat_WebLog GetModel(Dat_WebLogQueryCondition QueryCondition)
		{
			SqlParameter[] sqlParams;
			StringBuilder strSqlOption = new StringBuilder();
			QueryCondition.OutPut(out strSqlOption, out sqlParams);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 * ");
            strSql.Append(" FROM WebPlatForm_Dat_WebLog where 1=1 " + strSqlOption.ToString());
			DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
			Dat_WebLog _Dat_WebLog = null;
			if (dt.Rows.Count > 0)
			{
				_Dat_WebLog = new Dat_WebLog();
				DataRowToModel(_Dat_WebLog,dt.Rows[0]);
			}
			return _Dat_WebLog;
		}
		/// <summary>
		/// 读取DataRow数据到Model中
		/// </summary>
		private void DataRowToModel(Dat_WebLog model,DataRow dr)
		{
			model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strID = ObjectConvertClass.static_ext_string(dr["strID"]);
            model.strTrianManNumber = ObjectConvertClass.static_ext_string(dr["strTrianManNumber"]);
			model.nType = ObjectConvertClass.static_ext_int(dr["nType"]);
			model.dtPostTime = ObjectConvertClass.static_ext_date(dr["dtPostTime"]);
            model.strPageUrl = ObjectConvertClass.static_ext_string(dr["strPageUrl"]);
            model.strPageName = ObjectConvertClass.static_ext_string(dr["strPageName"]);
            model.strClientIP = ObjectConvertClass.static_ext_string(dr["strClientIP"]);
		}
	}
}
