using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Sync
{

    public class RegUser
    {
       public string UserID;
       public string UserName;
       public string ObjectName;
    }
    public class unregUser
    {
        public string UserID;
        public string ObjectName;


    }
    public class UserIndexAll
    {

        public int Success;
        public string ResultText;
        public UserIndex Data;
    }
    public class UserIndex
    {

        public int IsReg;
        public List<UserIndexeses> UserIndexes;
    }
    public class UserIndexeses

    {

        public string UserId;
        public string ObjectName;
        public String Key;
        public int OP;
        public DateTime UpdateTime;

    }
    public class ObjectData
    {
        public string ObjectName;
        public string Key;
    }
    public class ReturnObjectData
    {
        public int IsExist;
        public SyncData Data;
    }
    public class SyncData
    {

        public string Key;
        public string Json;
        public string Version;
    }
    public class SyncDataAll
    {
        public int Success;
        public string ResultText;
        public ReturnObjectData  Data;
    }
    /// <summary>
    /// 获取索引 
    /// </summary>
    public class SyncDataIndexListobj
    {

        public string Key;
        public string Version;

    }
    public class SyncDataIndexList
    {

        public List<SyncDataIndexListobj> DataList;
       

    }
    public  class SyncDataIndexAll
{
       public int Success;
        public string ResultText;
        public SyncDataIndexList  Data;
}
    public class commitUserup
    {

        public string UserID;
        public string ObjectName;
        public string Key;
    }


    
       
}
