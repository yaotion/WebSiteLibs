using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using TF.DB.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.YA.Sync
{
  public  class ImportDutyUserDal
    {
      public int addDutyUser(DutyUser lstuser)
      {


          int ret = 0;

          string sql = "insert into TAB_Org_DutyUser(strDutyGUID,strDutyNumber,strDutyName,strPassword,nRoleID) values(@bh,@strDutyNumber,@strDutyName,@strPassword,@nRoleID)";
          string bh = Guid.NewGuid().ToString();
          SqlParameter[] sqlparam = new SqlParameter[]{
               new SqlParameter("bh",bh),
                new SqlParameter("strDutyNumber",lstuser.DutyUserNumber),
                 new SqlParameter("strDutyName",lstuser.DutyUserName),
                  new SqlParameter("strPassword",lstuser.Password),
                   new SqlParameter("nRoleID",lstuser.RoleID) 
 
                };


          ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
          return ret;
      }

      public int UpdateDutyUsers(DutyUser lstuser)
      {

          string sql = "update TAB_Org_DutyUser set strDutyName=@strDutyName, strPassword= @strPassword ,nRoleID =@nRoleID  where strDutyNumber=@strDutyNumber";
          SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("strDutyNumber",lstuser.DutyUserNumber),
                 new SqlParameter("strDutyName",lstuser.DutyUserName),
                  new SqlParameter("strPassword",lstuser.Password),
                   new SqlParameter("nRoleID",lstuser.RoleID) 
 
                };
            int  ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
          return ret;
      }
      public int DeleteDutyUsers( DutyUser  lstuser)
      {
        
              string sql = "delete from TAB_Org_DutyUser where strDutyNumber=@strDutyNumber";
              SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("strDutyNumber",lstuser.DutyUserNumber)
           };

              int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
          return ret;
      }
      public List<DutyUser> GetDutyUserAll()
      {
          List<DutyUser> LstDutyUser = new List<DutyUser>();
          string sql = "select strDutyNumber,strDutyName,strPassword,nRoleID  from TAB_Org_DutyUser";
        var dt=  SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
        for (int i = 0; i < dt.Rows.Count;i++ )
        {
            DutyUser ObjdutyUser = new DutyUser();
            ObjdutyUser.DutyUserName = dt.Rows[i]["strDutyName"].ToString();
            ObjdutyUser.DutyUserNumber = dt.Rows[i]["strDutyNumber"].ToString();
            ObjdutyUser.Password = dt.Rows[i]["strPassword"].ToString();
            ObjdutyUser.RoleID = dt.Rows[i]["nRoleID"].ToString();
            LstDutyUser.Add(ObjdutyUser);
        }
        return LstDutyUser;

      }
    }
}
