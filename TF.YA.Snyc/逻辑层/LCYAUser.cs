using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Sync
{
    public class LCYAUser
    {
        public static List<YAUser> GetUserAlls()
        {
            return DBYAUser.GetUserAlls();
        }
        public static int DeleteYAUser(string UserID)
        {
            return DBYAUser.DeleteYAUser(UserID);
        }

        public static int UpdateYAUser(YAUser U)
        {
            if (DBYAUser.ExistYAUser(U.UserNumber))
            {
                return DBYAUser.UpdateYAUser(U);
            }
            return DBYAUser.AddYAUser(U);
        }
    }
}
