using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using TF.DB.DBUtility;
using Common.Logging;
using System.Data.SqlClient;

namespace TF.YA.Org
{

    public class IFLoader
    {
        public static string GetDBConn()
        {
            return TF.CommonUtility.XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");
        }
        public static ILog GetLogger()
        {
            return Common.Logging.LogManager.GetLogger("TF.YA.Org");
        }
    }
    
    public class LCOrgIF
    {
        private static TF.Web.WebAPI.InterfaceOutPut result = new TF.Web.WebAPI.InterfaceOutPut();

        public class OutGetAllDepts
        {
            //部门列表
            public List<TF.YA.Org.Dept> Depts = new List<TF.YA.Org.Dept>();
        }
        public TF.Web.WebAPI.InterfaceOutPut GetAllDepts(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                List<TF.YA.Org.Dept> depts = LCOrg.GetAllDepts(IFLoader.GetLogger(), Conn);
                OutGetAllDepts data = new OutGetAllDepts();
                data.Depts = depts;
                result.Data = data;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }

        public class OutGetAllPosts
        {
            //岗位列表
            public List<TF.YA.Org.Post> Posts = new List<TF.YA.Org.Post>();
        }
        public TF.Web.WebAPI.InterfaceOutPut GetAllPosts(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                List<TF.YA.Org.Post> depts = LCOrg.GetAllPosts(IFLoader.GetLogger(), Conn);
                OutGetAllPosts data = new OutGetAllPosts();
                data.Posts = depts;
                result.Data = data;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }

        private class GetAllUsersIn
        {
            public int PageIndex = 1;
            public int PageCount = 1;
        }
        public class GetAllUsersOut
        {
            public int TotalCount;
            public List<YA.Org.User> users;
        }
        public TF.Web.WebAPI.InterfaceOutPut GetAllUsers(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                GetAllUsersIn inParams = (GetAllUsersIn)Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllUsersIn>(InputString);


                List<TF.YA.Org.User> users = LCUser.QueryUser(IFLoader.GetLogger(), Conn, inParams.PageIndex, inParams.PageCount, "", "","");


                GetAllUsersOut outputData = new GetAllUsersOut();
                outputData.users = users;
                outputData.TotalCount = LCUser.QueryUserCount(IFLoader.GetLogger(), Conn, "", "","");

                result.Data = outputData;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }

        private class UpdateUserTelIn
        {
            public string UserNumber = "";
            public string UserTel = "";
        }
        public TF.Web.WebAPI.InterfaceOutPut UpdateUserTel(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                UpdateUserTelIn inParams = (UpdateUserTelIn)Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateUserTelIn>(InputString);

                User U = new User();
                if (!LCUser.GetUser(IFLoader.GetLogger(), Conn, inParams.UserNumber, U))
                {
                    result.Success = 0;
                    result.ResultText = string.Format("未找到人员{0}的信息", inParams.UserNumber);

                }
                else
                {
                    U.TelNumber = inParams.UserTel;
                    LCUser.UpdateUser(IFLoader.GetLogger(), Conn, U);                    
                    result.Success = 1;
                    result.ResultText = "返回成功";
                }
                result.Data = "";
                return result;
            }
        }

        private class GetUserFeaturesIn
        {
            public string UserNumber;            
        }

        public class OutGetUserFeatures
        {
            //特征类型
            public List<Feature> Features = new List<Feature>();
        }

        public TF.Web.WebAPI.InterfaceOutPut GetUserFeatures(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                GetUserFeaturesIn inParams = (GetUserFeaturesIn)Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserFeaturesIn>(InputString);
                List<TF.YA.Org.Feature> featureList = LCUser.GetUserFeatures(IFLoader.GetLogger(), Conn, inParams.UserNumber);
                for (int i = 0; i < featureList.Count; i++)
                {
                    if (featureList[i].FeatureContent is byte[])
                    {
                        featureList[i].FeatureContent = Convert.ToBase64String(featureList[i].FeatureContent as byte[]);
                    }
                }
                OutGetUserFeatures data = new OutGetUserFeatures();
                data.Features = featureList;
                result.Data = data;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }

        public TF.Web.WebAPI.InterfaceOutPut UpdateUserFeature(string InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                Feature inParams = (Feature)Newtonsoft.Json.JsonConvert.DeserializeObject<Feature>(InputString);
                if (inParams.FeatureContent != null)
                {
                    inParams.FeatureContent = Convert.FromBase64String(inParams.FeatureContent as string);
                }
                if (DBUser.ExistUserFeature(IFLoader.GetLogger(), Conn, inParams.UserNumber, inParams.FeatureType, inParams.FeatureIndex))
                {
                    DBUser.UpdateFeature(IFLoader.GetLogger(), Conn, inParams);
                }
                else
                {
                    DBUser.AddFeature(IFLoader.GetLogger(), Conn, inParams);
                }
                result.Data = null;
                result.Success = 1;
                result.ResultText = "返回成功";
                return result;
            }
        }


        public class InGetAllDutyUsers
        {
            //页索引，从1开始
            public int PageIndex;
            //每页数据数量
            public int PageCount;
        }

        public class OutGetAllDutyUsers
        {
            //总共数量
            public int TotalCount;
            //值班员信息列表
            public List<DutyUser> DutyUsers = new List<DutyUser>();
        }

        /// <summary>
        /// 分页会哦去所有值班员信息
        /// </summary>
        public TF.Web.WebAPI.InterfaceOutPut GetAllDutyUsers(String InputString)
        {
            using (SqlConnection Conn = new SqlConnection(IFLoader.GetDBConn()))
            {
                TF.Web.WebAPI.InterfaceOutPut output = new TF.Web.WebAPI.InterfaceOutPut();
                output.Success = 0;
                try
                {
                    InGetAllDutyUsers inParams = (InGetAllDutyUsers)Newtonsoft.Json.JsonConvert.DeserializeObject<InGetAllDutyUsers>(InputString);
                    OutGetAllDutyUsers OutParams = new OutGetAllDutyUsers();
                    OutParams.DutyUsers = TF.YA.Org.LCDutyUser.QueryDutyUser(IFLoader.GetLogger(), Conn, inParams.PageIndex, inParams.PageCount, "", "");
                    OutParams.TotalCount = TF.YA.Org.LCDutyUser.QueryDutyUserCount(IFLoader.GetLogger(), Conn, "", "");
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
}
