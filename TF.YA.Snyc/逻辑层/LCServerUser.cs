using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Sync
{
    public class LCServerUser
    {       
        public static bool RegUsers(string url, RegUser reguser)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(reguser);
            string msg = "";

            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.RegUser&Data=" + strobj;
            try
            {
                String bret = HttpCommon.GetStr(apiUrl, out msg);
                return true;
            }
            catch
            {
                return false;
            }



        }
        public static void UnRegUsers(string url, unregUser ureguser)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(ureguser);
            string msg = "";
            if (string.IsNullOrEmpty(url))
                url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.UnRegUser&Data=" + strobj;
            else

                url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.UnRegUser&Data=" + strobj;
            String bret = HttpCommon.GetStr(url, out msg);



        }
        public static UserIndexAll GetUserIndex(string url, unregUser ureguser)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(ureguser);
            string msg = "";
         

            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.GetUserIndex&Data=" + strobj;
            String bret = HttpCommon.GetStr(apiUrl, out msg);
            UserIndexAll userindelist = Newtonsoft.Json.JsonConvert.DeserializeObject<UserIndexAll>(bret);

            return userindelist;



        }
        public static SyncData GetUserData(string url, ObjectData objdata)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(objdata);
            string msg = "";
            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.GetObjectData&Data=" + strobj;
            String bret = HttpCommon.GetStr(apiUrl, out msg);
            SyncDataAll userlist = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncDataAll>(bret);
            if (userlist.Success == 1)
            {

                return userlist.Data.Data;
            }
            else
            {

                return null;
            }
        }
        public static List<SyncDataIndexListobj> GetAllDataIndex(string url, string ObjectName)
        {

            string msg = "";
            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.GetObjectDatas&Data={'ObjectName':'" + ObjectName + "'}";
            String bret = HttpCommon.GetStr(apiUrl, out msg);
            SyncDataIndexAll userindelist = Newtonsoft.Json.JsonConvert.DeserializeObject<SyncDataIndexAll>(bret);
            if (userindelist.Success == 1)
            {

                return userindelist.Data.DataList;
            }
            else
            {

                return null;
            }



        }
        public static void CommintUser(string url, commitUserup ureguser)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(ureguser);
            string msg = "";
            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.CommitUserIndex&Data=" + strobj;
            String bret = HttpCommon.GetStr(apiUrl, out msg);



        }
        public static void ClearUserIndex(string url, unregUser ureguser)
        {
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(ureguser);
            string msg = "";
            string apiUrl = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Soft.IFSync.ClearUserIndex&Data=" + strobj;
            String bret = HttpCommon.GetStr(apiUrl, out msg);
        }        

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<DeptCls> GetDeptTypelist(string url)
        {
            string msg = "";
            if (string.IsNullOrEmpty(url))
                url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
            else

                url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
            RespData bret = HttpCommon.Get(url, out msg);
            DeptClsList deptlist = bret.Data;
            List<DeptCls> depclass = deptlist.Depts;
            return depclass;
        }

        public static List<Posts> GetAllPosts(string URL)
        {
            string msg = "";//
            if (string.IsNullOrEmpty(URL))
                URL = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllPosts&Data={}";
            else

                URL = URL + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllPosts&Data={}";
            string RET = HttpCommon.GetStr(URL, out msg);
            PostALL postlist = Newtonsoft.Json.JsonConvert.DeserializeObject<PostALL>(RET);

            List<Posts> polst = postlist.Data.Posts;
            return polst;
        }

        public static int DoPosts(string url)
        {
            List<Posts> LstRemt = GetAllPosts(url);
            List<Posts> local = DBYAUser.GetPostAll();
            CompareUtils<Posts> cmp = new CompareUtils<Posts>();
            if (cmp.Compare(local, LstRemt))
            {
                return 0;
            }
            int count = 0;
            //添加 
            for (int i = 0; i < cmp.NewList.Count; i++)
            {
                count = count + DBYAUser.SavePost(cmp.NewList[i]);
            }
            //修改 
            for (int i = 0; i < cmp.UpdateList.Count; i++)
            {
                count = count + DBYAUser.UpdatePost(cmp.UpdateList[i]);
            }

            //删除 
            for (int i = 0; i < cmp.RemoveList.Count; i++)
            {
                count = count + DBYAUser.DeletePost(cmp.RemoveList[i]);
            }

            return count;
        }
    }
    public class UserAPI
    {
        public List<YAUser> UserInfo;

        public List<Features> UserFeatures;

    }
}
