using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TF.YA.Soft
{
    public class SyncTool
    {
        public static string MD5Encrypt16(string password)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
    public class DBSyncObject : ISyncStore
    {
        string _ConnString;
        public DBSyncObject(string ConnString)
        {
            _ConnString = ConnString;
        }
        public bool GetObject(string ObjectName, SyncObject Obj)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Sync_Store where ObjectName = @ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName",ObjectName)
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Obj.ObjectName = dt.Rows[0]["ObjectName"].ToString();
                    Obj.ObjectVersion = dt.Rows[0]["ObjectVersion"].ToString();
                    return true;
                }
                return false;
            }
        }
        public void AddObject(SyncObject Obj)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "insert into TAB_Sync_Store (ObjectName,ObjectVersion) values (@ObjectName,@ObjectVersion)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", Obj.ObjectName),
                    new SqlParameter("ObjectVersion", Obj.ObjectVersion)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void UpdateObject(SyncObject Obj)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "update TAB_Sync_Store set ObjectVersion=@ObjectVersion where ObjectName=@ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", Obj.ObjectName),
                    new SqlParameter("ObjectVersion", Obj.ObjectVersion)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void DeleteObject(string ObjectName)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "delete from TAB_Sync_Store where ObjectName=@ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", ObjectName)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }

        public void AddData(string ObjectName, SyncData Data)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "insert into TAB_Sync_Store_Data (ObjectName,[Key],Json,[Version],UpdateTime) values (@ObjectName,@Key,@Json,@Version,getdate())";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", ObjectName),
                    new SqlParameter("Key", Data.Key),
                    new SqlParameter("Json", Data.Json),
                    new SqlParameter("Version", Data.Version),
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void UpdateData(string ObjectName, SyncData Data)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "update TAB_Sync_Store_Data set Json=@Json,[Version]=@Version,UpdateTime=getdate() where ObjectName=@ObjectName and [Key]=@Key";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", ObjectName),
                    new SqlParameter("Key", Data.Key),
                    new SqlParameter("Json", Data.Json),
                    new SqlParameter("Version", Data.Version)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void DeleteData(string ObjectName, SyncData Data)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "delete from TAB_Sync_Store_Data where ObjectName=@ObjectName and [Key]=@Key";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName", ObjectName),
                    new SqlParameter("Key", Data.Key)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void GetObjectDatas(string ObjectName, List<SyncDataOP> DataList)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Sync_Store_Data where ObjectName = @ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName",ObjectName)
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncDataOP dop = new SyncDataOP();
                    dop.Key = dt.Rows[i]["Key"].ToString();
                    //此处故意不获取内容，为的是考虑效率
                    dop.Json = "";
                    //dop.Json = dt.Rows[i]["Json"].ToString();
                    dop.Version = dt.Rows[i]["Version"].ToString();
                    dop.OP  = 0;
                    DataList.Add(dop);
                }
            }
        }
        public void AddUserIndex(SyncObject Obj, SyncData Data, int OP)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "insert into TAB_Sync_User_Data (UserID,ObjectName,[Key],Op,UpdateTime) (select UserID,@ObjectName,@Key,@Op,getdate() from TAB_Sync_User where ObjectName=@ObjectName)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName",Obj.ObjectName),
                    new SqlParameter("Key",Data.Key),
                    new SqlParameter("OP",OP)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
        public void GetObjects(List<SyncObject> Objs)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Sync_Store order by ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                   
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncObject obj = new SyncObject();
                    obj.ObjectName = dt.Rows[i]["ObjectName"].ToString();
                    obj.ObjectVersion = dt.Rows[i]["ObjectVersion"].ToString();
                    Objs.Add(obj);
                }
            }
        }

        public void GetObjectsSum(List<SyncObjectSum> ObjsSum)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select ObjectName,count(*) as DataCount from TAB_Sync_Store_Data group by ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                   
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncObjectSum sum = new SyncObjectSum();
                    sum.ObjectName = dt.Rows[i]["ObjectName"].ToString();
                    sum.Count = TF.DB.DBConvert.ToInt32(dt.Rows[i]["DataCount"]);
                    ObjsSum.Add(sum);
                }
            }
        }

        public void GetUsers(List<SyncUser> Users)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Sync_User order by UserID,ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                   
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncUser u = new SyncUser();
                    u.UserID = dt.Rows[i]["UserID"].ToString();
                    u.UserName = dt.Rows[i]["UserName"].ToString();
                    u.ObjectName = dt.Rows[i]["ObjectName"].ToString();
                    u.CreateTime = TF.DB.DBConvert.ToDateTime(dt.Rows[i]["CreateTime"]);
                    Users.Add(u);
                }
            }
        }

        public void GetUsersSum(List<SyncUserSum> UsersSum)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select UserID,ObjectName,count(*) as DataCount from TAB_Sync_User_Data group by UserID,ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                   
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncUserSum sum = new SyncUserSum();
                    sum.UserID = dt.Rows[i]["UserID"].ToString();
                    sum.ObjectName = dt.Rows[i]["ObjectName"].ToString();                    
                    sum.Count = TF.DB.DBConvert.ToInt32(dt.Rows[i]["DataCount"]);
                    UsersSum.Add(sum);
                }
            }
        }


        public bool GetObjectData(string ObjectName, string Key, SyncData Data)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Sync_Store_Data where ObjectName = @ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName",ObjectName)
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Data.Json = dt.Rows[0]["Json"].ToString();
                    Data.Key = dt.Rows[0]["Key"].ToString();
                    Data.Version = dt.Rows[0]["Version"].ToString();
                    return true;
                }                
            }
            return false;
        }

        public bool GetUserIndex(string UserID, string ObjectName, List<UserIndex> UserIndexes)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                
                string strSql = "select * from TAB_Sync_User where UserID = @UserID and ObjectName = @ObjectName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("ObjectName",ObjectName)
                };
                DataTable dtUsers = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dtUsers.Rows.Count == 0)
                {
                    return false;
                }


                strSql = "select * from TAB_Sync_User_Data where UserID = @UserID and ObjectName = @ObjectName order by [Key]";
               
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserIndex d = new UserIndex();
                    d.UserID = dt.Rows[i]["UserID"].ToString();
                    d.ObjectName = dt.Rows[0]["ObjectName"].ToString();
                    d.Key = dt.Rows[i]["Key"].ToString();
                    d.OP = TF.DB.DBConvert.ToInt32(dt.Rows[i]["OP"]);
                    d.UpdateTime = TF.DB.DBConvert.ToDateTime(dt.Rows[i]["UpdateTime"]);
                    if (UserIndexes.Exists((UserIndex s) => (s.Key == d.Key) && (s.UpdateTime < d.UpdateTime)))
                    {
                        UserIndexes.Find((UserIndex s) => (s.Key == d.Key)).OP = d.OP;
                        UserIndexes.Find((UserIndex s) => (s.Key == d.Key)).UpdateTime = d.UpdateTime;
                    }

                    if (!UserIndexes.Exists((UserIndex s) => (s.Key == d.Key)))
                    {
                        UserIndexes.Add(d);
                    }
                }
                return true;                
            }
           
        }

        public void CommitUserIndex(string UserID, string ObjectName, string Key)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "delete from TAB_Sync_User_Data where UserID = @UserID and ObjectName = @ObjectName and [Key] = @Key";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                     new SqlParameter("UserID",UserID),
                    new SqlParameter("ObjectName",ObjectName),
                    new SqlParameter("Key",Key)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }

        public bool ExistUser(string UserID, string ObjectName)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select top 1 UserID from TAB_Sync_User  where ObjectName=@ObjectName and UserID=@UserID"; ;
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("ObjectName",ObjectName)
                };
                return TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count > 0;
            }
        }
        public void RegUser(string UserID, string UserName, string ObjectName)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "insert into TAB_Sync_User (ObjectName,UserID,UserName,CreateTime) values (@ObjectName,@UserID,@UserName,getdate())";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("UserName",UserName),
                    new SqlParameter("ObjectName",ObjectName)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }

        public void UnRegUser(string UserID, string ObjectName)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "delete from TAB_Sync_User where ObjectName=@ObjectName and UserID=@UserID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("ObjectName",ObjectName)
                };
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);

                strSql = "delete from TAB_Sync_User_Data where ObjectName=@ObjectName and UserID=@UserID";
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }


        public void ClearUserIndex(string UserID, string ObjectName)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "delete from TAB_Sync_User_Data where ObjectName=@ObjectName and UserID=@UserID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("ObjectName",ObjectName)
                };
          

                
                TF.DB.DBUtility.SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
            }
        }
    }
    public class DBSyncUser : ISyncSource
    {
        string _ConnString;
        public DBSyncUser(string ConnString)
        {
            _ConnString = ConnString;
        }
        public bool GetObject(string ObjectName, SyncObject Obj)
        {
            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                Obj.ObjectName = ObjectName;
                Obj.ObjectVersion = "";
                string version = "";

                int nVersion = 0;
                int nVersion2 = 0;


                string strSql = "select * from (SELECT OBJECT_NAME(object_id) TableName,is_track_columns_updated_on FROM sys.change_tracking_tables) t WHERE TableName = @ObjectName1 or TableName=@ObjectName2";

                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("ObjectName1","TAB_org_User"),
                    new SqlParameter("ObjectName2","TAB_org_User_Feature")
                };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt.Rows.Count != 2)
                {
                    return false;
                }

                strSql = "SELECT MAX(Sys_Change_Version) as v FROM CHANGETABLE(CHANGES dbo.TAB_org_User,0) as T  ";
                
                dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql).Tables[0];
               
                if (dt.Rows.Count > 0)
                {
                    nVersion = TF.DB.DBConvert.ToInt32(dt.Rows[0]["v"]);                                        
                }

                strSql = "SELECT MAX(Sys_Change_Version) as v FROM CHANGETABLE(CHANGES dbo.TAB_org_User_Feature,0) as T  ";

                dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    nVersion2 = TF.DB.DBConvert.ToInt32(dt.Rows[0]["v"]);
                }
                version = string.Format("{0}+{1}",nVersion,nVersion2);
                Obj.ObjectVersion = version;
                return true;
            }
        }
        public void GetDataList(string ObjectName, List<SyncData> DataList)
        {

            using (SqlConnection Conn = new SqlConnection(_ConnString))
            {
                string strSql = "select * from TAB_Org_User order by UserNumber";
                SqlParameter[] sqlParams = new SqlParameter[] { 

                        };
                DataTable dt = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SyncData d = new SyncData();
                    d.Key = dt.Rows[i]["UserNumber"].ToString();
                    d.Json = GetUserDataJson(Conn, d.Key);
                    d.Version = SyncTool.MD5Encrypt16(d.Json);
                    DataList.Add(d);
                }
            }
        }

        public class UserEntity
        {
            public List<User> UserInfo = new List<User>();
            public List<UserFeature> UserFeatures = new List<UserFeature>();
        }

        public class User
        {
            public string UserNumber = "";
            public string UserName = "";
            public string NameJP = "";
            public string TelNumber = "";
            public string UserGUID = "";
            public string DeptID = "";
            public string DeptName = "";
            public string  PostID = "";
            public string PostName = "";
            public string DeptFullName = "";
           
        }
        public class UserFeature
        {
            public string UserNumber = "";
            public int FeatureType = 0;
            public string FeatureContent = "";
            public int FeatureIndex = 1;
        }
        private string GetUserDataJson(SqlConnection Conn, string Key)
        {

            string JsonString = "";
            UserEntity u = new UserEntity();

            string strSql = "select * from VIEW_Org_User where UserNumber = @UserNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                        new SqlParameter("UserNumber",Key)
                    };
            DataTable dtUser = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                User tempUser = new User();
                tempUser.UserGUID = TF.DB.DBConvert.ToString(dtUser.Rows[i]["UserGUID"]);
                tempUser.UserNumber = TF.DB.DBConvert.ToString(dtUser.Rows[i]["UserNumber"]);
                tempUser.UserName = TF.DB.DBConvert.ToString(dtUser.Rows[i]["UserName"]);
                tempUser.NameJP = TF.DB.DBConvert.ToString(dtUser.Rows[i]["NameJP"]);
                tempUser.TelNumber = TF.DB.DBConvert.ToString(dtUser.Rows[i]["TelNumber"]);
                tempUser.PostID = TF.DB.DBConvert.ToString(dtUser.Rows[i]["PostID"]);
                tempUser.PostName = TF.DB.DBConvert.ToString(dtUser.Rows[i]["PostName"]);
                tempUser.DeptID = TF.DB.DBConvert.ToString(dtUser.Rows[i]["DeptID"]);
                tempUser.DeptName = TF.DB.DBConvert.ToString(dtUser.Rows[i]["DeptName"]);
                tempUser.DeptFullName = TF.DB.DBConvert.ToString(dtUser.Rows[i]["DeptFullName"]);                
                u.UserInfo.Add(tempUser);
            }
       ;
            strSql = "select * from TAB_Org_User_Feature where UserNumber = @UserNumber order by FeatureType,FeatureIndex";
            sqlParams = new SqlParameter[] { 
                        new SqlParameter("UserNumber",Key)
                    };
            DataTable dtFeature = TF.DB.DBUtility.SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dtFeature.Rows.Count; i++)
            {
                UserFeature tempFeature = new UserFeature();
                tempFeature.UserNumber = TF.DB.DBConvert.ToString(dtFeature.Rows[i]["UserNumber"]);
                if (!DBNull.Value.Equals(dtFeature.Rows[i]["FeatureContent"]))
                {
                    
                    tempFeature.FeatureContent = Convert.ToBase64String((byte[])dtFeature.Rows[i]["FeatureContent"]);
                }
                tempFeature.FeatureIndex = TF.DB.DBConvert.ToInt32(dtFeature.Rows[i]["FeatureIndex"]);
                tempFeature.FeatureType = TF.DB.DBConvert.ToInt32(dtFeature.Rows[i]["FeatureType"]);
                u.UserFeatures.Add(tempFeature);
            }
          
           JsonString =  Newtonsoft.Json.JsonConvert.SerializeObject(u);
           return JsonString;


        }
    }
}
