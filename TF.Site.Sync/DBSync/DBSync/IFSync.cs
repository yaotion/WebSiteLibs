using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Web.WebAPI;
using Newtonsoft.Json.Converters;

namespace TF.YA.Soft.DBSync
{
  
  /// <summary>                          
  ///类名: IFSync
  ///说明: 获取差异更新数据索引
  /// </summary>
    public class IFSync
    {
        public static string GetDBConn()
        {
            return TF.CommonUtility.XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");
        }

   

        public class InRegUser
        {
            //客户端编号
            public string UserID;
            //客户端名称        
            public string UserName;
            //同步对象名称
            public string ObjectName;
        }
        /// <summary>
        /// 注册同步客户端
        /// </summary>
        public InterfaceOutPut RegUser(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InRegUser inParams = (InRegUser)Newtonsoft.Json.JsonConvert.DeserializeObject<InRegUser>(InputString);
                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                if (!uStore.ExistUser(inParams.UserID,inParams.ObjectName))
                {
                    uStore.RegUser(inParams.UserID, inParams.UserName, inParams.ObjectName);
                }
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;
                throw ex;
            }
            return output;
        }


        public class InUnRegUser
        {
            //客户端编号
            public string UserID;
            //关注对象名称
            public string ObjectName;
        }
        /// <summary>
        /// 注销客户端关注
        /// </summary>
        public InterfaceOutPut UnRegUser(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InUnRegUser inParams = (InUnRegUser)Newtonsoft.Json.JsonConvert.DeserializeObject<InUnRegUser>(InputString);
                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                uStore.UnRegUser(inParams.UserID, inParams.ObjectName);
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;                
                throw ex;
            }
            return output;
        } 


        public class InGetUserIndex
        {
            //客户端编号
            public string UserID;
            //关注对象名称
            public string ObjectName;
        }
        public class OutGetUserIndex
        {
            //该客户端是否已经注册过
            public int IsReg;
            //用户索引列表
            public List<UserIndex> UserIndexes = new List<UserIndex>();
        }
        /// <summary>
        /// 获取差异更新数据索引
        /// </summary>
        public InterfaceOutPut GetUserIndex(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {

                InGetUserIndex inParams = (InGetUserIndex)Newtonsoft.Json.JsonConvert.DeserializeObject<InGetUserIndex>(InputString);
                OutGetUserIndex OutParams = new OutGetUserIndex();

                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                List<TF.YA.Soft.UserIndex> userIndexes = new List<TF.YA.Soft.UserIndex>();

                OutParams.IsReg = 1;
                if (!uStore.GetUserIndex(inParams.UserID, inParams.ObjectName, userIndexes))
                {
                    OutParams.IsReg = 0;                   
                }
                OutParams.UserIndexes = userIndexes;

                output.Data = OutParams;
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;
                throw ex;
            }
            return output;
        }


        public class InGetObjectData
        {
            //关注对象名称
            public string ObjectName;
            //数据主键
            public string Key;
        }
        public class OutGetObjectData
        {
            //是否存在(0不存在,1存在)
            public int IsExist;
            //数据内容
            public SyncData Data = new SyncData();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        public InterfaceOutPut GetObjectData(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InGetObjectData inParams = (InGetObjectData)Newtonsoft.Json.JsonConvert.DeserializeObject<InGetObjectData>(InputString);
                OutGetObjectData OutParams = new OutGetObjectData();

                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());                
                OutParams.IsExist = 0;
                OutParams.Data.Json = "";
                OutParams.Data.Key = "";
                OutParams.Data.Version = "";
                if (uStore.GetObjectData(inParams.ObjectName, inParams.Key, OutParams.Data))
                {
                    OutParams.IsExist = 1;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(OutParams.Data.Json);

                    OutParams.Data.Json = Convert.ToBase64String(buffer);
                }

                output.Data = OutParams;
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;                
                throw ex;
            }
            return output;
        }


        public class InGetObjectDatas
        {
            //同步对象名称
            public string ObjectName;
        }
        public class OutGetObjectDatas
        {
            //对象列表
            public List<SyncDataOP> DataList = new List<SyncDataOP>();
        }
        /// <summary>
        /// 获取全部数据索引
        /// </summary>
        public InterfaceOutPut GetObjectDatas(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InGetObjectDatas inParams = (InGetObjectDatas)Newtonsoft.Json.JsonConvert.DeserializeObject<InGetObjectDatas>(InputString);
                OutGetObjectDatas OutParams = new OutGetObjectDatas();

                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                uStore.GetObjectDatas(inParams.ObjectName, OutParams.DataList); 

                output.Data = OutParams;
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;                
                throw ex;
            }
            return output;
        }


        public class InCommitUserIndex
        {
            //客户端编号
            public string UserID;
            //用户名称
            public string ObjectName;
            //数据主键
            public string Key;
        }

        /// <summary>
        /// 提交用户更新
        /// </summary>
        public InterfaceOutPut CommitUserIndex(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InCommitUserIndex inParams = (InCommitUserIndex)Newtonsoft.Json.JsonConvert.DeserializeObject<InCommitUserIndex>(InputString);
                OutGetObjectDatas OutParams = new OutGetObjectDatas();
                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                uStore.CommitUserIndex(inParams.UserID,inParams.ObjectName, inParams.Key);
                output.Data = OutParams;
                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;
           
                throw ex;
            }
            return output;
        }


        public class InClearUserIndex
        {
            //客户端编号
            public string UserID;
            //关注对象名称
            public string ObjectName;
        }

        /// <summary>
        /// 清除客户端的所有更新
        /// </summary>
        public InterfaceOutPut ClearUserIndex(String InputString)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.Success = 0;
            try
            {
                InClearUserIndex inParams = (InClearUserIndex)Newtonsoft.Json.JsonConvert.DeserializeObject<InClearUserIndex>(InputString);
                OutGetObjectDatas OutParams = new OutGetObjectDatas();
                TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(GetDBConn());
                uStore.ClearUserIndex(inParams.UserID, inParams.ObjectName);
                output.Data = OutParams;

                output.Success = 1;
            }
            catch (Exception ex)
            {
                output.ResultText = ex.Message;
                throw ex;
            }
            return output;
        }  
    }
  }
