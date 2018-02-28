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
    public class LCUser
    {

        public static void AddUser(ILog Log, SqlConnection Conn, User AUser)
        {
            DBUser.AddUser(Log,Conn,AUser);
        }
        public static void UpdateUser(ILog Log, SqlConnection Conn, User AUser)
        {
            DBUser.UpdateUser(Log, Conn, AUser);
        }
        public static void DeleteUser(ILog Log, SqlConnection Conn, string UserNumber)
        {
            DBUser.DeleteUser(Log, Conn, UserNumber);
            DBUser.DeleteUserFeature(Log, Conn, UserNumber);
        }
        public static bool GetUser(ILog Log, SqlConnection Conn, string UserNumber, User AUser)
        {
            return DBUser.GetUser(Log,Conn,UserNumber,AUser);
        }
        public static List<User> QueryUser(ILog Log, SqlConnection Conn, int PageIndex, int PageCount, string DeptID, string UserNumber, string UserName)
        {
            return DBUser.QueryUser(Log,Conn,PageIndex,PageCount,DeptID,UserNumber,UserName);
        }

        public static int QueryUserCount(ILog Log, SqlConnection Conn, string DeptID, string UserNumber, string UserName)
        {
            return DBUser.QueryUserCount(Log, Conn, DeptID, UserNumber, UserName);
        }

        public static List<Feature> GetUserFeatures(ILog Log,SqlConnection Conn,string UserNumber)
        {
            return DBUser.GetUserFeature(Log, Conn, UserNumber);
        }

        public static void UpdateUserFeatures(ILog Log,SqlConnection Conn,Feature UserFeature)
        {
            DBUser.UpdateFeature(Log, Conn, UserFeature);
        }

    }
}
