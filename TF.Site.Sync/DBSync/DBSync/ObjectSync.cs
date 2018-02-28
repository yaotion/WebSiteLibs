using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Soft
{
    public class SyncData
    {
        public string Key;
        public string Json;
        public string Version;
    }
    public class SyncObject
    {
        public string ObjectName;
        public string ObjectVersion;
    }
    public class SyncObjectSum
    {
        public string ObjectName;
        public int Count;
    }
    public class SyncUser
    {
        public string ObjectName;
        public string UserID;
        public string UserName;
        public DateTime CreateTime;
    }
    public class SyncUserSum
    {
        public string UserID;
        public string ObjectName;
        public int Count;
    }
    /// <summary>
    /// 同步对象操作
    /// </summary>
    public class SyncDataOP : SyncData
    {
        //操作(0,1,2,3)不变,增，删，改
        public int OP = 0;        
    }
    public class UserIndex
    {        
        public string UserID;
        public string ObjectName;
        public string Key;
        public int OP;
        public DateTime UpdateTime;
    }
    public interface ISyncStore
    {
        bool GetObject(string ObjectName, SyncObject Obj);
        void AddObject(SyncObject Obj);
        void UpdateObject(SyncObject Obj);
        void DeleteObject(string ObjectName);

        void AddData(string ObjectName, SyncData Data);
        void UpdateData(string ObjectName, SyncData Data);
        void DeleteData(string ObjectName, SyncData Data);
        void GetObjectDatas(string ObjectName, List<SyncDataOP> DataList);
        /// <summary>
        /// 获取指定对象的数据
        /// </summary>
        /// <param name="ObjectName"></param>
        /// <param name="Key"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        bool GetObjectData(string ObjectName, string Key, SyncData Data);

        void AddUserIndex(SyncObject Obj, SyncData Data, int OP);
        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <param name="Objs"></param>
        void GetObjects(List<SyncObject> Objs);
        /// <summary>
        /// 获取所有对象统计信息
        /// </summary>
        /// <param name="ObjsSum"></param>
        void GetObjectsSum(List<SyncObjectSum> ObjsSum);
        /// <summary>
        /// 获取所有客户端信息
        /// </summary>
        /// <param name="Users"></param>
        void GetUsers(List<SyncUser> Users);
        //获取所有客户端统计信息
        void GetUsersSum(List<SyncUserSum> UsersSum);

        /// <summary>
        /// 获取客户端需要的更新信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ObjectName"></param>
        /// <param name="UserIndexes"></param>
        bool GetUserIndex(string UserID,string ObjectName,List<UserIndex> UserIndexes);        
        /// <summary>
        /// 提交客户端的更新
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Key"></param>
        /// <param name="IndexID"></param>
        void CommitUserIndex(string UserID, string ObjectName, string Key);
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <param name="ObjectName"></param>
        void RegUser(string UserID, string UserName, string ObjectName);
        //清除用户对于指定对象的更新
        void ClearUserIndex(string UserID, string ObjectName);
        //注销用户
        void UnRegUser(string UserID,string ObjectName);
    }
   
    public interface ISyncSource
    {
        bool GetObject(string ObjectName,SyncObject Obj);
        void GetDataList(string ObjectName,List<SyncData> DataList);
    }

    public class SyncUtils
    {
        public static void Update(string ObjectName,ISyncSource SourceUtils,ISyncStore StoreUtils)
        {
            SyncObject sourceObject = new SyncObject();
            if (!SourceUtils.GetObject(ObjectName,sourceObject) )
            {
                throw new NullReferenceException("目标数据源无此对象:" + ObjectName);
            }
            SyncObject storeObject = new SyncObject();
            if (!StoreUtils.GetObject(ObjectName, storeObject))
            {
                storeObject.ObjectName = ObjectName;
                storeObject.ObjectVersion = "";                
            }

            if (sourceObject.ObjectVersion == storeObject.ObjectVersion)
            {
                //比对版本，一致则退出
                return;
            }
            //加载本地数据信息
            List<SyncDataOP> storeDataList = new List<SyncDataOP>();
            StoreUtils.GetObjectDatas(ObjectName, storeDataList);

            List<SyncData> sourceDataList = new List<SyncData>();
            SourceUtils.GetDataList(ObjectName, sourceDataList);

            //遍历数据源索引
            foreach (var item in sourceDataList)
            {
                
                //本地仓储无数据则条件索引
                if (!storeDataList.Exists((SyncDataOP s) => s.Key == item.Key))
                {
                    SyncDataOP dop = new SyncDataOP();
                    dop.Key = item.Key;
                    dop.Json = item.Json;
                    dop.Version = item.Version;
                    dop.OP = 1;
                    storeDataList.Add(dop);
                }
                else
                {
                    //已有不同则更新
                    if (storeDataList.Find((SyncDataOP s) => s.Key == item.Key).Version != item.Version)
                    {
                        storeDataList.Find((SyncDataOP s) => s.Key == item.Key).Version = item.Version;
                        storeDataList.Find((SyncDataOP s) => s.Key == item.Key).Json = item.Json;
                        storeDataList.Find((SyncDataOP s) => s.Key == item.Key).OP = 2;
                    }                   
                }
            }
            //判断本地仓储是否有删除的
            foreach (var item in storeDataList)
            {
                if (!sourceDataList.Exists((SyncData s) => s.Key == item.Key))
                {
                    item.OP = 3;
                }
            }

            //保存本地存储索引信息
            foreach (var item in storeDataList)
            {
                SyncData d = new SyncData();
                d.Key = item.Key;
                d.Json = item.Json;
                d.Version = item.Version;
                if (item.OP == 0)
                    continue;
                if (item.OP == 1)
                {
                    StoreUtils.AddData(storeObject.ObjectName, d);
                }
                if (item.OP == 2)
                {
                    StoreUtils.UpdateData(storeObject.ObjectName, d);
                }
                if (item.OP == 3)
                {
                    StoreUtils.DeleteData(storeObject.ObjectName, d);
                }
                StoreUtils.AddUserIndex(storeObject, d, item.OP);
            }
            //保存本地对象版本信息
            if (storeObject.ObjectVersion == "")
            {
                storeObject.ObjectVersion = sourceObject.ObjectVersion;
                StoreUtils.AddObject(storeObject);
            }
            else {
                storeObject.ObjectVersion = sourceObject.ObjectVersion;
                StoreUtils.UpdateObject(storeObject);
            }
        }
       
    }
}
