using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace TF.YA.Sync
{
   public class ImportDutyUserBll
    {
       ImportDutyUserDal dutyuserdal = new ImportDutyUserDal();
       public List<DutyUser> GetRemoteDutyUsers(string url)
       {
           
           string msg = "";
           if (string.IsNullOrEmpty(url))
               url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.TF.LCOrgIF.GetAllDutyUsers&Data={'PageIndex':'1','PageCount':'20000'}";
           else

               url = url + "/AshxService/QueryProcess.ashx?DataType=TF.TF.LCOrgIF.GetAllDutyUsers&Data={'PageIndex':'1','PageCount':'20000'}";
           String bret = HttpCommon.GetStr(url, out msg);
          DutyUserAll allusers =  JsonConvert.DeserializeObject< DutyUserAll>(bret);
          ImportDutyUserDal persondal = new ImportDutyUserDal();
          
           if (allusers.Success == 1)
           {
               return allusers.Data.DutyUsers;
               
           }else
           {
               return  null;
           }
              
       }
       /// <summary>
       /// 处理数据同步信息
       /// </summary>
       /// <param name="url"></param>
       /// <returns></returns>
       public int DoDutyUser(string url)
       {
           List<DutyUser> LstRemtUser = GetRemoteDutyUsers(url);
           for (int i = 0; i < LstRemtUser.Count; i++)
           {
               int nRoleID;
               if (!Int32.TryParse(LstRemtUser[i].RoleID,out nRoleID))
               {
                   nRoleID = 1;
               }
               LstRemtUser[i].RoleID = nRoleID.ToString();
           }
           List<DutyUser> localuser = GetLocalDutyUsers();
           CompareUtils<DutyUser> cmp = new CompareUtils<DutyUser>();
           if (cmp.Compare(localuser, LstRemtUser))
           {
               return 0;
           }
           int count = 0;
           //添加新计划
           
          count=    AddDutyUsers(cmp.NewList);
           //修改已有计划
          count =count+ UpdateDutyUsers(cmp.UpdateList);
            
           //删除无用计划
         count=count+  DeleteDutyUsers(cmp.RemoveList);
           
           return count;
       }
       /// <summary>
       /// 获取本地信息
       /// </summary>
       /// <returns></returns>
       public List<DutyUser> GetLocalDutyUsers()
       {
            List<DutyUser> lstDutyUser=dutyuserdal.GetDutyUserAll();
            return lstDutyUser;

       }
       /// <summary>
       /// 增加
       /// </summary>
       /// <param name="dutyuser"></param>
       /// <returns></returns>
       public int AddDutyUsers( List<DutyUser>  dutyuser)
       {
           int ret = 0;
           for(int i=0;i<dutyuser.Count ;i++)
           {

          
            ret=ret+ dutyuserdal.addDutyUser(dutyuser[i]);
           }
          return ret;
       }
       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="dutyuser"></param>
       /// <returns></returns>
       public int UpdateDutyUsers(List<DutyUser> dutyuser)
       {
           int ret = 0;
           for (int i = 0; i < dutyuser.Count; i++)
           {
                 ret =ret+ dutyuserdal.UpdateDutyUsers(dutyuser[i]);
           }
           return ret;
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="dutyuser"></param>
       /// <returns></returns>
       public int DeleteDutyUsers(List<DutyUser> dutyuser)
       {
           int ret = 0;
           for (int i = 0; i < dutyuser.Count; i++)
           {
                 ret =ret+ dutyuserdal.DeleteDutyUsers(dutyuser[i]);
           }
           return ret;
       }
    }
}
