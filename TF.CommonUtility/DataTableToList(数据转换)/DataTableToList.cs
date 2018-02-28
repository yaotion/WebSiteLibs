using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Reflection;
using System.Collections;

namespace TF.CommonUtility
{
    /// <summary>
    /// DataTable转List类
    /// </summary>
    /// <typeparam name="T">任意类型</typeparam>
    public class TBToList<T> where T : new()
    {
        /// <summary>
        /// 获取列名集合
        /// </summary>
        private IList<string> GetColumnNames(DataColumnCollection dcc)
        {
            IList<string> list = new List<string>();
            foreach (DataColumn dc in dcc)
            {
                list.Add(dc.ColumnName);
            }
            return list;
        }

        /// <summary>
        /// 属性名称和类型名的键值对集合
        /// </summary>
        private Hashtable GetColumnType(DataColumnCollection dcc)
        {
            if (dcc == null || dcc.Count == 0)
            {
                return null;
            }
            IList<string> colNameList = GetColumnNames(dcc);

            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            Hashtable hashtable = new Hashtable();
            int i = 0;
            foreach (PropertyInfo p in properties)
            {
                foreach (string col in colNameList)
                {
                    if (col.ToLower().Contains(p.Name.ToLower()))
                    {
                        if (!hashtable.Contains(col))
                        {
                            hashtable.Add(col, p.PropertyType.ToString() + i++);
                        }
                    }
                }
            }

            return hashtable;
        }

        /// <summary>
        /// DataTable转换成IList
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IList<T> ToList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            PropertyInfo[] properties = typeof(T).GetProperties();//获取实体类型的属性集合
            Hashtable hh = GetColumnType(dt.Columns);//属性名称和类型名的键值对集合
            IList<string> colNames = GetColumnNames(hh);//按照属性顺序的列名集合
            List<T> list = new List<T>();
            T model = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                model = new T();//创建实体
                int i = 0;
                foreach (PropertyInfo p in properties)
                {
                    if (p.PropertyType == typeof(string))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? dr[colNames[i]] : "", null);
                        i++;
                    }
                    else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? int.Parse(dr[colNames[i]].ToString()) : 0, null);
                        i++;
                    }
                    else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? DateTime.Parse(dr[colNames[i]].ToString()) : DateTime.Now, null);
                        i++;
                    }
                    else if (p.PropertyType == typeof(float))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? float.Parse(dr[colNames[i]].ToString()) : 0, null);
                        i++;
                    }
                    else if (p.PropertyType == typeof(double))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? double.Parse(dr[colNames[i]].ToString()) : 0, null);
                        i++;
                    }
                    else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?))
                    {
                        p.SetValue(model, dr[colNames[i]].ToString() != "" ? decimal.Parse(dr[colNames[i]].ToString()) : 0, null);
                        i++;
                    }
                }

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 按照属性顺序的列名集合
        /// </summary>
        private IList<string> GetColumnNames(Hashtable hh)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();//获取实体类型的属性集合
            IList<string> ilist = new List<string>();
            int i = 0;
            foreach (PropertyInfo p in properties)
            {
                ilist.Add(GetKey(p.PropertyType.ToString() + i++, hh));
            }
            return ilist;
        }

        /// <summary>
        /// 根据Value查找Key
        /// </summary>
        private string GetKey(string val, Hashtable tb)
        {
            foreach (DictionaryEntry de in tb)
            {
                if (de.Value.ToString() == val)
                {
                    return de.Key.ToString();
                }
            }
            return null;
        }

    }
}
