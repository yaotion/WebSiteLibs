using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Common.Logging;

namespace TF.YA.Sync
{
    public class LCSyncUser
    {
        private static DBYAUser dbLocalUser = new DBYAUser();
        /// <summary>
        /// 执行用户同步流程
        /// </summary>
        public static int SyncUser(ILog Log,  string url, string Usernum, string UserObject, string UserCn)
        {
            unregUser usermodel = new unregUser();
            usermodel.UserID = Usernum;
            usermodel.ObjectName = UserObject;

            Log.Info("开始获取差异信息");
            //获取差异数据索引
            UserIndexAll userindelist = LCServerUser.GetUserIndex(url, usermodel);
         
            //未注册客户端
            if (userindelist.Data.IsReg == 0)//未注册
            {
                Log.Info("未注册过差异客户端信息");
                RegUser reg = new RegUser();
                reg.UserID = Usernum;
                reg.UserName = UserCn;
                reg.ObjectName = UserObject;
                //没注册过先注册后执行全表同步
                Log.Info("执行全表信息同步");
                int ncount = LCSyncUser.SyncUserAll(Log, url);
                Log.Info(string.Format("执行全表信息同步完成，共同步了{0}条信息",ncount));
                Log.Info("注册差异客户端");
                LCServerUser.RegUsers(url, reg);
                return ncount;
            }
            if (userindelist.Data.UserIndexes.Count == 0)
            {
                Log.Info("无差异信息");
                return 0;
            }
            Log.Info(string.Format("开始执行差异同步，共有{0}条差异信息",userindelist.Data.UserIndexes.Count));
            //注册过则执行差异同步
            int syncCount= LCSyncUser.SyncUserDiff(Log, url, userindelist.Data.UserIndexes);
            Log.Info("差异执行完毕");
            return syncCount;
        }

        /// <summary>
        /// 差异同步用户
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="url"></param>
        /// <param name="userindexlst"></param>
        /// <returns></returns>
        public static int SyncUserDiff(ILog Log, string url, List<UserIndexeses> userindexlst)
        {
            int result = 0;
            //循环获取需要更新的数据
            foreach (UserIndexeses uindex in userindexlst)
            {

                //删除
                if (uindex.OP == 3)
                {
                    DBYAUser.DeleteYAUser(uindex.Key);
                    result = result + 1;
                }
                else
                {
                    //获取用户详细信息
                    UserAPI aUser = GetAPIUser(url, uindex.Key, uindex.ObjectName, uindex.Key);
                    //获取部门信息
                    List<DeptCls> deptList = LCServerUser.GetDeptTypelist(url);
                    //翻译人员信息
                    YAUser userItem = FillUserFeature(aUser.UserInfo[0], deptList, aUser.UserFeatures);

                    //添加
                    if (uindex.OP == 1)
                    {
                        LCYAUser.UpdateYAUser(userItem);
                        result = result + 1;

                    }
                    //修改
                    if (uindex.OP == 2)
                    {
                        LCYAUser.UpdateYAUser(userItem);
                        result = result + 1;
                    }
                }

                //提交更新完毕
                commitUserup CommUser = new commitUserup();
                CommUser.Key = uindex.Key;
                CommUser.UserID = uindex.UserId;
                CommUser.ObjectName = uindex.ObjectName;
                LCServerUser.CommintUser(url, CommUser);
            }
            return result;
        }
        
        
        /// <summary>
        /// 全表同步
        /// </summary>
        /// <param name="Log"></param>
        /// <param name="Url"></param>
        /// <returns></returns>      
        public static int SyncUserAll(ILog Log, string Url)
        {
            List<YAUser> sourceUsers = new List<YAUser>();
            List<SyncDataIndexListobj> dataIndexList = LCServerUser.GetAllDataIndex(Url, "User");
            List<DeptCls> depts = LCServerUser.GetDeptTypelist(Url);
            for (int i = 0; i < dataIndexList.Count; i++)
            {
                ObjectData inputData = new ObjectData();
                inputData.Key = dataIndexList[i].Key;
                inputData.ObjectName = "User";
                SyncData sData = LCServerUser.GetUserData(Url, inputData);
                string strJson = Encoding.UTF8.GetString(Convert.FromBase64String(sData.Json));
                UserAPI userTemp = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAPI>(strJson);
                if (userTemp.UserInfo.Count > 0)
                {
                    YAUser userItem = FillUserFeature(userTemp.UserInfo[0], depts, userTemp.UserFeatures);
                    sourceUsers.Add(userItem);
                }
            }
            List<YAUser> localUsers = LCYAUser.GetUserAlls();

            CompareUtils<YAUser> cmp = new CompareUtils<YAUser>();
            if (cmp.Compare(localUsers, sourceUsers))
            {
                Log.Info("无变动信息，不需要更新");
                return 0;
            }
            Log.Info(string.Format("本次全表同步共有{0}条新增记录,{1}条修改记录,{2}条删除记录", cmp.NewList.Count, cmp.UpdateList.Count, cmp.RemoveList.Count));
            int count = 0;
            //添加
            for (int i = 0; i < cmp.NewList.Count; i++)
            {
                count = count + LCYAUser.UpdateYAUser((YAUser)cmp.NewList[i]);
            }
            //修改
            for (int i = 0; i < cmp.UpdateList.Count; i++)
            {
                count = count + LCYAUser.UpdateYAUser((YAUser)cmp.UpdateList[i]);
            }

            //删除无用计划
            for (int i = 0; i < cmp.RemoveList.Count; i++)
            {
                count = count + LCYAUser.DeleteYAUser(cmp.RemoveList[i].UserNumber);
            }
            return count;

        }

        /// <summary>
        /// 获取接口的记录的详细信息
        /// </summary>
        /// <param name="APIUrl"></param>
        /// <param name="User"></param>
        /// <param name="ObjectName"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static UserAPI GetAPIUser(string APIUrl, string User, string ObjectName, string Key)
        {
            ObjectData od = new ObjectData();
            od.Key = Key;
            od.ObjectName = ObjectName;

            //获取需要更新的数据内容
            SyncData SD = LCServerUser.GetUserData(APIUrl, od);

            string STRJSON = Encoding.UTF8.GetString(Convert.FromBase64String(SD.Json));

            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAPI>(STRJSON);
        }       
        /// <summary>
        /// 获取指定部门的所有运安部门形式 机务段-车间-车队
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="DeptList"></param>
        /// <returns></returns>
        private static string GetUserDeptFull(string DeptID, List<DeptCls> DeptList)
        {
            string jwd = "";
            string cj = "";
            string cd = "";            
            List<string> fullParentID = new List<string>();
            for (int i = 0; i < DeptList.Count; i++)
			{
                if (DeptID == DeptList[i].DeptId)
                {
                    fullParentID.AddRange(DeptList[i].FullParentID.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries));
                }
			}
            for (int i = 0; i < DeptList.Count; i++)
            {
                for (int k = 0; k < fullParentID.Count; k++)
                {
                    if (DeptList[i].DeptId == fullParentID[k])
                    {
                        if (DeptList[i].DeptType == "2")
                        {
                            jwd = fullParentID[k];
                        }
                        if (DeptList[i].DeptType == "3")
                        {
                            cj = fullParentID[k];
                        }
                        if (DeptList[i].DeptType == "4")
                        {
                            cd = fullParentID[k];
                        }
                    }
                }
            }
            return string.Format("{0}-{1}-{2}",jwd,cj,cd);
       
        }
        /// <summary>
        /// 填充用户的指纹人脸特征
        /// </summary>
        /// <param name="U"></param>
        /// <param name="Depts"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        private static YAUser FillUserFeature(YAUser U,List<DeptCls> Depts, List<Features> F)
        {
            
            YAUser yu = new YAUser();
            yu.UserName = U.UserName.Trim();
            yu.UserNumber = U.UserNumber;
            yu.NameJP = U.NameJP;
            yu.TelNumber = U.TelNumber;
            if (!string.IsNullOrEmpty(U.PostID))
            {
                yu.PostID = U.PostID;
            }

            yu.DepartFullName = GetUserDeptFull(U.DeptID, Depts);
            foreach (var item in F)
            {
                if (string.IsNullOrEmpty(item.FeatureContent))
                    item.FeatureContent = "";


                //指纹
                if (item.FeatureType == 1)
                {
                    if (item.FeatureIndex == 1)
                    {
                        yu.Finger1 = item.FeatureContent;
                    }
                    if (item.FeatureIndex == 2)
                    {
                        yu.Finger2 = item.FeatureContent;
                    }
                }
                //人脸
                if (item.FeatureType == 4)
                {
                    if (item.FeatureIndex == 1)
                    {
                        yu.Picture = item.FeatureContent;
                    }
                }
            }
            return yu;
        }
       
    }

}
