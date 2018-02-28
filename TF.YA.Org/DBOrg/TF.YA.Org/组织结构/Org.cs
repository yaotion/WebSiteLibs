using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Org
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Dept
    {
        //部门编号
        public string DeptID = "";
        //部门名称
        public string DeptName = "";
        //上级部门下的序号
        public int DeptOrder = 1;
        //部门类型
        public int DeptType = 1;
        //部门级别
        public int DeptLevel = 1;
        //上级部门编号
        public string ParentDeptID = "";
        //上级部门名称
        public string ParentDeptName = "";
        //全部上级部门编号
        public string FullParentID = "";
        //全怒上级部门名称
        public string FullParentName = "";
        //
        public string DeptData = "";
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            Dept dept = (Dept)obj;

            return
                this.DeptID.Equals(dept.DeptID) &&
                this.DeptName.Equals(dept.DeptName) &&
                this.DeptOrder.Equals(dept.DeptOrder) &&
                this.DeptType.Equals(dept.DeptType) &&
                this.ParentDeptID.Equals(dept.ParentDeptID) &&
                this.ParentDeptName.Equals(dept.ParentDeptName) &&
                this.FullParentID.Equals(dept.FullParentID) &&
                this.DeptData.Equals(dept.DeptData) &&
                this.FullParentName.Equals(dept.FullParentName);
        }
    }
    public class DeptRelation
    {
        //部门编号
        public string DeptID;
        //部门名称
        public string DeptName;
        //上级部门序号(倒叙，从1开始)
        public string HigherDepartID;
        //上级部门相对级别
        public int HigherLevel;
        //上级部门名称
        public string HigherDepartName;
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            DeptRelation relation = (DeptRelation)obj;

            return
                this.DeptID.Equals(relation.DeptID) &&
                this.DeptName.Equals(relation.DeptName) &&
                this.HigherDepartID.Equals(relation.HigherDepartID) &&
                this.HigherLevel.Equals(relation.HigherLevel) &&
                this.HigherDepartName.Equals(relation.HigherDepartName);
            
        }
    }
    public class Post
    {
        //职位编号
        public string PostID ;
        //职位名称
        public string PostName;
        //职位类型
        public int PostType;
        //职位类型名称
        public string PostTypeName;
    }
    
    public class PostType
    {
        public int PostTypeID;
        public string PostTypeName;
    }
    public class DeptType
    {
        //部门类型编号
        public int DeptTypeID;
        //部门类型名称
        public string DeptTypeName;
        //部门类型数据类型
        public string DeptTypeDS;
    }

    public class DeptData
    {
        public Dictionary<string, object> columns;
        public Dictionary<string, object> datas;
    }


}
