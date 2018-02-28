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
    public class LCICSection
    {
        public static List<ICSection> GetAllSections(ILog Log, SqlConnection Conn)
        {
            return TF.YA.Base.DBICSection.GetAllSections(Log, Conn);
        }

        
        public static void AddSection(ILog Log, SqlConnection Conn, ICSection Section)
        {
            TF.YA.Base.DBICSection.AddSection(Log, Conn, Section);
        }
        public static void UpdateSection(ILog Log, SqlConnection Conn, ICSection Section)
        {
            TF.YA.Base.DBICSection.UpdateSection (Log, Conn, Section);
        }

        public static bool GetSection(ILog Log, SqlConnection Conn, string JWDNumber,int SectionNumber, ICSection Section)
        {
            return TF.YA.Base.DBICSection.GetSection(Log, Conn, JWDNumber, SectionNumber, Section);
        }

        public static void DeleteSection(ILog Log, SqlConnection Conn ,string JWDNumber,int SectionNumber)
        {
            TF.YA.Base.DBICSection.DeleteSection(Log, Conn,  JWDNumber,SectionNumber);
        }
    }
}
