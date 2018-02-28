using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TF.YA.Sync
{
  public  class DutyUser:  SyncClass
    {
       [DataMember]
      public string DutyUserNumber{ get; set; }
       [DataMember]
      public string DutyUserName{ get; set; }
       [DataMember]
      public string RoleID{ get; set; }
       
      public string RoleName{ get; set; }
       [DataMember]
      public string Password{ get; set; }
       public override string Key { get { return DutyUserNumber; } }
    }
  public class DutyUserAll
  {
      public int Success;
      public String ResultText;
      public DATa Data;
  }
  public class DATa
  {
      public int TotalCount;
      public List<DutyUser> DutyUsers;
  }
}
