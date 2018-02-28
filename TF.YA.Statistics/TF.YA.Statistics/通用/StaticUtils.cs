using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Statistics
{
    public class StaticUtils
    {
        public static string GetNowDateString()
        {
            return GetLogicNowDate().ToString("yyyy-MM-dd");
        }
        public static DateTime GetLogicNowDate()
        {
            DateTime ln = DateTime.Now;
            if (ln.Hour >= 18)
            {
                ln = DateTime.Now.AddDays(1).Date;
            }
            return ln;
        }
    }
}
