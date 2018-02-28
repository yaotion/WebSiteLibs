using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;

namespace TF.YA.Org
{
    public class DBOrg
    {
        private static void DBToDepart(DataRow DR,Dept d)
        {
            d.DeptID = TF.DB.DBConvert.ToString(DR["DeptID"]);
            d.DeptName = TF.DB.DBConvert.ToString(DR["DeptName"]);
            d.DeptOrder = TF.DB.DBConvert.ToInt32(DR["DeptOrder"]);
            d.DeptType = TF.DB.DBConvert.ToInt32(DR["DeptType"]);
            d.DeptLevel = TF.DB.DBConvert.ToInt32(DR["DeptLevel"]);
            d.FullParentID = TF.DB.DBConvert.ToString(DR["FullParentID"]);
            d.FullParentName = TF.DB.DBConvert.ToString(DR["FullParentName"]);
            d.ParentDeptID = TF.DB.DBConvert.ToString(DR["ParentDeptID"]);
            d.ParentDeptName = TF.DB.DBConvert.ToString(DR["ParentDeptName"]);
            d.DeptData = TF.DB.DBConvert.ToString(DR["DeptData"]);
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="Depart">新部门</param>
        /// <param name="ParentDepart">上层部门</param>
        public static void AddDept(ILog Log, SqlConnection Conn, Dept Depart)
        {
            string strSql = @"insert into TAB_Org_Dept (DeptID,DeptName,DeptType,DeptOrder,DeptLevel,ParentDeptID,ParentDeptName,
                                FullParentID,FullParentName,DeptData) 
                values (@DeptID,@DeptName,@DeptType,@DeptOrder,@DeptLevel,@ParentDeptID,@ParentDeptName,
                                @FullParentID,@FullParentName,@DeptData)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DeptID",Depart.DeptID),
                                           new SqlParameter("DeptName",Depart.DeptName),
                                           new SqlParameter("DeptType",Depart.DeptType),
                                           new SqlParameter("DeptOrder",Depart.DeptOrder),
                                           new SqlParameter("DeptLevel",Depart.DeptLevel),
                                           new SqlParameter("ParentDeptID",Depart.ParentDeptID),
                                           new SqlParameter("ParentDeptName",Depart.ParentDeptName),
                                           new SqlParameter("FullParentID",Depart.FullParentID),
                                           new SqlParameter("FullParentName",Depart.FullParentName),
                                           new SqlParameter("DeptData",Depart.DeptData)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static bool GetDept(ILog Log, SqlConnection Conn,string DeptID, Dept Depart)
        {
            string strSql = @"select * from TAB_Org_Dept where DeptID = @DeptID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DeptID",DeptID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DBToDepart(dt.Rows[0],Depart);                
                return true;
            }
            return false;
        }
        public static bool GetDeptByFullName(ILog Log, SqlConnection Conn, string FullDeptName, Dept Depart)
        {
            string strSql = @"select * from TAB_Org_Dept where FullParentName = @FullDeptName or FullParentName = @FullDeptName2";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("FullDeptName",FullDeptName),
                                           new SqlParameter("FullDeptName2",FullDeptName + ",")
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DBToDepart(dt.Rows[0], Depart);
                return true;
            }
            return false;
        }
        public static void AddDeptRelation(ILog Log, SqlConnection Conn, DeptRelation Relation)
        {
            string strSql = @"insert into TAB_Org_Dept_Relation (DeptID,DeptName,HigherDepartID,HigherLevel,HigherDepartName) 
                values (@DeptID,@DeptName,@HigherDepartID,@HigherLevel,@HigherDepartName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DeptID",Relation.DeptID),
                                           new SqlParameter("DeptName",Relation.DeptName),
                                           new SqlParameter("HigherDepartID",Relation.HigherDepartID),
                                           new SqlParameter("HigherLevel",Relation.HigherLevel),
                                           new SqlParameter("HigherDepartName",Relation.HigherDepartName)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static List<DeptRelation> GetDeptRelation(ILog Log, SqlConnection Conn, string DeptID)
        {
            List<DeptRelation> result = new List<DeptRelation>();
            string strSql = @"select * from TAB_Org_Dept_Relation where DeptID = @DeptID order by HigherLevel";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("DeptID",DeptID)                                          
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptRelation r = new DeptRelation();
                r.DeptID = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptID"]);
                r.DeptName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptName"]);
                r.HigherDepartID = TF.DB.DBConvert.ToString(dt.Rows[i]["HigherDepartID"]);
                r.HigherLevel = TF.DB.DBConvert.ToInt32(dt.Rows[i]["HigherLevel"]);
                r.HigherDepartName = TF.DB.DBConvert.ToString(dt.Rows[i]["HigherDepartName"]);
                result.Add(r);
                
            }
            return result;
        }
        public static void UpdateDept(ILog Log, SqlConnection Conn,Dept D)
        {
            string strSql = @"update TAB_Org_Dept set DeptName=@DeptName,DeptType=@DeptType,DeptLevel=@DeptLevel,
                    FullParentID=@FullParentID,FullParentName=@FullParentName,ParentDeptID=@ParentDeptID,
                    ParentDeptName=@ParentDeptName where DeptID = @DeptID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DeptName",D.DeptName),
                new SqlParameter("DeptType",D.DeptType),
                new SqlParameter("DeptLevel",D.DeptLevel),
                new SqlParameter("DeptID",D.DeptID),
                new SqlParameter("FullParentID",D.FullParentID),
                new SqlParameter("FullParentName",D.FullParentName),
                new SqlParameter("ParentDeptID",D.ParentDeptID),
                new SqlParameter("ParentDeptName",D.ParentDeptName)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void UpdateDeptData(ILog Log, SqlConnection Conn, string DeptID, string DeptData)
        {
            string strSql = @"update TAB_Org_Dept set DeptData=@DeptData where DeptID = @DeptID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DeptID",DeptID),
                new SqlParameter("DeptData",DeptData)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteDept(ILog Log, SqlConnection Conn, string DeptID)
        {
            string strSql = @"delete from TAB_Org_Dept where DeptID = @DeptID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DeptID",DeptID)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeleteDeptRelation(ILog Log, SqlConnection Conn, string DeptID)
        {
            string strSql = @"delete from TAB_Org_Dept_Relation where DeptID = @DeptID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DeptID",DeptID)
            };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        /// <summary>
        /// 获取所有下级部门
        /// </summary>
        /// <param name="RootDepartID">根部门编号，为空字符串时为所有部门</param>
        /// <returns>部门列表</returns>
        public static List<Dept> GetSubAllDeparts(ILog Log, SqlConnection Conn, string RootDepartID)
        {
            List<Dept> result = new List<Dept>();
            string strSql = @"select * from TAB_Org_Dept where DeptID in 
                                (select DeptID from TAB_Org_Dept_Relation where HigherDepartID = @HigherDepartID) order by DeptLevel,DeptOrder";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("HigherDepartID",RootDepartID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dept d = new Dept();
                DBToDepart(dt.Rows[i], d);                
                result.Add(d);
            }
            return result;
        }

        public static List<Dept> GetSubOnlyDeparts(ILog Log, SqlConnection Conn, string ParentID)
        {
            List<Dept> result = new List<Dept>();
            string strSql = @"select * from TAB_Org_Dept where ParentDeptID = @ParentDeptID order by DeptLevel,DeptOrder";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("ParentDeptID",ParentID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dept d = new Dept();
                DBToDepart(dt.Rows[i], d);
                result.Add(d);
            }
            return result;
        }
        
        public static List<Dept> GetAllDeparts(ILog Log, SqlConnection Conn)
        {
            List<Dept> result = new List<Dept>();
            string strSql = @"select * from TAB_Org_Dept order by DeptLevel,DeptOrder";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dept d = new Dept();
                DBToDepart(dt.Rows[i], d);                
                result.Add(d);
            }
            return result;
        }
        
        public static List<Post> GetAllPosts(ILog Log, SqlConnection Conn)
        {
            List<Post> result = new List<Post>();
            string strSql = @"select * from TAB_Org_Post order by CAST (PostID AS int)";
            SqlParameter[] sqlParams = new SqlParameter[] { };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Post d = new Post();
                d.PostID = TF.DB.DBConvert.ToString(dt.Rows[i]["PostID"]);
                d.PostName = TF.DB.DBConvert.ToString(dt.Rows[i]["PostName"]);
                d.PostType = TF.DB.DBConvert.ToInt32(dt.Rows[i]["PostType"]);
                result.Add(d);
            }
            return result;
        }
        public static void AddPost(ILog Log, SqlConnection Conn,Post P)
        {
            string strSql = @"insert into TAB_Org_Post (PostID,PostName,PostType) 
                values (@PostID,@PostName,@PostType)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostID",P.PostID),
                                           new SqlParameter("PostName",P.PostName),
                                           new SqlParameter("PostType",P.PostType)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void UpdatePost(ILog Log, SqlConnection Conn, Post P)
        {
            string strSql = @"update TAB_Org_Post set PostName=@PostName,PostType=@PostType where PostID=@PostID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostID",P.PostID),
                                           new SqlParameter("PostName",P.PostName),
                                           new SqlParameter("PostType",P.PostType)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }
        public static void DeletePost(ILog Log, SqlConnection Conn, string PostID)
        {
            string strSql = @"delete from TAB_Org_Post where PostID=@PostID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostID",PostID)
                                       };
            SqlHelper.ExecuteNonQuery(Conn, CommandType.Text, strSql, sqlParams);
        }

        public static bool GetPost(ILog Log, SqlConnection Conn, string PostID,Post P)
        {
            string strSql = @"select * from TAB_Org_Post where PostID=@PostID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostID",PostID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                P.PostID = TF.DB.DBConvert.ToString(dt.Rows[0]["PostID"]);
                P.PostName = TF.DB.DBConvert.ToString(dt.Rows[0]["PostName"]);
                P.PostType = TF.DB.DBConvert.ToInt32(dt.Rows[0]["PostType"]);                
                return true;
            }
            return false;
        }
        public static bool GetPostByName(ILog Log, SqlConnection Conn, string PostName, Post P)
        {
            string strSql = @"select * from TAB_Org_Post where PostName=@PostName";
            SqlParameter[] sqlParams = new SqlParameter[]{
                                           new SqlParameter("PostName",PostName)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                P.PostID = TF.DB.DBConvert.ToString(dt.Rows[0]["PostID"]);
                P.PostName = TF.DB.DBConvert.ToString(dt.Rows[0]["PostName"]);
                P.PostType = TF.DB.DBConvert.ToInt32(dt.Rows[0]["PostType"]);
                return true;
            }
            return false;
        }
        public static List<PostType> GetAllPostType(ILog Log, SqlConnection Conn)
        {
            List<PostType> result = new List<PostType>();
            string strSql = @"select * from TAB_Org_Post_Type order by PostType";
            SqlParameter[] sqlParams = new SqlParameter[]{};
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PostType d = new PostType();
                d.PostTypeID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["PostType"]);
                d.PostTypeName = TF.DB.DBConvert.ToString(dt.Rows[i]["PostTypeName"]);                
                result.Add(d);
            }
            return result;
        }

        public static List<DeptType> GetAllDeptType(ILog Log, SqlConnection Conn)
        {
            List<DeptType> result = new List<DeptType>();
            string strSql = @"select * from TAB_Org_DeptType order by DeptType";
            SqlParameter[] sqlParams = new SqlParameter[]{ };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptType d = new DeptType();
                d.DeptTypeID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["DeptType"]);
                d.DeptTypeName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptTypeName"]);
                d.DeptTypeDS = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptTypeDS"]);
                result.Add(d);
            }
            return result;
        }

        public static bool GetDeptType(ILog Log, SqlConnection Conn, int DeptTypeID, DeptType DT)
        {
            string strSql = @"select top 1 * from TAB_Org_DeptType where DeptType=@DeptType ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("DeptType",DeptTypeID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(Conn, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DT.DeptTypeID = TF.DB.DBConvert.ToInt32(dt.Rows[i]["DeptType"]);
                DT.DeptTypeName = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptTypeName"]);
                DT.DeptTypeDS = TF.DB.DBConvert.ToString(dt.Rows[i]["DeptTypeDS"]);
                return true;
            }
            return false;
        }
    }
}
