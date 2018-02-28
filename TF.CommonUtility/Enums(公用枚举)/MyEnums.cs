using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;

namespace TF.CommonUtility
{
    /// <summary>
    /// 公用枚举操作 by lzy 2014-08-12-11-29
    /// </summary>
    public class MyEnums
    {
        /// <summary>
        /// 月份枚举
        /// </summary>
        public enum MonthEnum
        {
            一月 = 1,
            二月 = 2,
            三月 = 3,
            四月 = 4,
            五月 = 5,
            六月 = 6,
            七月 = 7,
            八月 = 8,
            九月 = 9,
            十月 = 10,
            十一月 = 11,
            十二月 = 12
        }

        /// <summary>
        /// 星期枚举
        /// </summary>
        public enum WeekEnum
        {
            星期日 = 0,
            星期一 = 1,
            星期二 = 2,
            星期三 = 3,
            星期四 = 4,
            星期五 = 5,
            星期六 = 6
        }

        /// <summary>
        /// 从枚举中绑定值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static DataTable GetDataByEnum(Type enumType)
        {
            //自己构造DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("key",typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(int)));
            //dt.Rows.Add("", "", "", "", "");//public DataRow Add(params object[] values);
            string[] keys = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            for (int i = 0; i < keys.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = keys[i];
                dr[1] = Convert.ToInt32(values.GetValue(i));
                dt.Rows.Add(dr);
            }

            return dt;
        }

    }
}
