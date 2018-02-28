using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.DB
{
    public class DBConvert
    {
        public static string ToString(object obj)
        {
            string ret = string.Empty;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToString(obj);
                }
            }

            return ret;
        }

        public static int ToInt32(object obj)
        {
            int ret = 0;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToInt32(obj);
                }
            }

            return ret;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime ret = DateTime.Parse("2000-01-01");
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    if (!DateTime.TryParse(obj.ToString(), out ret))
                    {
                        ret = DateTime.Parse("2000-01-01");
                    }
                    else
                    {
                        if (ret < DateTime.Parse("2000-01-01"))
                        {
                            ret = DateTime.Parse("2000-01-01");
                        }
                    }
                }
            }

            return ret;
        }

        public static DateTime? ToDateTime_N(object obj)
        {
            DateTime? ret = null;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToDateTime(obj);
                }
            }

            return ret;

        }

        public static double ToDouble(object obj)
        {
            int ret = 0;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToInt32(obj);
                }
            }

            return ret;
        }
    }
}
