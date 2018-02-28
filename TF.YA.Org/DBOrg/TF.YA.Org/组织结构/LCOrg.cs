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
    public class LCOrg
    {

        public static List<Post> GetAllPosts(ILog Log, SqlConnection Conn)
        {
            List<Post> result = DBOrg.GetAllPosts(Log, Conn);
            List<PostType> types = DBOrg.GetAllPostType(Log, Conn);
            for (int i = 0; i < result.Count; i++)
            {
                for (int k = 0; k < types.Count; k++)
                {
                    if (result[i].PostType == types[k].PostTypeID)
                    {
                        result[i].PostTypeName = types[k].PostTypeName;
                        break;
                    }
                }                
            }
            return result;
        }
        public static List<PostType> GetAllPostType(ILog Log, SqlConnection Conn)
        {
            return DBOrg.GetAllPostType(Log, Conn);
        }
        public static void DeletePost(ILog Log, SqlConnection Conn, string PostID)
        {
            DBOrg.DeletePost(Log, Conn,PostID);
        }
        public static void AddPost(ILog Log, SqlConnection Conn,Post P)
        {
            DBOrg.AddPost(Log, Conn, P);
        }

        public static void UpdatePost(ILog Log, SqlConnection Conn, Post P)
        {
            DBOrg.UpdatePost(Log,Conn,P);
        }
        public static bool GetPost(ILog Log, SqlConnection Conn, string PostID, Post P)
        {
            return DBOrg.GetPost(Log,Conn,PostID,P);
        }
        public static bool GetPostByName(ILog Log, SqlConnection Conn, string PostName, Post P)
        {
            return DBOrg.GetPostByName(Log,Conn,PostName,P);
        }

        public static List<DeptType> GetAllDeptType(ILog Log, SqlConnection Conn)
        {
            return DBOrg.GetAllDeptType(Log,Conn);
        }

        public static bool GetDeptType(ILog Log, SqlConnection Conn, int DeptTypeID, DeptType DT)
        {
            return DBOrg.GetDeptType(Log, Conn, DeptTypeID, DT);
        }
        public static List<Dept> GetAllDepts(ILog Log, SqlConnection Conn)
        {
            return DBOrg.GetAllDeparts(Log, Conn);
        }
        public static void UpdateDept(ILog Log, SqlConnection Conn,string ParentDeptID,Dept D)
        {
            
                List<DeptRelation> relations = new List<DeptRelation>();
                TF.YA.Org.Dept dp = new Dept();
                if (DBOrg.GetDept(Log, Conn, ParentDeptID, dp))
                {
                    relations = DBOrg.GetDeptRelation(Log, Conn, dp.DeptID);
                }
                //先清除以前的关系
                DBOrg.DeleteDeptRelation(Log, Conn, D.DeptID);

                //将自己也作为上级部门以便检索
                DeptRelation pRelation = new DeptRelation();
                pRelation.DeptID = D.DeptID;
                pRelation.DeptName = D.DeptName;
                pRelation.HigherDepartID = D.DeptID;
                pRelation.HigherDepartName = D.DeptName;
                pRelation.HigherLevel = 0;
                relations.Insert(0, pRelation);
                D.FullParentName = "";
                D.FullParentID = "";
                for (int i = 0; i < relations.Count; i++)
                {
                    relations[i].HigherLevel = relations[i].HigherLevel + 1;
                    relations[i].DeptID = D.DeptID; ;
                    relations[i].DeptName = D.DeptName;

                    D.FullParentName = D.FullParentName + relations[i].HigherDepartName + ",";
                    D.FullParentID = D.FullParentID + relations[i].HigherDepartID + ",";
                    //添加新关系
                    DBOrg.AddDeptRelation(Log, Conn, relations[i]);
                }
                //修改部门
                DBOrg.UpdateDept(Log, Conn, D);

          
        }

        public static void UpdateDeptData(ILog Log, SqlConnection Conn, string DeptID, string DeptData)
        {
            DBOrg.UpdateDeptData(Log, Conn, DeptID, DeptData);
        }
        
        public static void AddDept(ILog Log, SqlConnection Conn, string ParentDeptID, Dept D)
        {
            List<DeptRelation> relations = new List<DeptRelation>();
            TF.YA.Org.Dept dp = new Dept();
            if (DBOrg.GetDept(Log, Conn, ParentDeptID, dp))
            {
                relations = DBOrg.GetDeptRelation(Log, Conn, dp.DeptID);
                D.DeptLevel = dp.DeptLevel + 1;
                List<Dept> subDepts = LCOrg.GetSubOnlyDeparts(Log, Conn, dp.DeptID);
                D.DeptOrder = subDepts.Count;
            }

            D.DeptLevel = 0;
            D.DeptOrder = 0;
                      

            //将自己也作为上级部门以便检索
            DeptRelation pRelation = new DeptRelation();
            pRelation.DeptID = D.DeptID;
            pRelation.DeptName = D.DeptName;
            pRelation.HigherDepartID = D.DeptID;
            pRelation.HigherDepartName = D.DeptName;
            pRelation.HigherLevel = 0;
            relations.Insert(0, pRelation);

            for (int i = 0; i < relations.Count; i++)
            {
                relations[i].HigherLevel = relations[i].HigherLevel + 1;
                relations[i].DeptID = D.DeptID; ;
                relations[i].DeptName = D.DeptName; 
                D.FullParentName = D.FullParentName + relations[i].HigherDepartName + ",";
                D.FullParentID = D.FullParentID + relations[i].HigherDepartID + ",";
                DBOrg.AddDeptRelation(Log, Conn, relations[i]);
            }
            DBOrg.AddDept(Log, Conn, D);
        }

        public static bool GetDept(ILog Log, SqlConnection Conn, string DeptID, Dept ADept)
        {
            return DBOrg.GetDept(Log, Conn, DeptID, ADept);
        }

        public static bool GetDeptByFullName(ILog Log, SqlConnection Conn, string FullDeptName, Dept Depart)
        {
            return DBOrg.GetDeptByFullName(Log, Conn, FullDeptName, Depart);
        }
        public static List<Dept> GetSubOnlyDeparts(ILog Log, SqlConnection Conn, string DepartID)
        {
            return DBOrg.GetSubOnlyDeparts(Log, Conn, DepartID);
        }

        public static void DeleteDept(ILog Log, SqlConnection Conn, string DeptID)
        {
            Dept dept = new Dept();
            if (!DBOrg.GetDept(Log, Conn, DeptID, dept))
            {
                throw new Exception("无效的部门编号");
            }
            if (DBOrg.GetSubOnlyDeparts(Log, Conn, DeptID).Count > 0)
            {
                throw new Exception("非空的部门");
            }            
            DBOrg.DeleteDept(Log, Conn, DeptID);
            DBOrg.DeleteDeptRelation(Log, Conn, DeptID);
            List<Dept> opDepts = DBOrg.GetSubOnlyDeparts(Log, Conn, dept.ParentDeptID);
            for (int i = 0; i < opDepts.Count; i++)
            {
                opDepts[i].DeptOrder = i;
                DBOrg.UpdateDept(Log, Conn, opDepts[i]);
            }
        }
    }
}
