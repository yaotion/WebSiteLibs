using System;
using Common.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Transactions;
using NUnit.Framework;

namespace TF.YA.Org
{
    public class DeptTest
    {
        public SqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString.ToString();
            //string connString = "Data Source=192.168.1.166;Initial Catalog=TestDB;Persist Security Info=True;User ID=sa;Password=Think123;min pool size=1;max pool size=50;Pooling=true";            
            return new SqlConnection(connString);
        }
        public ILog GetLog()
        {
            return LogManager.GetLogger(this.ToString());
        }
        public Dept GetTestDept()
        {
            Dept result = new Dept();
            result.DeptID = "ZDZ1";
            result.DeptName = "1指导组";
            result.DeptOrder = 1;
            result.DeptType = 1;
            result.ParentDeptName = "1车队";
            result.ParentDeptID = "CD1";
            result.FullParentID = "CD1,CJ1,JWD1";
            result.FullParentName = "1对,1车间,1段"; 

            return result;
        }
        public  List<DeptRelation> GetTestDeptRelations()
        {
            List<DeptRelation> result = new List<DeptRelation>();

            DeptRelation relation = new DeptRelation();
            relation.DeptID = "ZDZ1";
            relation.DeptName = "1指导组";

            relation.HigherDepartID = "CD1";
            relation.HigherDepartName = "1车队";
            relation.HigherLevel = 1;
            result.Add(relation);

            relation = new DeptRelation();
            relation.DeptID = "ZDZ1";
            relation.DeptName = "1指导组";

            relation.HigherDepartID = "CJ1";
            relation.HigherDepartName = "1车间";
            relation.HigherLevel = 2;
            result.Add(relation);

            relation = new DeptRelation();
            relation.DeptID = "ZDZ1";
            relation.DeptName = "1指导组";

            relation.HigherDepartID = "JWD1";
            relation.HigherDepartName = "1段";
            relation.HigherLevel = 3;

            result.Add(relation);


            return result;
        }
        public List<DeptRelation> GetTestDept2Relations()
        {
            List<DeptRelation> result = new List<DeptRelation>();

            DeptRelation relation = new DeptRelation();
            relation.DeptID = "CD1";
            relation.DeptName = "1车队";
            relation.HigherDepartID = "CJ1";
            relation.HigherDepartName = "1车间";
            relation.HigherLevel = 1;
            result.Add(relation);

            relation = new DeptRelation();
            relation.DeptID = "CD1";
            relation.DeptName = "1车队";
            relation.HigherDepartID = "JWD1";
            relation.HigherDepartName = "1JWD";
            relation.HigherLevel = 2;
            result.Add(relation);

            return result;
        }
        public List<DeptRelation> GetTestDept3Relations()
        {
            List<DeptRelation> result = new List<DeptRelation>();

            DeptRelation relation = new DeptRelation();
            relation.DeptID = "CJ1";
            relation.DeptName = "1车间";

            relation.HigherDepartID = "JWD1";
            relation.HigherDepartName = "1机务段";
            relation.HigherLevel = 1;
            result.Add(relation);
            return result;
        }

        public List<DeptRelation> GetTestDept4Relations()
        {
            List<DeptRelation> result = new List<DeptRelation>();
            return result;
        }
        public Dept GetTestDept2()
        {
            Dept result = new Dept();
            result.DeptID = "CD1";
            result.DeptName = "1车队";
            result.DeptOrder = 1;
            result.DeptType = 1;
            result.ParentDeptName = "1车间";
            result.ParentDeptID = "CJ1";
            result.FullParentID = "CJ1,JWD1";
            result.FullParentName = "1车间,1段";

            return result;
        }
        public Dept GetTestDept3()
        {
            Dept result = new Dept();
            result.DeptID = "CJ1";
            result.DeptName = "1车间";
            result.DeptOrder = 1;
            result.DeptType = 1;
            result.ParentDeptName = "1段";
            result.ParentDeptID = "JWD1";
            result.FullParentID = "JWD1";
            result.FullParentName = "1段";

            return result;
        }
        public Dept GetTestDept4()
        {
            Dept result = new Dept();
            result.DeptID = "JWD1";
            result.DeptName = "1段";
            result.DeptOrder = 1;
            result.DeptType = 1;
            result.ParentDeptName = "";
            result.ParentDeptID = "";
            result.FullParentID = "";
            result.FullParentName = "";
            return result;
        }

        [Test]
        public void TestAddDept()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();
                //获取测试人员
                Dept dept = GetTestDept();                
                //添加测试人员
                DBOrg.AddDept(Log, Conn, dept);
                Dept dept2 = new Dept();
                //验证测试人员
                if (DBOrg.GetDept(Log, Conn, dept.DeptID, dept2))
                {
                    Assert.AreEqual(dept.Equals(dept2), true);
                }                
            }
        }
        [Test]
        public void AddDeptRelation()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();
                Dept d = GetTestDept();
                //获取测试关系
                List<DeptRelation> relations = GetTestDeptRelations();

                for (int i = 0; i < relations.Count; i++)
                {
                    //添加测试关系
                    DBOrg.AddDeptRelation(Log, Conn, relations[i]);  
                }
                
                List<DeptRelation> relationList = DBOrg.GetDeptRelation(Log, Conn, d.DeptID);
                //验证测试关系
                if (relationList.Count == relations.Count)
                {
                    Assert.AreEqual(relationList.Count == relations.Count, true);
                }
            }
        }
        [Test]
        public void TetGetAllDepart()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();
                //获取机务段
                Dept deptJWD = GetTestDept4();
                //添加机务段
                DBOrg.AddDept(Log, Conn, deptJWD);
                List<DeptRelation> relations = GetTestDept4Relations();
                for (int i = 0; i < relations.Count; i++)
                {
                    DBOrg.AddDeptRelation(Log, Conn, relations[i]);
                }

                //获取机务段
                Dept deptCJ = GetTestDept3();
                //添加机务段
                DBOrg.AddDept(Log, Conn, deptCJ);
                List<DeptRelation> relationCJ = GetTestDept3Relations();
                for (int i = 0; i < relationCJ.Count; i++)
                {
                    DBOrg.AddDeptRelation(Log, Conn, relationCJ[i]);
                }


                //获取机务段
                Dept deptCD = GetTestDept2();
                //添加机务段
                DBOrg.AddDept(Log, Conn, deptCD);
                List<DeptRelation> relationCD = GetTestDept2Relations();
                for (int i = 0; i < relationCD.Count; i++)
                {
                    DBOrg.AddDeptRelation(Log, Conn, relationCD[i]);
                }

                //获取机务段
                Dept deptZDZ = GetTestDept();
                //添加机务段
                DBOrg.AddDept(Log, Conn, deptZDZ);
                List<DeptRelation> relationZDZ = GetTestDeptRelations();
                for (int i = 0; i < relationZDZ.Count; i++)
                {
                    DBOrg.AddDeptRelation(Log, Conn, relationZDZ[i]);
                }

                List<Dept> deptList = DBOrg.GetSubAllDeparts(Log, Conn, deptJWD.DeptID);
                Assert.AreEqual(deptList.Count, 3);
               
            }
        }
    }
}
