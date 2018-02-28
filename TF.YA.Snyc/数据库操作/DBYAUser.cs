using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using TF.DB.DBUtility;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace TF.YA.Sync
{
    public class DBYAUser
    {
        public static List<YAUser> GetUserAlls()
        {
            List<YAUser> result = new List<YAUser>();
            string sql = "select strTrainmanNumber,strTrainmanName,nPostID,strTelNumber,strJP,strWorkShopGUID,strGuideGroupGUID,strAreaGUID,FingerPrint1,FingerPrint2,Picture  from TAB_Org_Trainman";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YAUser u = new YAUser();
                u.UserNumber = dt.Rows[i]["strTrainmanNumber"].ToString();
                u.UserName = dt.Rows[i]["strTrainmanName"].ToString().Trim();
                u.TelNumber = dt.Rows[i]["strTelNumber"].ToString();

                u.PostID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["nPostID"]).ToString();
                u.NameJP = dt.Rows[i]["strJP"].ToString();
                u.DepartFullName = string.Format("{0}-{1}-{2}", TF.DB.DBConvert.ToString(dt.Rows[i]["strAreaGUID"]),
                    TF.DB.DBConvert.ToString(dt.Rows[i]["strWorkShopGUID"]), TF.DB.DBConvert.ToString(dt.Rows[i]["strGuideGroupGUID"]));
                u.Finger1 = "";
                if (!DBNull.Value.Equals(dt.Rows[i]["FingerPrint1"]))
                {
                    u.Finger1 = Convert.ToBase64String(((byte[])dt.Rows[i]["FingerPrint1"]));
                }
                u.Finger2 = "";
                if (!DBNull.Value.Equals(dt.Rows[i]["FingerPrint2"]))
                {
                    u.Finger2 = Convert.ToBase64String(((byte[])dt.Rows[i]["FingerPrint2"]));
                }
                u.Picture = "";
                if (!DBNull.Value.Equals(dt.Rows[i]["Picture"]))
                {
                    u.Picture = Convert.ToBase64String(((byte[])dt.Rows[i]["Picture"]));
                }
                result.Add(u);
            }
            return result;
        }
        public static int AddYAUser(YAUser U)
        {
            string postID = U.PostID;
            if (string.IsNullOrEmpty(postID))
                postID = "1";
            string strSql = @"insert into TAB_Org_Trainman(strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strTelNumber,strJP,strWorkShopGUID,
                    strGuideGroupGUID,strAreaGUID,nTrainmanState,FingerPrint1,FingerPrint2,Picture) values(@strTrainmanGUID,@userid ,@username,@postid ,@telnum,@namejp ,@strWorkShopGUID,
                    @strGuideGroupGUID,@strAreaGUID,7,@FingerPrint1,@FingerPrint2,@Picture)";

            SqlParameter[] sqlparam = new SqlParameter[]{
                            new SqlParameter("strTrainmanGUID",U.UserNumber), 
                            new SqlParameter("userid",U.UserNumber), 
                            new SqlParameter("postid",int.Parse(U.PostID)), 
                            new SqlParameter("username",U.UserName), 
                            new SqlParameter("telnum",U.TelNumber), 
                            new SqlParameter("namejp",U.NameJP), 
                            new SqlParameter("strWorkShopGUID",U.DepartFullName.Split(new char[]{'-'})[1]), 
                            new SqlParameter("strGuideGroupGUID",U.DepartFullName.Split(new char[]{'-'})[2]), 
                            new SqlParameter("strAreaGUID",U.DepartFullName.Split(new char[]{'-'})[0]),
                            new SqlParameter("FingerPrint1",Convert.FromBase64String(U.Finger1)),
                            new SqlParameter("FingerPrint2",Convert.FromBase64String(U.Finger2)),
                            new SqlParameter("Picture",Convert.FromBase64String(U.Picture))
           };
            return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, strSql, sqlparam);
        }

        public static int UpdateYAUser(YAUser U)
        {
            string postID = U.PostID;
            if (string.IsNullOrEmpty(postID))
                postID = "1";
            string strSql = @"update TAB_Org_Trainman set strTrainmanName=@username,nPostID=@postid,
           strTelNumber=@telnum,strJP=@namejp,strWorkShopGUID=@strWorkShopGUID,strGuideGroupGUID=@strGuideGroupGUID,strAreaGUID=@strAreaGUID,FingerPrint1=@FingerPrint1,
            FingerPrint2=@FingerPrint2,Picture=@Picture where strTrainmanNumber=@userid";

            SqlParameter[] sqlparam = new SqlParameter[]{
                            new SqlParameter("strTrainmanGUID",U.UserNumber), 
                            new SqlParameter("userid",U.UserNumber), 
                            new SqlParameter("postid",int.Parse(U.PostID)), 
                            new SqlParameter("username",U.UserName), 
                            new SqlParameter("telnum",U.TelNumber), 
                            new SqlParameter("namejp",U.NameJP), 
                            new SqlParameter("strWorkShopGUID",U.DepartFullName.Split(new char[]{'-'})[1]), 
                            new SqlParameter("strGuideGroupGUID",U.DepartFullName.Split(new char[]{'-'})[2]), 
                            new SqlParameter("strAreaGUID",U.DepartFullName.Split(new char[]{'-'})[0]),
                            new SqlParameter("FingerPrint1",Convert.FromBase64String(U.Finger1)),
                            new SqlParameter("FingerPrint2",Convert.FromBase64String(U.Finger2)),
                            new SqlParameter("Picture",Convert.FromBase64String(U.Picture))
           };
            return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, strSql, sqlparam);
        }
        public static int DeleteYAUser(string UserID)
        {
            string strSql = @"delete from TAB_Org_Trainman where strTrainmanNumber=@strTrainmanGUID";
            SqlParameter[] sqlparam = new SqlParameter[]{
                            new SqlParameter("strTrainmanGUID",UserID)
            };
            return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, strSql, sqlparam);
        }

        public static bool ExistYAUser(string UserID)
        {
            string strSql = "select top 1 strTrainmanNumber  from TAB_Org_Trainman where strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strTrainmanNumber",UserID)
            };
            return SqlHelper.ExecuteDataset(PublicVar.Connstr, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count > 0;
        }
        public static List<Posts> GetPostAll()
        {

            List<Posts> LstObjPosts = new List<Posts>();
            string sql = "select nPost,strPostName  from TAB_System_Post";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Posts ObjPosts = new Posts();
                ObjPosts.PostName = dt.Rows[i]["strPostName"].ToString();
                ObjPosts.PostID = dt.Rows[i]["nPost"].ToString();

                LstObjPosts.Add(ObjPosts);
            }
            return LstObjPosts;
        }
        /// <summary>
        /// 保存职位 
        /// </summary>
        /// <param name="polst"></param>
        /// <returns></returns>
        public static int SavePost(Posts polst)
        {
            string id = polst.PostID;
            string name = polst.PostName;
            int type = polst.PostType;
            string sql = "insert into TAB_System_Post(nPost,strPostName)values(@id,@name)";
            SqlParameter[] sqlparam = new SqlParameter[]{
                new SqlParameter("id",int.Parse(id)),
                new SqlParameter("name",name) 
                };
            return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
        }
        public static int UpdatePost(Posts polst)
        {
            string id = polst.PostID;
            string name = polst.PostName;
            int type = polst.PostType;
            object obj = GetPost(int.Parse(id), name);
            if (obj == null || obj == DBNull.Value)
            {
                string sql = "update TAB_System_Post set strPostName=@name where nPost=@id";
                SqlParameter[] sqlparam = new SqlParameter[]{
                    new SqlParameter("id",int.Parse(id)),
                    new SqlParameter("name",name) 
                    };
                return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
            }
            return 0;
        }
        public static int DeletePost(Posts polst)
        {
            string id = polst.PostID;
            string name = polst.PostName;
            int type = polst.PostType;
            object obj = GetPost(int.Parse(id), name);
            if (obj == null || obj == DBNull.Value)
            {
                string sql = "delete from  TAB_System_Post  where nPost=@id";
                SqlParameter[] sqlparam = new SqlParameter[]{
                new SqlParameter("id",int.Parse(id)) 
                };
                return SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
            }
            return 0;
        }
        /// <summary>
        /// 判断职位
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetPost(int id, string name)
        {
            string sql = "select nPost  from  TAB_System_Post WHERE  nPost=@id AND strPostName=@name";
            SqlParameter[] sqlparam = new SqlParameter[]{
                new SqlParameter("id",id),
                new SqlParameter("name",name) 
                };
            return SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql, sqlparam);

        }
    }
}
