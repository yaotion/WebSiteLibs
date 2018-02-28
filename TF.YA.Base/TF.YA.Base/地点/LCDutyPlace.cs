using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using TF.DB.DBUtility;
using Common.Logging;

namespace TF.YA.Base
{

    public class LCDutyPlace
    {
        public static List<DutyPlace> GetAllPlace(ILog Log, SqlConnection Conn)
        {
            return TF.YA.Base.DBDutyPlace.GetAllPlace(Log, Conn);
        }

        public static void DeletePlace(ILog Log, SqlConnection Conn, string PlaceID)
        {
            TF.YA.Base.DBDutyPlace.DeletePlace(Log, Conn, PlaceID);
        }

        public static bool GetPlace(ILog Log, SqlConnection Conn, string PlaceID, DutyPlace Place)
        {
            return TF.YA.Base.DBDutyPlace.GetPlace(Log, Conn, PlaceID,Place);
        }

        public static void AddPlace(ILog Log, SqlConnection Conn, DutyPlace Place)
        {
            TF.YA.Base.DBDutyPlace.AddPlace(Log, Conn,  Place);
        }

        public static void UpdatePlace(ILog Log, SqlConnection Conn, DutyPlace Place)
        {
            TF.YA.Base.DBDutyPlace.UpdatePlace(Log, Conn, Place);
        }
    }
}
