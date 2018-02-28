using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Org
{
    public class DutyUserPost
    {
        //开通值班员的职位
        public int PostTypeID;

        //开通值班员的职位
        public string PostTypeName;
    }

    public class DutyUser
    {
        public static string DefaultPWD = "123456";
        public string DutyUserNumber;
        public string DutyUserName;        
        public string RoleID;
        public string Password;
        public string TokenID;
        public DateTime TokenTime;
        public string RoleName;
    }
}
