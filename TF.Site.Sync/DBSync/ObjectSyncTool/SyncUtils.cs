using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Soft.Demo
{

    public class UserIndex
    {
        public string UserID;
        public string ObjectName;
        public string Key;
        public int OP;
        public DateTime UpdateTime;
    }
     public class SyncData
    {
        public string Key;
        public string Json;
        public string Version;
    }

     public class SyncDataOP : SyncData
     {
         //操作(0,1,2,3)不变,增，删，改
         public int OP = 0;
     }

    public class SyncUtils
    {
        public static string APIUrl = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx";

        private class GetUserIndexIn
        {
            public string UserID;
            public string ObjectName;            
        }
        private class GetUserIndexOut
        {
            public int IsReg;
            public List<UserIndex> UserIndexes;
        }
        public static bool GetUserIndex(string UserID, string ObjectName,ref List<UserIndex> UserIndexs)
        {
            GetUserIndexIn inParams = new GetUserIndexIn();
            inParams.UserID = UserID;
            inParams.ObjectName = ObjectName;
            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);

            TF.Web.WebAPI.InterfaceOutPut resp = TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.GetUserIndex", inputString);
            string respData = resp.Data.ToString();
            GetUserIndexOut outData = (GetUserIndexOut)Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserIndexOut>(respData);
            if (outData.IsReg < 1)
                return false;

            UserIndexs = outData.UserIndexes;
            return true;
        }

        private class GetObjectDataIn
        {
            public string ObjectName;
            public string Key;
        }
        private class GetObjectDataOut
        {
            public int IsExist;
            public SyncData Data;
        }
        public static bool GetObjectData(string ObjectName, string Key, ref SyncData Data)
        {
            GetObjectDataIn inParams = new GetObjectDataIn();
            inParams.ObjectName = ObjectName;
            inParams.Key = Key;
            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);

            TF.Web.WebAPI.InterfaceOutPut resp = TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.GetObjectData", inputString);
            string respData = resp.Data.ToString();
            GetObjectDataOut outData = (GetObjectDataOut)Newtonsoft.Json.JsonConvert.DeserializeObject<GetObjectDataOut>(respData);
            if (outData.IsExist < 1)
                return false;

            Data = outData.Data;
            return true;
        }

        private class CommitUserIndexIn
        {
            public string UserID;
            public string ObjectName;
            public string Key;
        }
        public static void CommitUserIndex(string UserID, string ObjectName, string Key)
        {
            CommitUserIndexIn inParams = new CommitUserIndexIn();
            inParams.UserID = UserID;
            inParams.ObjectName = ObjectName;
            inParams.Key = Key;
            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);
            TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.CommitUserIndex", inputString);
        }

        private class RegUserIn
        {
            public string UserID;
            public string UserName;
            public string ObjectName;
        }
        public static void RegUser(string UserID, string UserName, string ObjectName)
        {
            RegUserIn inParams = new RegUserIn();
            inParams.UserID = UserID;
            inParams.ObjectName = ObjectName;
            inParams.UserName = UserName;
            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);
            TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.RegUser", inputString);
        }

        private class UnRegUserIn
        {
            public string UserID;
            public string ObjectName;
        }
        public static void UnRegUser(string UserID, string ObjectName)
        {
            UnRegUserIn inParams = new UnRegUserIn();
            inParams.UserID = UserID;
            inParams.ObjectName = ObjectName;
            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);
            TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.UnRegUser", inputString);
        }

        private class GetObjectDatasIn
        {
            public string ObjectName;
        }
        private class GetObjectDatasOut
        {
            public List<TF.YA.Soft.Demo.SyncDataOP> DataList;
        }
        public static void GetObjectDatas(string ObjectName, List<TF.YA.Soft.Demo.SyncDataOP> userIndexes)
        {
            GetObjectDatasIn inParams = new GetObjectDatasIn();
            inParams.ObjectName = ObjectName;

            string inputString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);

            TF.Web.WebAPI.InterfaceOutPut resp = TF.Web.WebAPI.WebApiUtils.GetAPI(APIUrl, "TF.YA.Soft.IFSync.GetObjectDatas", inputString);
            string respData = resp.Data.ToString();
            GetObjectDatasOut outData = (GetObjectDatasOut)Newtonsoft.Json.JsonConvert.DeserializeObject<GetObjectDatasOut>(respData);

            userIndexes.AddRange(outData.DataList.ToArray());
            userIndexes = outData.DataList;
        }
 
    }
}
