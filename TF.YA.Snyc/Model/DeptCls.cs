using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;


namespace TF.YA.Sync
{
    /// <summary>
    ///  部门 
    /// </summary>
    public class DeptCls : SyncClass
    {
          [DataMember]
     public string DeptId{ get; set; } 
          [DataMember]
     public string DeptName{ get; set; } 
     public int  DeptOrder;
     public string DeptType;
          [DataMember]
     public string ParentDeptID{ get; set; } 
     public string ParentDeptName{ get; set; } 
     public string FullParentID{ get; set; } 
     public String FullParentName{ get; set; } 
     public int DeptLevel{ get; set; } 
     public override string Key { get { return DeptId + ParentDeptID; } }

    }
 public class DeptClsList
 {
     public List<DeptCls> Depts;
 }
    /// <summary>
    /// 用户 
    /// </summary>
 public class Users : SyncClass
    {
     [DataMember]
     public string UserNumber { get; set; }
     [DataMember]
     public string UserName { get; set; }
     [DataMember]
     public string NameJP { get; set; }
     [DataMember]
     public string TelNumber { get; set; }
     [DataMember]
     public string DeptID { get; set; }
     public string DeptName { get; set; }
     [DataMember]
     public string PostID { get; set; }
     public String PostName { get; set; }
     public string DeptFullName { get; set; }
     public override string Key { get { return UserNumber; } }

    }

 public class YAUser : SyncClass
 {
     [DataMember]
     public string UserNumber { get; set; }
     [DataMember]
     public string UserName { get; set; }
     [DataMember]
     public string NameJP { get; set; }
     [DataMember]
     public string TelNumber { get; set; }

     [DataMember]
     public string PostID { get; set; }
     public string DeptID { get; set; }
     [DataMember]
     public string DepartFullName { get; set; }
     [DataMember]
     public string Finger1 { get; set; }
     [DataMember]
     public string Finger2 { get; set; }
     [DataMember]
     public string Picture { get; set; }

     public override string Key { get { return UserNumber; } }
     public YAUser()
     {
         UserNumber = "";
         UserName = "";
         NameJP = "";
         TelNumber = "";
         PostID = "0";
         DepartFullName = "--";
         Finger1 = "";
         Finger2 = "";
         Picture = "";
     }
 }
  
    /// <summary>
    /// 岗位
    /// </summary>
    public class Posts : SyncClass
    {
         [DataMember]
        public string PostID {get; set; }
         [DataMember]
         public String PostName { get; set; }
        public int  PostType;
        public override string Key { get { return PostID; } }

    }
    public class  PostALL
    {

        public int Success;
        public String ResultText;
        public PostsList Data;
    }
    public class PostsList
    {

        public List<Posts> Posts;
    }
    /// <summary>
    /// 特征
    /// </summary>
    public class Features
    {
        public string UserNumber;
        public int  FeatureType;
        public String  FeatureContent;
        public int FeatureIndex;

    }
    public class FeatureList
    {

        public List<Features> Features;
    }
    public class FeatureAll
    {
        public int Success;
        public String ResultText;
        public FeatureList Data;
    }
}
