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
    [TestFixture]
    public class DBUserTest 
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
        public User GetTestUser()
        {
            User result = new User();
            result.UserGUID = Guid.NewGuid().ToString();
            result.UserNumber = "9999999";
            result.UserName = "测试人员";
            result.TelNumber = "0371-9999999";
            result.NameJP = "CSRY";
            result.DeptID = "D1";
            result.DeptName = "部门1";
            result.PostID = "P1";
            result.PostName = "职位1";
            result.DeptFullName = "部门1,上级部门1,上级部门2";
            return result;
        }
        public Feature GetTestUserFeature()
        {
            Feature result = new Feature();
            result.UserNumber = "9999999";
            result.FeatureIndex = 1;
            result.FeatureType = 1;
            result.FeatureContent = new Byte[] { 1,2,3,4,5,6}; ;
            return result;
        }
        public UserDeptRelation GetTestUserDept()
        {
            UserDeptRelation relation = new UserDeptRelation();
            relation.UserNumber = "9999999";
            relation.DeptID = "D1";
            relation.DeptLevel = 1;
            relation.DeptName = "部门1";
            return relation;
        }
        [Test]
        //[Rollback]
        public void TestAddUser()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();
                //获取测试人员
                User user = GetTestUser();
                
                //添加测试人员
                DBUser.AddUser(Log, Conn, user);

                //验证测试人员
                User user2 = new User();
                if (DBUser.GetUser(Log, Conn, user.UserNumber, user2))
                {
                    Assert.AreEqual(user.Equals(user2), true);
                }
            }
        }
        [Test]    
        public void TestAddUserDept()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();

                //获取测试用户所属部门
                UserDeptRelation relation = GetTestUserDept();
                
                //添加用户所属部门
                DBUser.AddUserDept(Log, Conn, relation);

                //验证用户所属部门
                List<UserDeptRelation> relations = DBUser.GetUserDept(Log, Conn, relation.UserNumber);
                if (relations.Count == 1)
                {
                    Assert.AreEqual(relation.Equals(relations[0]), true);
                }
            }
        }

        [Test]
        public void TestDeleteUserDept()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
             
                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();

                //获取测试用户所属部门
                UserDeptRelation relation = GetTestUserDept();
                //添加用户所属部门
                DBUser.AddUserDept(Log, Conn, relation);
                //删除用户所属部门
                DBUser.DeleteUserDept(Log, Conn, relation.UserNumber);

                List<UserDeptRelation> relations = DBUser.GetUserDept(Log, Conn, relation.UserNumber);
                Assert.AreEqual(relations.Count, 0);
            }
        }

        [Test]
        public void TestQueryUser()
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();

                User user1 = GetTestUser();
                UserDeptRelation user1Dept = GetTestUserDept();
                

                DBUser.AddUser(Log, Conn, user1);
                DBUser.AddUserDept(Log, Conn, user1Dept);

                //验证查询部门
                List<User> userList = DBUser.QueryUser(Log, Conn, user1.DeptID, "", "");
                if (userList.Count == 0)
                {
                    Assert.AreEqual(true,false);
                }

                //验证查询不到部门
                userList = DBUser.QueryUser(Log, Conn, user1.DeptID + "-", "", "");
                if (userList.Count > 0)
                {
                    Assert.AreEqual(true, false);
                }

                //验证查询工号
                userList = DBUser.QueryUser(Log, Conn, "", user1.UserNumber, "");
                if (userList.Count == 0)
                {
                    Assert.AreEqual(true, false);
                }

                //验证查询不到工号
                userList = DBUser.QueryUser(Log, Conn, "", user1.UserNumber + "1", "");
                if (userList.Count > 0)
                {
                    Assert.AreEqual(true, false);
                }


                //验证查询姓名
                userList = DBUser.QueryUser(Log, Conn, "", "",  user1.UserName.Substring(0,1));
                if (userList.Count == 0)
                {
                    Assert.AreEqual(true, false);
                }

                //验证查询不到工号
                userList = DBUser.QueryUser(Log, Conn, "", "", user1.UserName + "-");
                if (userList.Count > 0)
                {
                    Assert.AreEqual(true, false);
                }

                //验证查询所有信息
                userList = DBUser.QueryUser(Log, Conn, user1.DeptID, user1.UserNumber, user1.UserName.Substring(0, 1));
                Assert.AreEqual(userList.Count, 1);
            }
        }

        [Test]
        public void TestAddFeature()
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();

                Feature f = GetTestUserFeature();

                DBUser.AddFeature(Log,Conn, f);

                List<Feature> featureList =  DBUser.GetUserFeature(Log, Conn, f.UserNumber);
                if (featureList.Count == 1)
                {
                    Assert.AreEqual(f.Equals(featureList[0]), true);
                }
            }
        }

        [Test]
        public void TestUpdateFeature()
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                //获取连接日志
                ILog Log = GetLog();
                SqlConnection Conn = GetConnection();

                Feature f = GetTestUserFeature();

                DBUser.AddFeature(Log, Conn, f);
                f.FeatureContent = new Byte[] { 6,5,4,3,2,1};
                DBUser.UpdateFeature(Log,Conn,f);

                List<Feature> featureList = DBUser.GetUserFeature(Log, Conn, f.UserNumber);
                if (featureList.Count == 1)
                {
                    Assert.AreEqual(f.Equals(featureList[0]), true);
                }
            }
        }
    }
}
