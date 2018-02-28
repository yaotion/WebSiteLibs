using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Org
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class User
    {
        //工号
        public string UserNumber;
        //姓名
        public string UserName;
        //姓名简拼
        public string NameJP; 
        //联系电话
        public string TelNumber;
        //内部GUID
        public string UserGUID;
        //部门编号
        public string DeptID;
        //部门名称
        public string DeptName;
        //职位编号
        public string PostID;
        //职位名称
        public string PostName;
     
        //所有上级部门名称，从小到大，逗号间隔
        public string DeptFullName;
        public string ReorderFullName()
        {
            string[] nameArray = DeptFullName.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> OrgList = new List<string>();
            OrgList.AddRange(nameArray);
            string result = "";
            for (int i = OrgList.Count -1; i >= 0; i--)
            {
                if (i == 0)
                {
                    result = result + OrgList[i];
                }
                else
                {
                    result = result + OrgList[i] + ",";
                }
            }
            return result;
            
        }

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
            User user = (User)obj;

            return
                this.UserNumber.Equals(user.UserNumber) &&
                this.UserName.Equals(user.UserName) &&
                this.NameJP.Equals(user.NameJP) &&
                this.TelNumber.Equals(user.TelNumber) &&
                this.UserGUID.Equals(user.UserGUID) &&
                this.DeptID.Equals(user.DeptID) &&
                this.DeptName.Equals(user.DeptName) &&
                this.PostID.Equals(user.PostID) &&
                this.PostName.Equals(user.PostName) &&
                this.DeptFullName.Equals(user.DeptFullName);
        }
    }
   
    /// <summary>
    /// 人员部门关系
    /// </summary>
    public class UserDeptRelation
    {
        //用户工号
        public string UserNumber;
        //上级部门编号
        public string DeptID;
        //上级部门序号(倒叙，从1开始)
        public int DeptLevel;
        //上级部门名称
        public string DeptName;
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
            UserDeptRelation relation = (UserDeptRelation)obj;

            return
                this.UserNumber.Equals(relation.UserNumber) &&
                this.DeptID.Equals(relation.DeptID) &&
                this.DeptName.Equals(relation.DeptName) &&
                this.DeptLevel.Equals(relation.DeptLevel);
        }
    }

    /// <summary>
    /// 人员特征
    /// </summary>
    public class Feature
    {
        //工号
        public string UserNumber;
        //特征类型
        public int FeatureType;
        //特征内容
        public object FeatureContent;
        //特征位置
        public int FeatureIndex;

        public bool EqualContent(object DestContent)
        {
            if ((FeatureContent == null) && (DestContent == null))
            {
                return true;
            }
            if ((FeatureContent != null) && (DestContent == null))
            {
                return false;
            }
            if ((FeatureContent == null) && (DestContent != null))
            {
                return false;
            }
            byte[] bArray1 = (byte[])FeatureContent;
            byte[] bArray2 = (byte[])DestContent;
            if (bArray1.Length != bArray2.Length) return false;
            for (int i = 0; i < bArray1.Length; i++)
            {
                if (bArray1[i] != bArray2[i])
                {
                    return false;
                }

            }
            return true;
        }
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
            Feature f = (Feature)obj;

           
            return
                this.UserNumber.Equals(f.UserNumber) &&
                this.FeatureIndex.Equals(f.FeatureIndex) &&
                this.EqualContent(f.FeatureContent) &&
                this.FeatureType.Equals(f.FeatureType);                
        }
    }

    /// <summary>
    /// 人员特征类型
    /// </summary>
    public class FeatureType
    {
        //特征类型编号
        public int FeatureTypeID;
        //特征类型名称
        public string FeatureTypeName;
    }

}
